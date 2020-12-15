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
        controls.GameController.Quit.performed += context => Exit();
        controls.GameController.SlowMo.performed += context => SlowMo();

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

    public void SlowMo()
    {
        if (Time.timeScale == 1)
            Time.timeScale = 0.1f;
        else
            Time.timeScale = 1;
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

    public void Exit()
    {
        Application.Quit();
    }

    void GoToCheckpoint()
    {
        currentCam = cameraCheckpoint;
        charaRespawnController.SetNewSpawn(spawnCheckpoint.position);
        characterControllerScript.Death();
    }
}
