using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controller of all the checkpoints in the game. It'll know at which checkpoint we're at, set the camera and call the death function if the player chooses a checkpoint
/// I put some general functions in here as well, like slowmotion, quit the application and restart the level.
/// </summary>
public class CheckpointController : MonoBehaviour
{
    private CinemachineVirtualCamera currentCam;
    public CinemachineVirtualCamera firstCamera;
    //public CinemachineVirtualCamera[] camerasCheckpoint;
    private CinemachineVirtualCamera tempCam;

    private CharacterControllerScript characterControllerScript;

    private int numCheckpoint=0;

    private RespawnController charaRespawnController;
    //public Transform[] spawnsCheckpoint;

    //Input
    InputMaster controls;


    private void Start()
    {
        characterControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerScript>();
        charaRespawnController = GameObject.FindGameObjectWithTag("Player").GetComponent<RespawnController>();
        characterControllerScript.onDeath += OnCharacterDeath;

        controls = characterControllerScript.getControls();
        controls.GameController.Restart.performed += context => Restart();
        controls.GameController.FirstCheckpoint.performed += context => GoToCheckpoint(0);
        controls.GameController.SecondCheckpoint.performed += context => GoToCheckpoint(1);
        controls.GameController.Quit.performed += context => Exit();
        controls.GameController.SlowMo.performed += context => SlowMo();

        currentCam = firstCamera;
        OnCharacterDeath();
    }


    /// <summary>
    /// When a player enters a checkpoint, we set the new cameras
    /// </summary>
    /// <param name="numCheckpointPassed"></param>
    /// <param name="activeCamera"></param>
    public void NewCheckpoint(int numCheckpointPassed, CinemachineVirtualCamera activeCamera = null)
    {
        numCheckpoint=numCheckpointPassed;
        if (activeCamera == null)
            activeCamera = firstCamera;

        currentCam = activeCamera;
    }

    /// <summary>
    /// Function called when the player is dead, we reset all cameras and up the priority of the main used for the checkpoint
    /// </summary>
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


    /// <summary>
    /// When the input for the checkpoint is triggered, we call the "death" function, and set the camera.
    /// </summary>
    /// <param name="numcp"></param>
    void GoToCheckpoint(int numcp=-1)
    {
       
        if (numcp != -1)
            numCheckpoint = numcp;

        Checkpoint checkpointScript=null;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Checkpoint"))
        {
            if (obj.GetComponent<Checkpoint>().numCheckpoint == numCheckpoint)
                checkpointScript = obj.GetComponent<Checkpoint>();
        }

        if (checkpointScript != null)
        {
            currentCam = checkpointScript.activeCamera;
            charaRespawnController.SetNewSpawn(checkpointScript.spawnPos.position);
            characterControllerScript.Death();
        }
    }


    //A few functions that controls the game
    public void SlowMo()
    {
        if (Time.timeScale == 1)
            Time.timeScale = 0.1f;
        else
            Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
