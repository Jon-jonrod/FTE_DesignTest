using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;


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
        checkpointController = GameObject.Find("CheckpointController").GetComponent<CheckpointController>();
        cutsceneToReplay = new StartCutsceneScript[cutsceneToAffect.Length];
        cutsceneToStops = new PlayableDirector[cutsceneToAffect.Length];
        for (int i=0; i<cutsceneToAffect.Length; i++)
        {
            Debug.Log(i);
            cutsceneToReplay[i] = cutsceneToAffect[i].GetComponent<StartCutsceneScript>();
            cutsceneToStops[i] = cutsceneToAffect[i].GetComponent<PlayableDirector>();
        }


        characterScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerScript>();
        characterScript.onDeath += OnCharacterDeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            RespawnController charaRespawn = other.GetComponent<RespawnController>();
            charaRespawn.SetNewSpawn(other.transform.position, other.transform.rotation);
            checkpointController.NewCheckpoint(numCheckpoint, activeCamera);
        }
    }

    void OnCharacterDeath()
    {
        for (int i=0; i<cutsceneToAffect.Length; i++)
        {
            cutsceneToStops[i].Stop();
            cutsceneToReplay[i].Reset();
        }
    }
}
