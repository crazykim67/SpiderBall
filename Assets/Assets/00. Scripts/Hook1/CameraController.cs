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

    private void Awake() => cam = Camera.main;

    private void Update()
    {
        camPos = new Vector3(playerTr.position.x, playerTr.position.y, this.transform.position.z);
        this.transform.position = camPos;

        if (hookController.rg.velocity.magnitude >= 6 && hookController.rg.velocity.magnitude < 10)
            cam.orthographicSize = hookController.rg.velocity.magnitude;
        else
            cam.orthographicSize = 6;
    }
}
