using UnityEngine;
using System.Collections;
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
        characterControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerScript>();
        spawnPosition = transform.localPosition;
        spawnRotation = transform.localRotation;
        characterControllerScript.onDeath += OnRespawn;
    }
    public void OnRespawn()
    {
       
        //if (useAlternativePos)
            //spawnPosition = alternativePos;
        transform.localPosition = spawnPosition;
        transform.localRotation = spawnRotation;
        if (name == "BadGuy")
        {
            GetComponent<BossChaseController>().Reset();
        }

        if (name == "PlaneDeath")
        {
            gameObject.SetActive(false);
        }
        //onRespawn();
    }

    public void SetNewSpawn(Vector3 pos, Quaternion rot=new Quaternion())
    {
        spawnPosition = pos;
        spawnRotation = rot;
    }

}
