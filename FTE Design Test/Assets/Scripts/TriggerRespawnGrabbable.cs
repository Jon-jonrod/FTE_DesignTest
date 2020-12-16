using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
