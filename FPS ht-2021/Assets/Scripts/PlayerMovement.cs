using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    CharacterController playerCtrl;
    

    float originalHeight;
    public float reducedHeight;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;   

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask; 

    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        playerCtrl = GetComponent<CharacterController>();
        originalHeight = playerCtrl.height;
    }

    // Update is called once per frame
    void Update()
    {
        //Kollar om jag står på marken
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime); 

        //Jump;
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); 
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //Sprint;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 30f;
        }
        else
            speed = 12f;

       //Crouch;
       if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = 6f;
            Crouch();
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            speed = 12f;
            GoUp();
        }
    }

    void Crouch()
    {
        playerCtrl.height = reducedHeight;
    }

    void GoUp()
    {
        playerCtrl.height = originalHeight;
    }
}

