using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
    public Image damageImage; // Ссылка на Image
    public float fadeSpeed = 2f; // Скорость исчезновения эффекта
    private bool isDamaged = false;

    private void Update()
    {
        if (isDamaged)
        {
            // Постепенно уменьшаем прозрачность
            damageImage.color = Color.Lerp(damageImage.color, new Color(1, 0, 0, 0.3f), fadeSpeed * Time.deltaTime);

            // Если эффект полностью исчез, выключаем
            if (damageImage.color.a >= 0.3f)
            {
                damageImage.color = new Color(1, 0, 0, 0.3f); 
                isDamaged = false;
            }
        }
    }

    public void TriggerDamageEffect()
    {
        
        damageImage.color = new Color(1, 0, 0, 0f); 
        isDamaged = true;
    }
}
