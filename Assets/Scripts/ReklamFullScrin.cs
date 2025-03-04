
using System.Collections;
using TMPro;
using UnityEngine;
using YG;

public class ReklamFullScrin : MonoBehaviour
{
    public YandexGame _sdk;
    public TextMeshProUGUI textTimer;
    public GameObject timerPanel;
    public GameObject buttonContinius;
    bool isPlayningReklama = false;
    [SerializeField] Weapon weapon;

   // [SerializeField]float timeToStartTimer;
    //public PausedAndaREsume pausedAndaREsume;

    float timer = 2;


    private void Update()
    {
        if (YandexGame.timerShowAd >= YandexGame.Instance.infoYG.fullscreenAdInterval
                    && Time.timeScale != 0)
        {
            StartCoroutine(TimerPeredReklamoy());
        }
    }

    IEnumerator TimerPeredReklamoy()
    {


        
        //pausedAndaREsume.PauseGame();
        weapon.enabled = false;
        timerPanel.SetActive(true);
        isPlayningReklama = true;


        while (timer>=0.1)
        {
           
            textTimer.text = "Сейчас будет реклама:  " + timer.ToString();
            timer --;
            yield return new WaitForSecondsRealtime(1f);
        }
        
        _sdk._FullscreenShow();
       
        timer = 2;
        //StartCoroutine(StartTimerAfterDelay());
        timerPanel.SetActive(false);
        weapon.enabled = true;
     




    }



   // IEnumerator StartTimerAfterDelay()
    //{
       

       // yield return new WaitForSecondsRealtime(timeToStartTimer);
        //isPlayningReklama = false;
    //}

    public void TimeRusime()
    {
        Time.timeScale = 1f;
    }


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


}

