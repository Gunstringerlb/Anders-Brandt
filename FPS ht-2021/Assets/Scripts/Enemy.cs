using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 50f;
    public float health = 50f;
    public float respawnTime = 3f;
    public Vector3 respawnPos;
    public Quaternion respawnRot;

    private void Start()
    {
        health = maxHealth;

        respawnPos = transform.position;
        respawnRot = transform.rotation;
    }

    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die ()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.enabled = false;

        Collider col = GetComponent<Collider>();
        col.enabled = false;

        health = maxHealth;

        StartCoroutine(Respawn());
    }

    //gör det möjlig att ha en timer i bakgrunden  
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
