using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRespawnGrabbable : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Grabbable")
        {
            if (other.GetComponent<RespawnController>()!=null)
                other.GetComponent<RespawnController>().OnRespawn();
        }
    }
}
