using UnityEngine;
using YG;

public class YandexGetPlatform : MonoBehaviour
{
    
    public string platform;

    private void Start()
    {
        
        if (YandexGame.SDKEnabled)
        {
            CheckPlatform();
        }
        else
        {
            
            YandexGame.GetDataEvent += OnSDKInitialized;
        }
    }

    private void OnSDKInitialized()
    {
        // После инициализации проверяем платформу
        CheckPlatform();
    }

    public void CheckPlatform()
    {
        // Получаем данные о платформе
         platform = YandexGame.EnvironmentData.deviceType.ToString();

        Debug.Log($"Платформа: {platform}");

        // Проверяем, если игра запущена на десктопе, отключаем Canvas
        if (platform == "desktop")
        {
            
            
            
        } else
        {

           

        }
    }

    private void OnDestroy()
    {
        // Отписываемся от события, чтобы избежать ошибок
        YandexGame.GetDataEvent -= OnSDKInitialized;
    }
}
