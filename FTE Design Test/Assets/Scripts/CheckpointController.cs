using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CheckpointController : MonoBehaviour
{
    private CinemachineVirtualCamera currentCam;
    public CinemachineVirtualCamera firstCamera;

    private CharacterControllerScript characterControllerScript;


    private void Start()
    {
        characterControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerScript>();
        characterControllerScript.onDeath += OnCharacterDeath;
    }

    public void SetTransformSpawn(CinemachineVirtualCamera activeCamera=null)
    {
        if (activeCamera == null)
            activeCamera = firstCamera;
       
        currentCam = activeCamera;
    }

    void OnCharacterDeath()
    {
        foreach (GameObject cam in GameObject.FindGameObjectsWithTag("VirtualCamera"))
        {
            CinemachineVirtualCamera virtualCam = cam.GetComponent<CinemachineVirtualCamera>();
            if (virtualCam != currentCam)
                virtualCam.Priority = 10;
            else virtualCam.Priority = 20;
        }
    }
}
