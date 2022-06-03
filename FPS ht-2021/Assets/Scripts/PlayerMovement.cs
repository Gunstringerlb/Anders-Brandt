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

    private Vector3 grndCheckPos;
    bool isGrounded;
    [SerializeField] public Vector3 velocity;

    [Space] 
    [Space] // Bara lite roliga grejer för att göra inspector window renare/finare.

    [Header("Wall Climbing")]
    [SerializeField] private float wallClimbAngle = 10;
    [SerializeField] private float wallClimbHeight = 4;
    [SerializeField] private float wallClimbDist = 1;
    [SerializeField] private float wallClimbTime = 1;
    [SerializeField] private float crestFactor = 0.1f;

    // Sätter startvärden för längd, groundedposition & charactercontroller
    void Start()
    {
        playerCtrl = GetComponent<CharacterController>();
        originalHeight = playerCtrl.height;
        grndCheckPos = groundCheck.localPosition;
        
    }
    

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        } // Kollar om jag står på marken


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //Kontrollerar hopp funktion
        if (Input.GetButtonDown("Jump") && isGrounded)
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
        

        Ray ray = new Ray(Camera.main.transform.position, transform.forward);
        RaycastHit hitInfo;
        if(Input.GetAxis("Vertical") > 0  && Physics.Raycast(ray, out hitInfo, wallClimbDist))
        {
            if (hitInfo.normal.y <= Mathf.Sin(wallClimbAngle * Mathf.Deg2Rad) || hitInfo.normal.y >= -Mathf.Sin(wallClimbAngle * Mathf.Deg2Rad))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ClimbWall(transform.forward);
                }
            }
        }
    }

    void Crouch()
    {
        playerCtrl.height = reducedHeight;
        groundCheck.localPosition = new Vector3(groundCheck.localPosition.x, grndCheckPos.y + (originalHeight - reducedHeight)/2, groundCheck.localPosition.z);
    } 

    void GoUp()
    {
        playerCtrl.height = originalHeight;
        groundCheck.localPosition = new Vector3(groundCheck.localPosition.x, grndCheckPos.y, groundCheck.localPosition.z);

    }

    void ClimbWall(Vector3 rayDirection)
    {
        StartCoroutine(Climb(wallClimbTime, rayDirection));
    }

    IEnumerator Climb(float time, Vector3 rayDirection) //
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + new Vector3(0, wallClimbHeight, 0);


        float timer = 0;
        while(timer < time)
        {
            velocity = Vector3.zero;
            transform.position = Vector3.Lerp(startPos, endPos, WallClimbSpeed(timer / time));


            if((endPos - startPos) == new Vector3(0, wallClimbHeight, 0))
            {
                Ray ray = new Ray(Camera.main.transform.position, rayDirection + Vector3.down);
                RaycastHit hitInfo;
                if(Physics.Raycast(ray, out hitInfo, wallClimbDist * 2))
                {
                    if(hitInfo.normal.y >= Mathf.Sin((90-wallClimbAngle) * Mathf.Deg2Rad))
                    {
                        startPos = transform.position;
                        endPos = hitInfo.point + Vector3.up * originalHeight / 2;
                        endPos += rayDirection * 0.15f;
                        timer = 0;
                        time *= crestFactor;
                    }
                }
            }

            timer += Time.deltaTime;
            yield return null;
        }
    }

    // Lite olika ekvationer som beskriver hastigheten för väggklättrande, thx till mattekursen. Lägg in i geogebra för att se hur hastigheten ser ut grafiskt.
    float WallClimbSpeed(float x)
    {
        //return -(x - 1) * (x - 1) + 1;
        //return Mathf.Sqrt(1 - (x - 1) * (x - 1));
        return -(x - 1) * (x - 1) * (x - 1) * (x - 1) + 1;
    }
}
