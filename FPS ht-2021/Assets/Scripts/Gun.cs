
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float firerate = 10f;
    public float knockback = 10f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    [SerializeField] private LayerMask enemyLayers;

    private float nextTimeTofire = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeTofire)
        {
            nextTimeTofire = Time.time + 1f / firerate;
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        Ray ray = new Ray(fpsCam.transform.position, fpsCam.transform.forward);

        if(Physics.Raycast(ray, out hit, range, enemyLayers))
        {
            muzzleFlash.Play();

            Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            

            Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 force = ray.direction * knockback;

                rb.AddForce(force);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);

            

        }
    }
}
