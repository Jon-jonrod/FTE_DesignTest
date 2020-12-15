using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour
{
    private CinemachineVirtualCamera currentCam;
    public CinemachineVirtualCamera firstCamera;

    private CharacterControllerScript characterControllerScript;

    private int numCheckpoint=0;

    //Input
    InputMaster controls;

    private void Awake()
    {
        controls = new InputMaster();
        controls.GameController.Restart.performed += context => Restart();
        currentCam = firstCamera;
        OnCharacterDeath();
    }

    private void Start()
    {
        characterControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerScript>();
        characterControllerScript.onDeath += OnCharacterDeath;
    }

    public void NewCheckpoint(int numCheckpointPassed, CinemachineVirtualCamera activeCamera = null)
    {
        numCheckpoint=numCheckpointPassed;
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
            else
            {
                virtualCam.Priority = 20;
            }
        }
    }

    void Restart()
    {
        if (numCheckpoint == 0)
            SceneManager.LoadScene("Level");
        else characterControllerScript.Death();
    }
}
