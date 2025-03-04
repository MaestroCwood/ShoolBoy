using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoseGame : MonoBehaviour
{
    public GameObject loseGamePanel;
    public GameObject crosshair;
    public GameObject gun;
    PausedAndaREsume pausedAndaREsume;

    private void Awake()
    {
        pausedAndaREsume = GetComponent<PausedAndaREsume>();
    }

    public void ReloadScene()
    {
        int curretScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curretScene);
        TimeScaleResume();
        gun.SetActive(true);
    }

 

    public void LoseGameActivePanel()
    {
        loseGamePanel.SetActive(true);
        crosshair.SetActive(false);
        gun.SetActive(false);


        Image image = loseGamePanel.GetComponent<Image>();

        StartCoroutine(FadeInImage(image));
        Cursor.lockState = CursorLockMode.None;
        Invoke("TimeScalePause", 1f);
    }

    IEnumerator FadeInImage(Image image)
    {
        Color color = image.color;
        color.a = 0f;
        image.color = color;
        float duration = 1.0f; // ƒлительность анимации в секундах
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 0.3f, elapsedTime / duration); // Ћинейна€ интерпол€ци€ альфа-значени€
            image.color = new Color(color.r, color.g, color.b, alpha);
            yield return null; // ∆дем следующий кадр
           
        }

        image.color = new Color(color.r, color.g, color.b, 0.3f);
    }


    public void TimeScalePause()
    {
        pausedAndaREsume.PauseGame();
    }

    public void TimeScaleResume()
    {
        pausedAndaREsume.ResumeGame();
        crosshair.SetActive(true);
    }
}
