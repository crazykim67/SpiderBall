using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;

    [SerializeField]
    private Transform playerTr;

    private Vector3 camPos;

    [SerializeField]
    private HookController hookController;

    [SerializeField]
    private float camSpeed;

    private void Awake() => cam = Camera.main;

    private void Update()
    {
        camPos = new Vector3(Mathf.Lerp(camPos.x, playerTr.position.x, camSpeed), Mathf.Lerp(camPos.y, playerTr.position.y, camSpeed), this.transform.position.z);
        this.transform.position = camPos;
        if (hookController.rg.velocity.magnitude >= 6)
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, hookController.rg.velocity.magnitude, camSpeed);
        else
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 6, camSpeed);
    }
}
