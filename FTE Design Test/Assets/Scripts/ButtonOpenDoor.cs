using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOpenDoor : MonoBehaviour
{
    public Transform door, doorNextPosition;
    public float speed=1f;
    private bool opened;
    private Vector3 doorTarget;

    // Start is called before the first frame update
    void Start()
    {
        doorTarget = doorNextPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (opened)
        {
            door.position = Vector3.MoveTowards(door.position, doorTarget, Time.deltaTime * speed);
        }
    }

    public void OpenDoor()
    {
        opened = true;
    }
}
