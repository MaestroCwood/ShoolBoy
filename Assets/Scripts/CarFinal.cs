
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
            // ��������� ������ ����� ��������� ����
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
        Color startColor = image.color; // ��������� ���� ������
        float targetAlpha = 0.5f; // ������� �������� �����
        float duration = 1f; // ����� ��������
        float elapsedTime = 0f; // ������� �������

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // ����������� �����
            float newAlpha = Mathf.Lerp(startColor.a, targetAlpha, elapsedTime / duration); // �������� ������������ �����
            image.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha); // ������������� ����� ����
            yield return null; // ��� ��������� ����
        }

        image.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha); // ������������� �������� ����
    }

    void StopTimeGame()
    {
        Time.timeScale = 0f;
    }
}