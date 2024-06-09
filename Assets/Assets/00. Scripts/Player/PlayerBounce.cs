using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : MonoBehaviour
{
    private Rigidbody2D rg;
    [SerializeField]
    private HookController hookController;

    Vector2 velocity;

    private void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        hookController = GetComponent<HookController>();
    }

    private void Update()
    {
        velocity = rg.velocity;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.CompareTag("Monster"))
        {
            //충돌 시 그래플이 풀리면서 반대 방향으로 튕겨내기
            if(hookController.isAttach)
                hookController.UnGrappling();

            float speed = velocity.magnitude;
            Vector2 dir = Vector2.Reflect(velocity.normalized, coll.contacts[0].normal);

            rg.velocity = dir * Mathf.Max(speed, 0f);
        }
        else if (coll.transform.CompareTag("NoRing"))
            if (hookController.isAttach) 
                hookController.UnGrappling();
    }
}
