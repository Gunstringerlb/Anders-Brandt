using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Interactable : MonoBehaviour
{

    [SerializeField] private float activationDistance = 1f;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject promptPrefab;
    [SerializeField] private Vector3 promptPosition = Vector3.up * 3.5f;
    private GameObject prompt;
    private Transform player;


    //public delegate void OnInteractHandler();
    //public event OnInteractHandler OnInteract;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        prompt = Instantiate(promptPrefab, transform.position + promptPosition, transform.rotation);

        prompt.transform.SetParent(transform);

        //this.OnInteract += Test;
    }


    void Test()
    {
        Debug.Log("Event fired!");
    }

    // Update is called once per frame
    void Update()
    {
        // Kollar avståndet mellan spak och spelare
        if((player.position - transform.position).sqrMagnitude <= activationDistance * activationDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //OnInteract?.Invoke();
                anim.SetBool(
                    "Interacted", 
                    !anim.GetBool("Interacted")
                );
            }

            //Display interaction prompt 
            prompt.SetActive(true);
        }
        else
        {
            prompt.SetActive(false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + promptPosition, 0.1f);
    }
}
