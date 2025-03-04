
using UnityEngine;

public class TrigerIgor1 : MonoBehaviour
{

    public PlayerAudioControl playerAudioControl;
    bool isPlaySound = false;
    bool isHasTrgiggered = false;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isPlaySound && !isHasTrgiggered)
        {
            isHasTrgiggered = true;
            playerAudioControl.PlayGolosPlayer(7);
        }
    }
}
