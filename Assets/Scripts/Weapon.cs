using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{

    public TextMeshProUGUI countBulletTExt;
    public WeaponThrowAndPickUp _weaponThrowAndPickUp;
    public int countBullet;
    public float damage = 21;
    float bulletSpeed = 25f;
    public float fireRate = 1;
    public float range = 15;
    public ParticleSystem muzleFlash;
    public Transform bulletPos;
    public AudioClip audioClipShoot;
    public AudioClip audioClipNoBullet;
    
    public AudioSource audioSource;
    public float forceRange;
    public GameObject hitEffect;
    public GameObject hitEffectOther;
    
    
    public Camera _camera;

    private float nextFire = 0;
    public YandexGetPlatform sdk;


    private void Start()
    {
        countBullet = PlayerPrefs.GetInt("countBullet");
        UpdateCountBullet();
    }

    void Update()
    {   

        if(sdk.platform == "desktop")
        {
            if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
            {
                nextFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
      
    }

     public void Shoot()
     {
         RaycastHit hit;
        if (!_weaponThrowAndPickUp.isHoldingWeapon) return;
        if (countBullet > 0)
        {
            muzleFlash.Play();
            SaveCountBullet();
            float randomPitch = Random.Range(0.9f, 1.1f);
            audioSource.pitch = randomPitch;
            audioSource.PlayOneShot(audioClipShoot);
           // Cursor.lockState = CursorLockMode.Locked;
            countBullet--;
            UpdateCountBullet();
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, range))
            {
                Debug.Log("piuu piu" + hit.collider);

                if (hit.collider.CompareTag("Enemy"))
                {

                    float timeToHit = hit.distance / bulletSpeed;
                    GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    DamageEnemyInBullet target = hit.transform.GetComponent<DamageEnemyInBullet>();
                    if (target != null)
                    {
                        target.TakeDamage(damage);
                    }

                    Destroy(impact, 3f);
                }
                if (!hit.collider.CompareTag("Enemy"))
                {
                    GameObject impactOther = Instantiate(hitEffectOther, hit.point, Quaternion.LookRotation(hit.normal));
                }

                if (hit.collider != null)
                {
                    //hit.rigidbody.AddForce(-hit.normal * forceRange);
                }
            }
        }
        else 
        {
            audioSource.PlayOneShot(audioClipNoBullet);
            countBulletTExt.color = Color.red;
        } 
         
     }

    public void UpdateCountBullet()
    {
        countBulletTExt.text = countBullet.ToString();
        countBulletTExt.color = Color.white;

    }

    public void SaveCountBullet()
    {
        PlayerPrefs.SetInt("countBullet", countBullet);
        PlayerPrefs.Save();
    }
   
}
