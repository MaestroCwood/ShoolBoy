
using UnityEngine;

public class TriggerPlayerStartSound : MonoBehaviour
{
    public PlayerAudioControl playerAudioControl;
    bool isPlayning = false;

    private void Start()
    {
        if(PlayerPrefs.GetInt("isPlayning") == 1)
        {
            isPlayning = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isPlayning)
        {
            isPlayning = true;
            playerAudioControl.StartPlaySound();
            PlayerPrefs.SetInt("isPlayning", 1);
            PlayerPrefs.Save();
        }
    }
}
