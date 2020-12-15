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

    // Start is called before the first frame update
    void Start()
    {
        checkpointController = GameObject.Find("CheckpointController").GetComponent<CheckpointController>();
        cutsceneToReplay = new StartCutsceneScript[cutsceneToAffect.Length];
        cutsceneToStops = new PlayableDirector[cutsceneToAffect.Length];
        for (int i=0; i<cutsceneToAffect.Length; i++)
        {
            cutsceneToReplay[i] = cutsceneToAffect[i].GetComponent<StartCutsceneScript>();
            cutsceneToStops[i] = cutsceneToAffect[i].GetComponent<PlayableDirector>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CharacterControllerScript characterScript = other.GetComponent<CharacterControllerScript>();
            RespawnController charaRespawn = other.GetComponent<RespawnController>();
            characterScript.onDeath += OnCharacterDeath;
            charaRespawn.SetNewSpawn(other.transform.position, other.transform.rotation);
            checkpointController.SetTransformSpawn(activeCamera);
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
