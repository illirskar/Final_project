using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwap : MonoBehaviour
{
    private Camera PrevCamera;
    public Camera NextCamera;

    public Canvas UICanvas;

    private void OnTriggerEnter(Collider other) //Entering a trigger as a player will deactivate the previously active camera and activate the new one.
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (Camera.current != null)
            {
                PrevCamera = Camera.current;
                PrevCamera.GetComponent<Camera>().enabled = false;
                PrevCamera.tag = "Cams";
            };

            NextCamera.tag = "MainCamera"; //Tag-switching is the part that actually swaps between cameras.

            NextCamera.GetComponent<Camera>().enabled = true;

            UICanvas.worldCamera = NextCamera;
        }
    }
}
