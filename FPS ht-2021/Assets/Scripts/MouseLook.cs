using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 320f;

    public Transform playerBody;

    float xRotation = 0f;

    public Animator anim;

    public GameObject canvas;

   
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }


    void Update() 
       
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // Hämtar värden som sen gör det möjligt för mig att kolla runt

        xRotation -= mouseY; //
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        AimDownSight();
       
    }

    // Denna funktionen är ansvarig för mitt vapens ADS funktion, den ändrar värden för att skapa en vy som efterliknar en siktande handling
    void AimDownSight()
    {
        if (Input.GetMouseButton(1))
        {
            Debug.Log("Aiming");
            anim.SetBool("ADS", true);
            mouseSensitivity = 160;
            canvas.SetActive(false);
        }
        else
        {
            Debug.Log("NotAiming");
            anim.SetBool("ADS", false);
            mouseSensitivity = 320;
            canvas.SetActive(true);
        }
    }
    

}
