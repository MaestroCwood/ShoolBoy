
using System.Collections.Generic;
using UnityEngine;

public class AddBullet : MonoBehaviour
{
    public Weapon weapon;
    public AudioClip[] audioClipAddBullet;
    AudioSource audioSourceAddBullet;
    public float destroyDelay = 1f;
    public int add = 10;
    private bool isCollected = false;
    [SerializeField] private string uniqueID;

    private void Start()
    {
        audioSourceAddBullet = GetComponent<AudioSource>();
        if (IsBulletCollected(uniqueID))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)

    {
        if(other.CompareTag("Player") && !isCollected)
        {   
            isCollected = true;
            weapon.countBullet += add;
            PlauSoundAdd(0);
            weapon.UpdateCountBullet();
            TargetQustionText.SetTextTargetQustion("Найти Игоря");
            
            MarkBulletAsCollected(uniqueID);
            Invoke(nameof(DestroyBullet), destroyDelay);
        }
        
    }

    private void DestroyBullet()
    {   
        if(gameObject != null)
            Destroy(gameObject);
        
    }

    public void PlauSoundAdd(int nameclip)
    {
    
        audioSourceAddBullet.PlayOneShot(audioClipAddBullet[nameclip]);
    }

    // Проверяет, собран ли объект по его ID
    private bool IsBulletCollected(string id)
    {
        string collectedBullets = PlayerPrefs.GetString("CollectedBullets", "");
        List<string> collectedList = new List<string>(collectedBullets.Split(';'));
        return collectedList.Contains(id);
    }

    private void MarkBulletAsCollected(string id)
    {
        string collectedBullets = PlayerPrefs.GetString("CollectedBullets", "");
        List<string> collectedList = new List<string>(collectedBullets.Split(';'));
        if (!collectedList.Contains(id))
        {
            collectedList.Add(id);
            PlayerPrefs.SetString("CollectedBullets", string.Join(";", collectedList));
            PlayerPrefs.Save();
        }
    }
}
