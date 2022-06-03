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
    //public event OnInteractHandler OnInteract; (Försökte ge mig på lite event system men det kom inte riktigt nånstans)


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        prompt = Instantiate(promptPrefab, transform.position + promptPosition, transform.rotation);

        prompt.transform.SetParent(transform);
        // Definierar spelare och prompt så fort spelet startar
        
    }

    void Update()
    {
        // Kollar avståndet mellan spak och spelare
        if((player.position - transform.position).sqrMagnitude <= activationDistance * activationDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
               
                anim.SetBool(
                    "Interacted", 
                    !anim.GetBool("Interacted")
                );
            }

            
            prompt.SetActive(true);
        }
        else
        {
            prompt.SetActive(false);
        }
    }
    private void OnDrawGizmos() // Här gjorde jag en egen gizmo för interactable game objects
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationDistance); // En gul wiresphere för distansen då jag kan använda dörren
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + promptPosition, 0.1f); // En röd boll för själva prompten, alltså texten.
    }
}
