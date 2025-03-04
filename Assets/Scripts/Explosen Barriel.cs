using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosenBarriel : MonoBehaviour
{
    public float force;
    public float radius;
    public ParticleSystem explosenFx;
    [SerializeField] float raduisDamage = 3f;
    [SerializeField] Transform targetPlayer;
    public PlayerUIControl playerUIControl;
    public PlayerAudioControl playerAudioControl;
    public DinamiteControl dinamiteControl;
    bool playSound = false;
    bool colisionPlayer = false;
    public bool setUpDinamite = false;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Explosen()
    {
        Collider[] overLapedsCollider = Physics.OverlapSphere(transform.position, radius);
       
        for (int i = 0; i < overLapedsCollider.Length; i++)
        {
            Rigidbody rigidbody = overLapedsCollider[i].attachedRigidbody;

            if (rigidbody)
            {
                rigidbody.isKinematic = false;
                rigidbody.AddExplosionForce(force, transform.position, radius);
                CarObject resetScript = rigidbody.gameObject.AddComponent<CarObject>();
                resetScript.Initialize(3f);
                explosenFx.Play();
                audioSource.Play();
                Destroy(gameObject, 1f);
                PlayDamagePlayer();

            }
        }
       
    }

 

    public void PlayDamagePlayer()
    {
        float distance = Vector3.Distance(transform.position, targetPlayer.transform.position);
        if (distance <= raduisDamage)
            playerUIControl.DamagePlayer(10, transform.position);


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (colisionPlayer) return;
            if (!dinamiteControl.isHawPlayerDinamit && !playSound &&!setUpDinamite)
            {
                playSound = true;
                colisionPlayer = true;
                playerAudioControl.PlayGolosPlayer(5);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Invoke("playSoundDelay",5f);
        }
    }

  void playSoundDelay()
    {
        playSound = false;
        colisionPlayer = false;
    }
}
