using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour

{
    // Start is called before the first frame update
    void Start()
    {

    }

    public GameObject lLight;
    internal float shadowStrength;
    internal float shadowBias;
    internal float intensity;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            lLight.SetActive(false);
        }
        else
        {
            lLight.SetActive(true);
        }
    }
}
