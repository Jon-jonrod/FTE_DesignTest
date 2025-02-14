﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// Script used for controlling the character movement and behaviour
/// </summary>
public class CharacterControllerScript : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;

    //Variables for movement
    float horizontal, vertical;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    Vector3 direction;

    //Variables for jump
    public LayerMask groundMask;
    Vector3 velocity;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    bool isGrounded;
    public float jumpHeight = 3f;
    
    //Variables for Grab
    public bool grabbed;
    RaycastHit hit;
    public float distance = 2f;
    public Transform destinationGrab, originGrab, originGrab2;
    public LayerMask grabbableMask;

    //Input
    InputMaster controls;

    //Death & Checkpoint
    public delegate void MyDelegate();
    public event MyDelegate onDeath;
    private bool dead;

    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Jump.performed += context => Jump();
        controls.Player.Grab.performed += context => Grab();
        controls.Player.Movement.performed += context => Move(context.ReadValue<Vector2>());
       

    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            //Movement of the character
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded)
            {
                if (velocity.y < 0)
                    velocity.y = -2f;
            }


            direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);


                Vector3 movDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                controller.Move(movDir.normalized * speed * Time.deltaTime);

            }

            //Debug.DrawRay(originGrab.transform.position, transform.TransformDirection(Vector3.forward) * distance, Color.yellow);

            //Gravity applied on the character
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

        }
    }

    void Move(Vector2 direction)
    {
        horizontal = direction.x;
        vertical = direction.y;
    }

    void Grab()
    {
        //Using raycast to detect if objects are in the right position to be grabbed
        if (!grabbed)
        {
            if (Physics.Raycast(originGrab.transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, grabbableMask) ||
                Physics.Raycast(originGrab2.transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, grabbableMask))
            {
                if (hit.collider != null && hit.collider.tag == "Grabbable")
                {
                    grabbed = true;
                    hit.collider.transform.position = new Vector3(destinationGrab.position.x, hit.collider.transform.position.y, destinationGrab.position.z);
                    hit.collider.transform.parent = transform;
                    hit.collider.GetComponent<GrabbableScript>().SetGrabbed(true);
                    if (hit.collider.GetComponent<Rigidbody>() != null)
                    {
                        hit.collider.GetComponent<Rigidbody>().useGravity = false;
                    }

                }
            }
        }
        else
        {
            grabbed = false;
            hit.collider.transform.parent = GameObject.Find("GrabbableObjects").transform;
            hit.collider.GetComponent<GrabbableScript>().SetGrabbed(false);
            if (hit.collider.GetComponent<Rigidbody>() != null)
            {
                hit.collider.GetComponent<Rigidbody>().useGravity = true;
            }
        }


    }

    public void SetGrabbed(bool value)
    {
        grabbed = value;
    }

    public void Jump()
    {
        if (isGrounded && !grabbed)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    public void Death()
    {
        dead = true;
        onDeath.Invoke();
        Invoke("WaitDeath", 0.5f);
    }

    void WaitDeath()
    {
        dead = false;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void EnableControls()
    {
        controls.Enable();
    }

    public void DisableControls()
    {
        controls.Disable();
    }


    public void Stop()
    {
        horizontal = 0;
        vertical = 0;
    }
    
    void OnTriggerEnter(Collider other)
    {       
        if (other.tag == "ButtonMusic")
        {
            other.GetComponent<ButtonMusic>().PlaySound();
        }

        if (other.tag == "Death")
        {
            Death();
        }

        
    }

    public InputMaster getControls()
    {
        return controls;
    }

}
