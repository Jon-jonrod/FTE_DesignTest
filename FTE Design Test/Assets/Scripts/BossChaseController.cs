using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is the controller for the Boss Chaser. I just used a few waypoints for this one as an AI would have been too much for a prototype
/// </summary>
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
