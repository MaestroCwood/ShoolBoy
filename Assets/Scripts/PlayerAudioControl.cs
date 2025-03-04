using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioControl : MonoBehaviour
{
   
    AudioSource audioSourcePlayer;
    public  AudioClip[] audioClips;

    private void Start()
    {
        audioSourcePlayer = GetComponent<AudioSource>();
    }
    public void PlayGolosPlayer(int name)
    {
        audioSourcePlayer.PlayOneShot(audioClips[name]);
    }

    public void StartPlaySound()
    {
        audioSourcePlayer.PlayOneShot(audioClips[0]);
    }
}
