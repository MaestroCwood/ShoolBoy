using UnityEngine;
using System.Collections.Generic;

public class SaveSystem : MonoBehaviour
{
    private const string KilledEnemiesKey = "KilledEnemies"; // Ключ для сохранения данных

    // Метод для загрузки прогресса
    public GameData LoadProgress()
    {
        GameData data = new GameData();

        // Проверяем, если сохранение существует
        if (PlayerPrefs.HasKey(KilledEnemiesKey))
        {
            // Загружаем сохраненные данные
            string savedEnemies = PlayerPrefs.GetString(KilledEnemiesKey);
            data = JsonUtility.FromJson<GameData>(savedEnemies); // Используем GameData для сериализации
        }

        return data;
    }

    // Метод для сохранения прогресса
    public void SaveProgress(List<string> killedEnemies)
    {
        // Оборачиваем список в GameData
        GameData data = new GameData();
        data.killedEnemies = killedEnemies;

        // Преобразуем объект GameData в JSON строку
        string json = JsonUtility.ToJson(data);

        // Сохраняем строку в PlayerPrefs
        PlayerPrefs.SetString(KilledEnemiesKey, json);
        PlayerPrefs.Save(); // Сохраняем изменения
        Debug.Log("Прогресс сохранен.");
    }

    // Метод для очистки сохранений
    public void ResetProgress()
    {
        // Удаляем данные о убитых врагах из PlayerPrefs
        PlayerPrefs.DeleteKey(KilledEnemiesKey);
        PlayerPrefs.Save(); // Сохраняем изменения
        Debug.Log("Сохранения очищены.");
    }

    [System.Serializable] // Для сериализации данных
    public class GameData
    {
        public List<string> killedEnemies = new List<string>(); // Список убитых врагов
    }
}
