using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenegerEnemy : MonoBehaviour
{
    private SaveSystem saveSystem; // Ссылка на SaveSystem
   
    public GameData gameData = new GameData();
    public Button continueButton;
    private List<string> killedEnemies = new List<string>(); // Список уничтоженных врагов

    private void Start()
    {
        saveSystem = FindObjectOfType<SaveSystem>(); // Находим SaveSystem в сцене
       
        // Загружаем сохраненные данные, если они есть
        var loadedData = saveSystem.LoadProgress();
        killedEnemies = new List<string>(loadedData.killedEnemies); // Сохраняем список убитых врагов локально
        if (PlayerPrefs.GetInt("NewGameStarted", 0) == 1)
        {
            // Если новая игра начата, скрываем меню
            
          
        }
        // Если сохранённые данные есть, продолжаем с ними
        if (loadedData.killedEnemies.Count > 0)
        {
            if (continueButton != null)
                continueButton.interactable = true;
            // Перебираем всех врагов на сцене
            foreach (DamageEnemyInBullet enemy in FindObjectsOfType<DamageEnemyInBullet>())
            {
                // Если враг был уничтожен ранее, уничтожаем его в сцене
                if (loadedData.killedEnemies.Contains(enemy.enemyID))
                {
                    Destroy(enemy.gameObject);
                }
            }
        }
        else
        {
            // Если сохранений нет, начинаем новую игру
            Debug.Log("Нет сохранений, начинаем новую игру.");
            if (continueButton != null)
                continueButton.interactable = false;
        }
       

    }

    // Метод для обновления списка убитых врагов и сохранения прогресса
    public void EnemyKilled(string enemyID)
    {
        if (!killedEnemies.Contains(enemyID)) // Проверяем, нет ли ID врага в списке
        {
            killedEnemies.Add(enemyID); // Добавляем ID врага в список
            saveSystem.SaveProgress(killedEnemies); // Сохраняем прогресс
        }
    }

    // Метод для очистки всех сохраненных данных
    public void ClearSavedProgress()
    {
        saveSystem.ResetProgress(); // Очищаем сохранения
        gameData.killedEnemies.Clear(); // Очищаем данные в игре
        Debug.Log("Сохранения и данные очищены.");
    }

    // Метод для начала новой игры (нажата кнопка "Новая игра")
    public void StartNewGame()
    {
        PlayerPrefs.DeleteKey("PlayerPosition");
        PlayerPrefs.SetInt("NewGameStarted", 1); // Устанавливаем флаг, что началась новая игра
        PlayerPrefs.Save(); // Сохраняем изменения в PlayerPrefs
        ClearSavedProgress(); // Очищаем сохраненные данные
        gameData.killedEnemies.Clear(); // Очищаем локальные данные
      
       

        // Перезагружаем сцену, чтобы сбросить все объекты
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Метод для продолжения игры (нажата кнопка "Продолжить")
    public void ContinueGame()
    {
       
        Debug.Log("Продолжаем игру.");
        // Здесь будет логика для продолжения игры с текущим сохранением
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
     

    }

    [System.Serializable]
    public class GameData
    {
        public List<string> killedEnemies = new List<string>();
    }
}
