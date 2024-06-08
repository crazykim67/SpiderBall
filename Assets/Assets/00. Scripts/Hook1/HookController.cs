using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool isHook = false;
    private bool isLengthMax = false;
    public bool isAttach = false;

    private bool isFirst = false;

    private void Start()
    {
        hookTr.position = transform.position;
        line.positionCount = 2;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hookTr.position);
        line.useWorldSpace = true;
    }

    private void Update()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hookTr.position);

        if (Input.GetMouseButtonDown(0) && !isHook)
        {
            // 고리가 플레이어에게서 발사되기 때문에 hookTr을 플레이어 위치로 초기화
            hookTr.position = transform.position;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hookTr.position);
            mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            isHook = true;
            hookTr.gameObject.SetActive(true);

            if (!isFirst)
            {
                isFirst = true;
                rg.bodyType = RigidbodyType2D.Dynamic;
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
            hookTr.position = Vector2.MoveTowards(hookTr.position, transform.position, Time.deltaTime * 50);
            if(Vector2.Distance(transform.position, hookTr.position) < 0.1f)
            {
                isHook = false;
                isLengthMax = false;
                hookTr.gameObject.SetActive(false);
            }
        }
        // 고리가 벽에 연결되어있는 상태
        else if (isAttach)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isAttach = false;
                isHook = false;
                isLengthMax = false;
                distanceJoint.enabled = false;
                hookTr.gameObject.SetActive(false);
                Debug.Log("취소");
            }
        }
    }
}
