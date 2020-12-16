using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseController : MonoBehaviour
{
    public GameObject[] wayPoints;
    int current = 0;
    public float speed;
    float radiusDetection = 0.5f;
    bool moving = false;
    Vector3 newDirection;
    Quaternion newRotation;

    // Update is called once per frame
    void Update()
    {
        if (moving && Vector3.Distance(wayPoints[current].transform.position, transform.position) < radiusDetection)
        {
            current++;
            if (current == wayPoints.Length)
                moving = false;
        }
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[current].transform.position, Time.deltaTime * speed);

            //newDirection = Vector3.RotateTowards(transform.forward, wayPoints[current].transform.position - transform.position, rotSpeed * Time.deltaTime, 0.0f);
            newRotation = Quaternion.LookRotation((wayPoints[current].transform.position - transform.position).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * speed);
        }
    }

    public void SetMoving(bool value)
    {
        moving = value;
    }

    public void Reset()
    {
        SetMoving(false);
        current = 0;
    }
}
