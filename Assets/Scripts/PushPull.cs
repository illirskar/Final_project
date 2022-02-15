using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPull : MonoBehaviour
{
    public float forceRange = 2;
    public float Force = 1;
    public bool push;

    public AudioSource whoosh;

    public void FixedUpdate() 
    {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, forceRange)) //Checks for objects in range that is assigned in editor, with 2 being default unless specified otherwise in prefabs.
        {
            //Directional vector
            Vector3 forceDirection = transform.position - collider.transform.position;
            forceDirection = forceDirection.normalized;
            Vector3 scaleDown = new Vector3(Mathf.Pow(forceDirection.x, -1), Mathf.Pow(forceDirection.y, -1), Mathf.Pow(forceDirection.z, -1));

            if (collider.gameObject.CompareTag("Player")) //Checks for Player tag to avoid trying to push objects without Rigidbody component.
            {
                whoosh.Play();

                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (!push)
                {
                    rb.AddForce(forceDirection.normalized * 2.5f * Mathf.Pow(Force, 2) * Time.fixedDeltaTime, ForceMode.Impulse); //Pulls the object
                }
                else
                {
                    rb.AddForce(-forceDirection.normalized * Mathf.Pow(Force, 2) * Time.fixedDeltaTime, ForceMode.VelocityChange); //Pushes the object
                }

                if (rb.velocity.y != 0f)
                {
                    Vector3 push = new Vector3(0f, forceDirection.y, 0f);
                    rb.AddForce(push, ForceMode.VelocityChange);
                }
            }
        }
    }

}
