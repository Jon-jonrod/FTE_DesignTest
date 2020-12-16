using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

/// <summary>
/// Script used to trigger a checkpoint in the level as well as containing necessaries informations for the checkpoint : cutscenes, spawnPosition, camera
/// </summary>
public class Checkpoint : MonoBehaviour
{
    public CinemachineVirtualCamera activeCamera;
    private CheckpointController checkpointController;

    private StartCutsceneScript[] cutsceneToReplay;
    private PlayableDirector[] cutsceneToStops;
    
    public GameObject[] cutsceneToAffect;

    public int numCheckpoint = 1;
    public Transform spawnPos;

    CharacterControllerScript characterScript;

    // Start is called before the first frame update
    void Start()
    {

        //For each checkpoint, the designer has to specify which cutscenes are impacted by which checkpoint. He can use the public variable "cutsceneToAffect" to do so.
        checkpointController = GameObject.Find("CheckpointController").GetComponent<CheckpointController>();
        cutsceneToReplay = new StartCutsceneScript[cutsceneToAffect.Length];
        cutsceneToStops = new PlayableDirector[cutsceneToAffect.Length];
        for (int i=0; i<cutsceneToAffect.Length; i++)
        {
            cutsceneToReplay[i] = cutsceneToAffect[i].GetComponent<StartCutsceneScript>();
            cutsceneToStops[i] = cutsceneToAffect[i].GetComponent<PlayableDirector>();
        }


        characterScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerScript>();
        characterScript.onDeath += OnCharacterDeath;
    }


    private void OnTriggerEnter(Collider other)
    {
        //If the player enters, we set the new checkpoint
        if (other.tag == "Player")
        {
            RespawnController charaRespawn = other.GetComponent<RespawnController>();
            charaRespawn.SetNewSpawn(other.transform.position, other.transform.rotation);
            checkpointController.NewCheckpoint(numCheckpoint, activeCamera);
        }
    }

    //When restarting, we have to reset the cutscenes
    void OnCharacterDeath()
    {
        for (int i=0; i<cutsceneToAffect.Length; i++)
        {
            cutsceneToStops[i].Stop();
            cutsceneToReplay[i].Reset();
        }
    }
}
