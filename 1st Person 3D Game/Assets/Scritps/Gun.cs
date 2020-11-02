
using UnityEngine;

public class Gun : MonoBehaviour
{
   public float damage = 10f;
   public float range = 100f;
    public Animator animator;
    public float fireRate = 100f;

    public ParticleSystem muzzleFlash;

    public Camera fpsCam;
    private float nextTimeToFire = 0f;
    // Update is called once per frame
    void Update()
    {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            animator.SetTrigger("Shoot");
            Shoot();
            
        }
    }

    void Shoot()
    {
        
        muzzleFlash.Play();
        
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);

            }

            
        }
    }

    
}
