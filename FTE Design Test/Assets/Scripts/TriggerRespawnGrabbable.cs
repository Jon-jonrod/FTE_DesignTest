using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Safety script used in case the grabbable objects fall through the floor
/// </summary>
public class TriggerRespawnGrabbable : MonoBehaviour
{
    public Transform newSpawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Grabbable")
        {
            other.transform.position = newSpawn.position;
        }
    }
}
