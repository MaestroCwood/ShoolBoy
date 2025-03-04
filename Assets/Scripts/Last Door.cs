
using UnityEngine;

public class LastDoor : MonoBehaviour
{
    public PlayerAudioControl playerAudioControl;
    public LastKeyControl lastKeyControl;
    AudioSource audioSource;
    

    bool playSound = false;
    bool isOpenDoor = false;
    bool triggeredPlayer = false;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (lastKeyControl.isHawKeyPlayer && !isOpenDoor)
            {
                animator.SetTrigger("isOpen");
                audioSource.Play();
                lastKeyControl.PickUpKeyToDoor();
                isOpenDoor = true;
            }
            else if (!lastKeyControl.isHawKeyPlayer && !playSound & !triggeredPlayer)
            {
                playSound = true;
                triggeredPlayer = true;
                playerAudioControl.PlayGolosPlayer(6);
                Invoke("OffTriggered", 10f);
            }
        } 

       
    }

    void OffTriggered ()
    {
        triggeredPlayer = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playSound = false;
        }
    }
}
