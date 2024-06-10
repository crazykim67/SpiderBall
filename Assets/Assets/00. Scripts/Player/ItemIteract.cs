using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIteract : MonoBehaviour
{
    private Rigidbody2D rg;
    private HookController hookController;

    private Vector2 velocity;

    private void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        hookController = GetComponent<HookController>();
    }

    private void Update()
    {
        velocity = rg.velocity;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.CompareTag("Booster"))
        {
            Destroy(coll.gameObject);
            rg.AddForce(velocity * 100f, ForceMode2D.Force);
        }
        else if (coll.transform.CompareTag("ArrowBooster"))
        {
            if (hookController.isAttach)
                hookController.UnGrappling();

            rg.velocity = Vector2.zero;
            Destroy(coll.gameObject);
            rg.AddForce((Vector2.right + Vector2.up).normalized * 1000f, ForceMode2D.Force);
        }
    }
}
