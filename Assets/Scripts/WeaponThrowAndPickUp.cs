using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponThrowAndPickUp : MonoBehaviour
{
    public GameObject weapon;  // Ссылка на объект оружия
    public float throwForce = 10f; // Сила выброса оружия
    public float pickupRange = 7f; // Радиус, в котором можно подобрать оружие
    public Transform holdPosition; // Позиция, где оружие будет держаться в руках (например, у камеры)
    public TextMeshProUGUI countBulletText;
   

    public AudioSource audiSource;
    public AudioClip[] audioClips;

    public event System.Action OnWeaponPickedUp;

    private Rigidbody weaponRb;  // Rigidbody оружия
    public bool isHoldingWeapon = false;  // Флаг, если оружие в руках
   
    
    public RaycastHit hit;
    public GameObject outPosRacast;
    public float RangeRaycasy;

    private void Start()
    {
        weaponRb = weapon.GetComponent<Rigidbody>();
        bool isWeaponPickedUp = PlayerPrefs.GetInt("IsWeaponPickedUp", 0) == 1;

        if (isWeaponPickedUp)
        {
            PickUpWeapon(); // Если оружие у игрока, переносим его в руки
        }
        else
        {
            weapon.SetActive(true); // Если оружие на земле, оно активно
        }

    }

    private void Update()
    {
        RaycastForWeaponPickup();
    }

   


    // Метод для выброса оружия
    /* private void ThrowWeapon()
     {
         isHoldingWeapon = false;  // Оружие больше не в руках
         weaponRb.isKinematic = false;  // Включаем физику для оружия
         weaponRb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);  // Выбрасываем оружие вперед
         weapon.transform.SetParent(null);
         countBulletText.enabled = false;
     }*/

    // Метод райкаст игрока
    private void RaycastForWeaponPickup()
    {
        Vector3 rayOrigin = outPosRacast.transform.position; 
        Vector3 rayDirection = transform.forward;


        if (Physics.Raycast(rayOrigin, rayDirection, out hit, pickupRange))
        {
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {   if (isHoldingWeapon) return;
            PickUpWeapon();
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 rayOrigin = outPosRacast.transform.position;
        Vector3 rayDirection = transform.forward;
        
        Gizmos.DrawLine(rayOrigin, rayOrigin + rayDirection * RangeRaycasy);

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, RangeRaycasy))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(hit.point, 0.2f);
        }
    }

    // Метод для подбора оружия
    public void PickUpWeapon()
    {
            
            
            isHoldingWeapon = true;  // Оружие снова в руках
            weaponRb.isKinematic = true;  // Отключаем физику
            weapon.transform.SetParent(holdPosition);  // Привязываем оружие обратно к руке игрока
            weapon.transform.localPosition = Vector3.zero;  // Устанавливаем позицию оружия относительно игрока
            weapon.transform.localRotation = Quaternion.identity;
            countBulletText.enabled = true;
            TargetQustionText.SetTextTargetQustion("Нужно найти патроны");
            audiSource.PlayOneShot(audioClips[0]);
            OnWeaponPickedUp?.Invoke();
            // Сохраняем, что оружие теперь у игрока
            PlayerPrefs.SetInt("IsWeaponPickedUp", 1); // 1 - оружие у игрока
            PlayerPrefs.Save();  


    }

    
}
