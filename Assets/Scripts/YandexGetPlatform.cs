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
        // ����� ������������� ��������� ���������
        CheckPlatform();
    }

    public void CheckPlatform()
    {
        // �������� ������ � ���������
         platform = YandexGame.EnvironmentData.deviceType.ToString();

        Debug.Log($"���������: {platform}");

        // ���������, ���� ���� �������� �� ��������, ��������� Canvas
        if (platform == "desktop")
        {
            
            
            
        } else
        {

           

        }
    }

    private void OnDestroy()
    {
        // ������������ �� �������, ����� �������� ������
        YandexGame.GetDataEvent -= OnSDKInitialized;
    }
}
