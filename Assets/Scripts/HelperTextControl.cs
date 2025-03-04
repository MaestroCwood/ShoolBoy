using TMPro;
using UnityEngine;

public class HelperTextControl : MonoBehaviour
{
    public WeaponThrowAndPickUp weaponThrowAndPickUp;
    public TextMeshProUGUI needGunText;

    private void Start()
    {
        needGunText.enabled = true; // Показывать текст изначально
    }

    private void OnEnable()
    {
        weaponThrowAndPickUp.OnWeaponPickedUp += HideNeedGunText;
    }

    private void OnDisable()
    {
        weaponThrowAndPickUp.OnWeaponPickedUp -= HideNeedGunText;
    }

    private void HideNeedGunText()
    {
        needGunText.enabled = false;
    }
}
