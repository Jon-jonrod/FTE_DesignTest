using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour
{
    private CinemachineVirtualCamera currentCam;
    public CinemachineVirtualCamera firstCamera, cameraCheckpoint;
    private CinemachineVirtualCamera tempCam;

    private CharacterControllerScript characterControllerScript;

    private int numCheckpoint=0;

    private RespawnController charaRespawnController;
    public Transform spawnCheckpoint;

    //Input
    InputMaster controls;


    private void Start()
    {
        characterControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerScript>();
        charaRespawnController = GameObject.FindGameObjectWithTag("Player").GetComponent<RespawnController>();
        characterControllerScript.onDeath += OnCharacterDeath;

        controls = characterControllerScript.getControls();
        controls.GameController.Restart.performed += context => Restart();
        controls.GameController.Checkpoint.performed += context => GoToCheckpoint();

        currentCam = firstCamera;
        OnCharacterDeath();
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
            virtualCam.Priority = 10;

            if (virtualCam == currentCam)
                tempCam = virtualCam;
        }
        StartCoroutine(WaitCam(tempCam, 0.02f));
       
    }

    IEnumerator WaitCam(CinemachineVirtualCamera cam, float waitTime = 0.02f)
    {
        yield return new WaitForSeconds(waitTime);
        cam.Priority = 20;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level");
    }

    void GoToCheckpoint()
    {
        currentCam = cameraCheckpoint;
        charaRespawnController.SetNewSpawn(spawnCheckpoint.position);
        characterControllerScript.Death();
    }
}
