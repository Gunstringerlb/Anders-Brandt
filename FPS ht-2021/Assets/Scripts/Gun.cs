
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float firerate = 10f;
    public float knockback = 10f;
    private float nextTimeTofire = 0f;
    // public int maxAmmo = 30; (tanke för ett magasin sqript)
    // public float reloadTime = 1f; (tanke för ett magasin sqript)
    // private int currentAmmo; (tanke för ett magasin sqript)



    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    [SerializeField] private LayerMask enemyLayers;

   void Start()
    {
        // currentAmmo = maxAmmo; (tanke för ett magasin sqript)
    }

    // Denna funktionen ger mitt vapen automatiserad avfyrning.
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeTofire)
        {
            nextTimeTofire = Time.time + 1f / firerate;
            Shoot();
        }
    }

   // Denna funktionen är ansvarig för mitt vapens skjutning. Det innefattar skottens riktning och effekt på objekt samt vapnets muzzleflash när det avfyras.
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
            } // Minskar fiendens liv

            

            Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 force = ray.direction * knockback;

                rb.AddForce(force);
            } //Knockback

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); //
            Destroy(impactGO, 2f); 

            

        }
    }
}
