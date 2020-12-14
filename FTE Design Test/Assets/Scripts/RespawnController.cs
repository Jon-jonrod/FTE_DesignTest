using UnityEngine;
using System.Collections;
public class RespawnController : MonoBehaviour
{
    private CharacterControllerScript characterControllerScript;
    public delegate void MyDelegate();
    public event MyDelegate onRespawn;
    Vector3 initialPosition;
    Quaternion initialRotation;
    void Awake()
    {
        characterControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerScript>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        characterControllerScript.onDeath += OnRespawn;
    }
    public void OnRespawn()
    {
        transform.position = initialPosition;
        //onRespawn();
    }
}
