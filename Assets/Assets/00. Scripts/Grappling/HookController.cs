using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HookController : MonoBehaviour
{
    public Rigidbody2D rg;
    [SerializeField]
    private DistanceJoint2D distanceJoint;
    [SerializeField]
    private LineRenderer line;
    [SerializeField]
    private Transform hookTr;
    // 고리가 날아가야하는 방향
    private Vector2 mouseDir;
    private Vector2 targetPos;

    [SerializeField]
    private bool isHook = false;
    [SerializeField]
    private bool isLengthMax = false;
    public bool isAttach = false;

    private bool isFirst = false;

    [SerializeField]
    private float rayDis = 0.2f;

    private Vector2 prevVelocity;
    [SerializeField]
    private bool isLeft = false;
    [SerializeField]
    private Button boostBtn;
    private bool isBoost = false;

    private void Start()
    {
        prevVelocity = rg.velocity;
        hookTr.position = transform.position;
        line.positionCount = 2;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hookTr.position);
        line.useWorldSpace = true;
    }

    // UI 터치 시 Raycast 방지 함수
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void Update()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hookTr.position);

        if (Input.GetMouseButtonDown(0) && !isHook && !IsPointerOverUIObject())
        {
            // 고리가 플레이어에게서 발사되기 때문에 hookTr을 플레이어 위치로 초기화
            hookTr.position = transform.position;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hookTr.position);
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDir = targetPos - (Vector2)transform.position;

            RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector2.zero);

            if(hit.collider != null && hit.collider.CompareTag("Ring"))
            {
                isHook = true;
                hookTr.gameObject.SetActive(true);

                if (!isFirst)
                {
                    isFirst = true;
                    rg.bodyType = RigidbodyType2D.Dynamic;
                }
            }
        }

        // 고리가 활성화 되어있으며 최고 길이가 아닐 때
        if (isHook && !isLengthMax && !isAttach)
        {
            // 벡터의 방향을 유지한 상태로 길이 정규화
            hookTr.Translate(mouseDir.normalized * Time.deltaTime * 50);

            // 플레이어와 고리의 거리가 최대 거리를 벗어나면
            if(Vector2.Distance(transform.position, hookTr.position) > 7)
                isLengthMax = true;
        }
        // 고리가 활성화 되어있으며 최고 길이일 때
        else if (isHook && isLengthMax && !isAttach)
        {
            Vector2[] direaction = new Vector2[]
            {
                Vector2.up,
                Vector2.down,
                Vector2.left,
                Vector2.right
            };

            foreach (var dir in direaction)
            {
                RaycastHit2D hit = Physics2D.Raycast(hookTr.position, dir, rayDis);

                if (hit.collider != null && hit.collider.CompareTag("Ring"))
                {
                    isAttach = true;
                    distanceJoint.enabled = true;
                    isBoost = true;
                }
                else
                {
                    hookTr.position = Vector2.MoveTowards(hookTr.position, transform.position, Time.deltaTime * 50);
                    if (Vector2.Distance(transform.position, hookTr.position) < 0.1f)
                    {
                        isHook = false;
                        isLengthMax = false;
                        hookTr.gameObject.SetActive(false);
                    }
                }
            }
        }
        // 고리가 벽에 연결되어있는 상태
        else if (isAttach)
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
                UnGrappling();

        if(isHook && !isAttach && Vector2.Distance(hookTr.position, targetPos) < 0.1f)
        {
            isAttach = true;
            distanceJoint.enabled = true;
            isBoost = true;
        }

        HookDirectionCheck();
        BoostCheck();
    }

    public void UnGrappling()
    {
        isAttach = false;
        isHook = false;
        isLengthMax = false;
        distanceJoint.enabled = false;
        hookTr.gameObject.SetActive(false);
        isBoost = false;
    }


    public void HookDirectionCheck()
    {
        if (!isHook || !isAttach)
            return;

        // 가속도 (velocity / 시간)
        Vector2 acceleration = (rg.velocity - prevVelocity) / Time.deltaTime;

        // 힘의 방향 (질량 * 가속도)
        Vector2 forceDirection = rg.mass * acceleration;

        // 힘의 방향을 정규화하여 단위 벡터로 변환
        Vector2 normalizedForceDir = forceDirection.normalized;

        if(normalizedForceDir.x < 0)
            isLeft = true;
        else if(normalizedForceDir.x > 0)
            isLeft = false;

        prevVelocity = rg.velocity;
    }

    private void BoostCheck()
    {
        if (!isBoost)
            boostBtn.interactable = false;
        else
            boostBtn.interactable = true;
    }

    public void OnClickBoost()
    {
        if(!isBoost || !isHook || !isAttach)
            return;

        isBoost = false;

        if (isLeft)
            rg.AddForce(new Vector2(-500, 0));
        else
            rg.AddForce(new Vector2(500, 0));
    }
}
