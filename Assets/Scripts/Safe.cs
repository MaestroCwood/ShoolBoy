using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Safe : MonoBehaviour
{
    public bool isHasKey = false;
    public bool isOpenSafe = false;
    bool playSound = false;
    public Animator animator;
    public TextMeshProUGUI textSafe;
    public Collider[] colliders;
    [SerializeField]PlayerAudioControl playerAudioControl;

  
    public void OpenSafe()
    {
        if (isHasKey)
        {
           
            

        } 
         
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (isHasKey)
            {
                animator.SetTrigger("isOpen");
                textSafe.enabled = false;
                isOpenSafe = true;
                foreach (var col in colliders)
                {
                    col.enabled = false;
                }

            } else if(!isHasKey && !playSound)
            {   
                playSound = true;
                textSafe.enabled = true;
                playerAudioControl.PlayGolosPlayer(4);
                Invoke("SoundDelelay", 5f);
            }
                

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            

        }
    }

    void SoundDelelay()
    {
        textSafe.enabled = false;
        playSound = false;
    }
}
