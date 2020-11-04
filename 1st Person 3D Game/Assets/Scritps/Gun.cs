
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
   public float damage = 10f;
   public float range = 100f;
    public Animator animator;
    public Animator animator2;
    public Text AmmoAmzeige;

    public int maxAmmo = 10;
    private int currentAmo;
    
    private bool isRelading = false;
    
    public float fireRate = 100f;

    public ParticleSystem muzzleFlash;
    public GameObject ImpactHit;

  
    

    public Camera fpsCam;
    private float nextTimeToFire = 0f;


    private void Start()
    {
        currentAmo = maxAmmo;
        
    }


    // Update is called once per frame

    void Update()
    {
    
        AmmoAmzeige.text = currentAmo + "/10";

        if (isRelading)
            return;
        
       

        if(currentAmo <= 0 || Input.GetKeyDown(KeyCode.R) && currentAmo != maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            
            nextTimeToFire = Time.time + 1f / fireRate;
            animator.SetTrigger("Shoot");
            animator2.SetTrigger("isShot");
            
            Shoot();
            
        }

        
         
    }


    IEnumerator Reload()
    {

        isRelading = true;
        yield return new WaitForSeconds(.7f);
        animator.SetTrigger("Reload");
        FindObjectOfType<AudioManager>().Play("Reload");
        yield return new WaitForSeconds(1.2f);
        
        currentAmo = maxAmmo;
        isRelading = false;
    }

    void Shoot()
    {

        currentAmo--;
        
        muzzleFlash.Play();
        FindObjectOfType<AudioManager>().Play("Laser13");
        
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);

            }

            GameObject impactGO = Instantiate(ImpactHit, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
            
        }
    }



   
  

    
}
