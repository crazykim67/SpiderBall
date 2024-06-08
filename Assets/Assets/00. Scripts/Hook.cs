using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField]
    private HookController hookController;
    [SerializeField]
    private DistanceJoint2D distanceJoint;

    [SerializeField]
    private float timer = 0f;

    [SerializeField]
    private float firstDrag = 0.1f;
    [SerializeField]
    private float secondDrag = 0.3f;

    private void Update()
    {
        PosCheck();
    }

    private void PosCheck()
    {
        if (hookController.isAttach)
        {
            timer += Time.deltaTime;
            if (timer > 5 && timer < 8)
                hookController.rg.drag = 0.1f;
            else if (timer > 8)
                hookController.rg.drag = 0.3f;
        }
        else
            timer = 0;
    }
    
}
