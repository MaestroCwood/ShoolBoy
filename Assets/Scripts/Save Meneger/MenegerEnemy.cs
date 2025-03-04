using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenegerEnemy : MonoBehaviour
{
    private SaveSystem saveSystem; // ������ �� SaveSystem
   
    public GameData gameData = new GameData();
    public Button continueButton;
    private List<string> killedEnemies = new List<string>(); // ������ ������������ ������

    private void Start()
    {
        saveSystem = FindObjectOfType<SaveSystem>(); // ������� SaveSystem � �����
       
        // ��������� ����������� ������, ���� ��� ����
        var loadedData = saveSystem.LoadProgress();
        killedEnemies = new List<string>(loadedData.killedEnemies); // ��������� ������ ������ ������ ��������
        if (PlayerPrefs.GetInt("NewGameStarted", 0) == 1)
        {
            // ���� ����� ���� ������, �������� ����
            
          
        }
        // ���� ���������� ������ ����, ���������� � ����
        if (loadedData.killedEnemies.Count > 0)
        {
            if (continueButton != null)
                continueButton.interactable = true;
            // ���������� ���� ������ �� �����
            foreach (DamageEnemyInBullet enemy in FindObjectsOfType<DamageEnemyInBullet>())
            {
                // ���� ���� ��� ��������� �����, ���������� ��� � �����
                if (loadedData.killedEnemies.Contains(enemy.enemyID))
                {
                    Destroy(enemy.gameObject);
                }
            }
        }
        else
        {
            // ���� ���������� ���, �������� ����� ����
            Debug.Log("��� ����������, �������� ����� ����.");
            if (continueButton != null)
                continueButton.interactable = false;
        }
       

    }

    // ����� ��� ���������� ������ ������ ������ � ���������� ���������
    public void EnemyKilled(string enemyID)
    {
        if (!killedEnemies.Contains(enemyID)) // ���������, ��� �� ID ����� � ������
        {
            killedEnemies.Add(enemyID); // ��������� ID ����� � ������
            saveSystem.SaveProgress(killedEnemies); // ��������� ��������
        }
    }

    // ����� ��� ������� ���� ����������� ������
    public void ClearSavedProgress()
    {
        saveSystem.ResetProgress(); // ������� ����������
        gameData.killedEnemies.Clear(); // ������� ������ � ����
        Debug.Log("���������� � ������ �������.");
    }

    // ����� ��� ������ ����� ���� (������ ������ "����� ����")
    public void StartNewGame()
    {
        PlayerPrefs.DeleteKey("PlayerPosition");
        PlayerPrefs.SetInt("NewGameStarted", 1); // ������������� ����, ��� �������� ����� ����
        PlayerPrefs.Save(); // ��������� ��������� � PlayerPrefs
        ClearSavedProgress(); // ������� ����������� ������
        gameData.killedEnemies.Clear(); // ������� ��������� ������
      
       

        // ������������� �����, ����� �������� ��� �������
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ����� ��� ����������� ���� (������ ������ "����������")
    public void ContinueGame()
    {
       
        Debug.Log("���������� ����.");
        // ����� ����� ������ ��� ����������� ���� � ������� �����������
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
     

    }

    [System.Serializable]
    public class GameData
    {
        public List<string> killedEnemies = new List<string>();
    }
}
