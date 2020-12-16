using UnityEngine;
using System.Collections;

/// <summary>
/// Script used when a checkpoint is activated or the character is dead 
/// It makes the object respawn to its initial position as well as resetting some parameters for specials objects
/// </summary>
public class RespawnController : MonoBehaviour
{
    private CharacterControllerScript characterControllerScript;
    public delegate void MyDelegate();
    public event MyDelegate onRespawn;
    Vector3 spawnPosition;
    Quaternion spawnRotation;

    //public Vector3 alternativePos;
    //public bool useAlternativePos=false;

    void Awake()
    {
        //We setup the initial pos & rot and we associate the OnRespawn() function to the character's death
        characterControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerScript>();
        spawnPosition = transform.localPosition;
        spawnRotation = transform.localRotation;
        characterControllerScript.onDeath += OnRespawn;
    }
    public void OnRespawn()
    {
        transform.localPosition = spawnPosition;
        transform.localRotation = spawnRotation;

        //For the special exceptions, we do a case by case script
        if (name == "BadGuy")
        {
            GetComponent<BossChaseController>().Reset();
        }

        if (name == "PlaneDeath")
        {
            gameObject.SetActive(false);
        }
    }


    //Function used if the initial pos & rot have to changed
    public void SetNewSpawn(Vector3 pos, Quaternion rot=new Quaternion())
    {
        spawnPosition = pos;
        spawnRotation = rot;
    }

}
