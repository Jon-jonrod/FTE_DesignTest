using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterControllerScript : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;

    //Variables for movement
    float horizontal, vertical;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //Variables for jump
    public LayerMask groundMask;
    Vector3 velocity;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    bool isGrounded;
    public float jumpHeight = 3f;

    //Variables for Button
    bool isOnButton;
    public LayerMask buttonMask;
    


    //Variables for Grab
    public bool grabbed;
    RaycastHit hit;
    public float distance = 2f;
    public Transform destinationGrab, originGrab;
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
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded)
            {
                if (velocity.y < 0)
                    velocity.y = -2f;
            }


            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 movDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(movDir.normalized * speed * Time.deltaTime);


            }

            Debug.DrawRay(originGrab.transform.position, transform.TransformDirection(Vector3.forward) * distance, Color.yellow);


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
        if (!grabbed)
        {
            if (Physics.Raycast(originGrab.transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, grabbableMask))
            {
                if (hit.collider != null && hit.collider.tag == "Grabbable")
                {
                    grabbed = true;
                    hit.collider.GetComponent<Rigidbody>().useGravity = false;
                    hit.collider.GetComponent<Rigidbody>().isKinematic = true;
                    hit.collider.transform.position = destinationGrab.position;
                    hit.collider.transform.parent = destinationGrab;

                }
            }
        }
        else
        {
            grabbed = false;
            hit.collider.transform.parent = GameObject.Find("GrabbablesObjects").transform;
            hit.collider.GetComponent<Rigidbody>().useGravity = true;
            hit.collider.GetComponent<Rigidbody>().isKinematic = false;
        }


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
