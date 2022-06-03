using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardFX : MonoBehaviour
{


    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(
            transform.position - Camera.main.transform.position, Vector3.up
        );
    }
}
