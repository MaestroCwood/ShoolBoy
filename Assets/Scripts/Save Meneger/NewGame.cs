using UnityEngine.SceneManagement;
using UnityEngine;

public class NewGame : MonoBehaviour
{
    public MenegerEnemy menegerEnemy;
    public GameData gameData;

    public void StartNewGame()
    {
        Time.timeScale = 1.0f;
        menegerEnemy.ClearSavedProgress();
        gameData.killedEnemies.Clear();
        PlayerPrefs.SetInt("isPlayning", 0);
        PlayerPrefs.DeleteKey("CollectedBullets");
        PlayerPrefs.SetInt("countBullet",0);
        PlayerPrefs.SetInt("IsWeaponPickedUp", 0); // Оружие не подбрано
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       

    }
}
