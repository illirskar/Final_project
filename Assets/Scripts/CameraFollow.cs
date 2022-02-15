using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Movement player;
    public Vector3 CameraOffset;
    [Range(0f, 1f)]
    public float CameraSpeed;

    public static CameraFollow Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        MoveCamera();
    }

    public void MoveCamera() //Method to make sure that the camera instance follows player object, taken from previous assignment.
    {
        Vector3 TargetPos = player.transform.position + CameraOffset;
        transform.position = Vector3.MoveTowards(transform.position, TargetPos, CameraSpeed);
    }
}
