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
    //public event OnInteractHandler OnInteract; (F�rs�kte ge mig p� lite event system men det kom inte riktigt n�nstans)


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        prompt = Instantiate(promptPrefab, transform.position + promptPosition, transform.rotation);

        prompt.transform.SetParent(transform);
        // Definierar spelare och prompt s� fort spelet startar
        
    }

    void Update()
    {
        // Kollar avst�ndet mellan spak och spelare
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
    private void OnDrawGizmos() // H�r gjorde jag en egen gizmo f�r interactable game objects
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationDistance); // En gul wiresphere f�r distansen d� jag kan anv�nda d�rren
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + promptPosition, 0.1f); // En r�d boll f�r sj�lva prompten, allts� texten.
    }
}
