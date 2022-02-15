using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToStart : MonoBehaviour
{

    public Vector3 Location;
    public Rigidbody rigidbody;


    private void OnTriggerEnter(Collider other)
    {
        rigidbody.position = Location;
    }
}
