using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeCameraScript : MonoBehaviour
{
    public CinemachineVirtualCamera firstCamera, newCamera;

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ChangeCam();
        }
    }

    private void ChangeCam()
    {
        newCamera.Priority = firstCamera.Priority + 1;
    }
}
