using System.Collections;
using System.Collections.Generic;
using MFPC.Camera;
using UnityEngine;
using UnityEngine.UI;
using MFPC.Input.PlayerInput;
using MFPC.Utils;
using MFPC;

public class PlayerUIControl : MonoBehaviour
{
   public Slider heatBarPlayer;
    public float heatPlayer;
    float maxHp;
    public bool isDead = false;
    [SerializeField] float distanceDamageToPlayer;
    public DamageEffect damageEffectl;
    [SerializeField] Image imageFxDamage;
    public AudioClip[] audioClips;
    AudioSource audioSource;
    public MFPCCameraRotation cameraRotation;
    public LoseGame loseGame;
   



    private void Awake()
    {
        maxHp = heatPlayer;
        heatBarPlayer.maxValue = maxHp;
        heatBarPlayer.value = heatPlayer;
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
       
    }

    public void DamagePlayer(int damage, Vector3 curentPos)

    {
        if (isDead) return;
     
            if (heatPlayer > 0)
            {
                PlaySoundFxPlayer(0);

                 DamageFxPlayer();
                heatPlayer -= damage;
                heatBarPlayer.value = heatPlayer;
            }

       




        if (heatPlayer <= 0)
        {
          
            PlaySoundFxPlayer(1);
            PlayerDeath();
            loseGame.LoseGameActivePanel();
        }
        
    }

    public void PlaySoundFxPlayer(int nameSound)
    {
        
        audioSource.PlayOneShot(audioClips[nameSound]);
    }

    void PlayerDeath()
    {
        isDead = true;
        damageEffectl.TriggerDamageEffect();
        GetComponent<CharacterController>().enabled = false;
        Weapon weapon = GetComponentInChildren<Weapon>();
        if (weapon != null)
            weapon.enabled = false;
        cameraRotation.enabled = false;
        GetComponent<Player>().enabled =false;
        // Запускаем анимацию падения
        StartCoroutine(PlayerFall());
       
    }

   

   

    IEnumerator PlayerFall()
    {
        float duration = 1.5f; // Длительность падения
        float elapsedTime = 0;

        // Пример наклона игрока
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;

        // После падения показываем экран проигрыша
        //ShowGameOverScreen();
    }


    public void DamageFxPlayer()
    {
        StartCoroutine(FxDamageRed());
    }

    IEnumerator FxDamageRed()
    {
        imageFxDamage.color = new Color(1, 0, 0, 1);
        float speedFade = 0.5f;
        while (imageFxDamage.color.a > 0)
        {
            Color currentColor = imageFxDamage.color;
            currentColor.a -= speedFade * Time.deltaTime; 
            imageFxDamage.color = currentColor;
            yield return null; 
        }
        imageFxDamage.color = new Color(1, 0, 0, 0);
    }
}
