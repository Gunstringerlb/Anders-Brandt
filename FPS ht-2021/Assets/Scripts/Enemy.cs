using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 50f;
    public float health = 50f;
    public float respawnTime = 3f;
    public Vector3 respawnPos;
    public Quaternion respawnRot;

    // H�r s�tts min fiendes startposition f�r att den senare ska kunna respawna d�r ifall den d�r
    private void Start()
    {
        health = maxHealth;

        respawnPos = transform.position;
        respawnRot = transform.rotation;
    }
    
    // H�r s�ger jag till fienden att d� n�r hans liv �r mindre �n 0
    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    // Denna funktion f�rklarar vad som ska ske med fienden n�r den d�r
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
