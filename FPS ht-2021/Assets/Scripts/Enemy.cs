using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 50f;
    public float health = 50f;
    public float respawnTime = 3f;
    public Vector3 respawnPos;
    public Quaternion respawnRot;

    // Här sätts min fiendes startposition för att den senare ska kunna respawna där ifall den dör
    private void Start()
    {
        health = maxHealth;

        respawnPos = transform.position;
        respawnRot = transform.rotation;
    }
    
    // Här säger jag till fienden att dö när hans liv är mindre än 0
    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    // Denna funktion förklarar vad som ska ske med fienden när den dör
    void Die ()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.enabled = false;

        Collider col = GetComponent<Collider>();
        col.enabled = false;

        health = maxHealth;

        StartCoroutine(Respawn());
    }

    //
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);

        transform.position = respawnPos;
        transform.rotation = respawnRot;

        Renderer renderer = GetComponent<Renderer>();
        renderer.enabled = true;

        Collider col = GetComponent<Collider>();
        col.enabled = true;
    }

}
