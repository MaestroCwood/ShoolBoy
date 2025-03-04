
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CarFinal : MonoBehaviour
{
    public VikaConroller vikaConroller;
    public IgorController igorController;
    public GameObject finalPanel;

    public void FinalGame()
    {
        if (vikaConroller.finals && igorController.isHasTriggered)
        {
            // Финальная логика потом придумать надо
            finalPanel.SetActive(true);
            StartCoroutine(FinalPanelActive());
            Cursor.lockState = CursorLockMode.None;
            Invoke("StopTimeGame", 3f);
         
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FinalGame();
           
        }
    }

    IEnumerator FinalPanelActive()
    {
        Image image = finalPanel.GetComponent<Image>();
        Color startColor = image.color; // Начальный цвет панели
        float targetAlpha = 0.5f; // Целевое значение альфа
        float duration = 1f; // Время перехода
        float elapsedTime = 0f; // Счётчик времени

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // Увеличиваем время
            float newAlpha = Mathf.Lerp(startColor.a, targetAlpha, elapsedTime / duration); // Линейная интерполяция альфа
            image.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha); // Устанавливаем новый цвет
            yield return null; // Ждём следующий кадр
        }

        image.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha); // Устанавливаем конечный цвет
    }

    void StopTimeGame()
    {
        Time.timeScale = 0f;
    }
}