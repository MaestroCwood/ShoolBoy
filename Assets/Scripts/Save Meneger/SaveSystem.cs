using UnityEngine;
using System.Collections.Generic;

public class SaveSystem : MonoBehaviour
{
    private const string KilledEnemiesKey = "KilledEnemies"; // ���� ��� ���������� ������

    // ����� ��� �������� ���������
    public GameData LoadProgress()
    {
        GameData data = new GameData();

        // ���������, ���� ���������� ����������
        if (PlayerPrefs.HasKey(KilledEnemiesKey))
        {
            // ��������� ����������� ������
            string savedEnemies = PlayerPrefs.GetString(KilledEnemiesKey);
            data = JsonUtility.FromJson<GameData>(savedEnemies); // ���������� GameData ��� ������������
        }

        return data;
    }

    // ����� ��� ���������� ���������
    public void SaveProgress(List<string> killedEnemies)
    {
        // ����������� ������ � GameData
        GameData data = new GameData();
        data.killedEnemies = killedEnemies;

        // ����������� ������ GameData � JSON ������
        string json = JsonUtility.ToJson(data);

        // ��������� ������ � PlayerPrefs
        PlayerPrefs.SetString(KilledEnemiesKey, json);
        PlayerPrefs.Save(); // ��������� ���������
        Debug.Log("�������� ��������.");
    }

    // ����� ��� ������� ����������
    public void ResetProgress()
    {
        // ������� ������ � ������ ������ �� PlayerPrefs
        PlayerPrefs.DeleteKey(KilledEnemiesKey);
        PlayerPrefs.Save(); // ��������� ���������
        Debug.Log("���������� �������.");
    }

    [System.Serializable] // ��� ������������ ������
    public class GameData
    {
        public List<string> killedEnemies = new List<string>(); // ������ ������ ������
    }
}
