using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script used grab object. It uses physics so it can have some weird behaviour. 
/// </summary>
public class GrabbableScript : MonoBehaviour
{
    private bool isGrabbed;
    private Rigidbody myRigidbody;
    private float distance;
    private CharacterControllerScript characterControllerScript;
    private Transform playerTransform;
    public float maxDistanceGrabbed = 1;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        characterControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabbed)
        {
            //We don't want the object to move when it's grabbed by the player
            myRigidbody.velocity = Vector3.zero;
            myRigidbody.angularVelocity = Vector3.zero; if (distance >= maxDistanceGrabbed)

            //if the player is too far away from the object, then the object will be dropped
            distance = Vector3.Distance(transform.position, playerTransform.position);          
            if (distance >= maxDistanceGrabbed)
            {
                Drop();
            }
        }   
    }

    public void SetGrabbed(bool value)
    {
        isGrabbed = value;
    }

    public void Drop()
    {
        SetGrabbed(false); 
        transform.parent = GameObject.Find("GrabbableObjects").transform;
        myRigidbody.useGravity = true;
        characterControllerScript.SetGrabbed(false);
    }
}
