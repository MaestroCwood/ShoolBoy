using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedAndaREsume : MonoBehaviour
{

    public void PauseGame()
    {

        Time.timeScale = 0f;
        MuteAllAudio(true);    
    }

    public void ResumeGame()
    {

        Time.timeScale = 1f;
        MuteAllAudio(false);
    }

    void MuteAllAudio(bool mute)
    {
        AudioListener.pause = mute;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ResumeGame();

        }
    }
}
