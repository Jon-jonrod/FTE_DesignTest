using UnityEngine;
using System.Collections;
public class RespawnController : MonoBehaviour
{
    private CharacterControllerScript characterControllerScript;
    public delegate void MyDelegate();
    public event MyDelegate onRespawn;
    Vector3 spawnPosition;
    Quaternion spawnRotation;
    void Awake()
    {
        characterControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerScript>();
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
        characterControllerScript.onDeath += OnRespawn;
    }
    public void OnRespawn()
    {
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
        //onRespawn();
    }

    public void SetNewSpawn(Vector3 pos, Quaternion rot)
    {
        spawnPosition = pos;
        spawnRotation = rot;
    }
}
