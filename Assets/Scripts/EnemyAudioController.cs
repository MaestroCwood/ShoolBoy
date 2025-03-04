using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    public AudioClip[] audioClips;
    AudioSource audioSource;
    [SerializeField] float randomStartSound;
    [SerializeField] float randomEndSound;

    public PlayerUIControl playerUIControl;
    public ZombieController zombieController;

    private bool isPlayingSound = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();   
    }
    void Start()
    {
        
    }

   
    void Update()
    {
        if (playerUIControl.isDead) return;
        if (audioSource != null && !audioSource.isPlaying && !zombieController.isAttacking &&!isPlayingSound)
        {
            
            StartCoroutine(nameof(WaitAndPlaySound));
        }
    }

    public void PlayAudioZombie(int nameClips )
    {   
        if((nameClips >= 0 && nameClips < audioClips.Length)) 
        {
            audioSource.clip = audioClips[nameClips];
            audioSource.Play();
        }
        
    }

    IEnumerator PlayRandomSoun()
    {
        isPlayingSound = true;
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
        yield return new WaitForSeconds(Random.Range(randomStartSound, randomEndSound));

        isPlayingSound = false;

    }

    IEnumerator WaitAndPlaySound()
    {
        
        yield return new WaitForSeconds(5f);
        StartCoroutine(nameof(PlayRandomSoun));
    }



}
