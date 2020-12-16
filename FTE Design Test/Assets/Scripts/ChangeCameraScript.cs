using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// Script used to change the live camera in the cinematicBrain component of the main camera
/// </summary>
public class ChangeCameraScript : MonoBehaviour
{
    public CinemachineVirtualCamera camera1, camera2;
    public float timeDelay=0;
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Invoke("ChangeCam", timeDelay);
        }
    }

    private void ChangeCam()
    {

        if (camera2.Priority > camera1.Priority)
            camera1.Priority = camera2.Priority + 1;
        else camera2.Priority = camera1.Priority + 1;

    }
}
