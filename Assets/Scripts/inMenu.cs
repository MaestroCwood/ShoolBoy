using UnityEngine.SceneManagement;
using UnityEngine;

public class inMenu : MonoBehaviour
{

  public PausedAndaREsume pausedAndaREsume;
  public void InExitMenu()

    {
        if (pausedAndaREsume !=null)
                pausedAndaREsume.ResumeGame();
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
