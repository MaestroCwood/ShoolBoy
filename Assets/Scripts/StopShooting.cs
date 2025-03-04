
using UnityEngine;

public class StopShooting : MonoBehaviour
{
    public Weapon weapon;
    public GameObject setingsField;
    void Start()
    {
        
    }

   
    void Update()
    {
        if (weapon != null) // Проверка, что weapon не null
        {
            // Если настройки активны, отключаем оружие, иначе включаем
            weapon.enabled = setingsField.activeSelf ? false : true;
        }
    }
}
