using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField]
    private HookController hookController;
    [SerializeField]
    private DistanceJoint2D distanceJoint;

    private float timer = 0f;

    private void Update()
    {
        PosCheck();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Ring")
        {
            distanceJoint.enabled = true;
            hookController.isAttach = true;
        }
    }

    private void PosCheck()
    {
        if (hookController.isAttach)
        {
            timer += Time.deltaTime;
            if (timer > 5 && timer < 8)
                hookController.rg.drag = 0.05f;
            else if (timer > 8)
                hookController.rg.drag = 0.2f;
        }
        else
            timer = 0;
    }
    
}
