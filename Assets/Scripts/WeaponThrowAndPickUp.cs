using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponThrowAndPickUp : MonoBehaviour
{
    public GameObject weapon;  // ������ �� ������ ������
    public float throwForce = 10f; // ���� ������� ������
    public float pickupRange = 7f; // ������, � ������� ����� ��������� ������
    public Transform holdPosition; // �������, ��� ������ ����� ��������� � ����� (��������, � ������)
    public TextMeshProUGUI countBulletText;
   

    public AudioSource audiSource;
    public AudioClip[] audioClips;

    public event System.Action OnWeaponPickedUp;

    private Rigidbody weaponRb;  // Rigidbody ������
    public bool isHoldingWeapon = false;  // ����, ���� ������ � �����
   
    
    public RaycastHit hit;
    public GameObject outPosRacast;
    public float RangeRaycasy;

    private void Start()
    {
        weaponRb = weapon.GetComponent<Rigidbody>();
        bool isWeaponPickedUp = PlayerPrefs.GetInt("IsWeaponPickedUp", 0) == 1;

        if (isWeaponPickedUp)
        {
            PickUpWeapon(); // ���� ������ � ������, ��������� ��� � ����
        }
        else
        {
            weapon.SetActive(true); // ���� ������ �� �����, ��� �������
        }

    }

    private void Update()
    {
        RaycastForWeaponPickup();
    }

   


    // ����� ��� ������� ������
    /* private void ThrowWeapon()
     {
         isHoldingWeapon = false;  // ������ ������ �� � �����
         weaponRb.isKinematic = false;  // �������� ������ ��� ������
         weaponRb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);  // ����������� ������ ������
         weapon.transform.SetParent(null);
         countBulletText.enabled = false;
     }*/

    // ����� ������� ������
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

    // ����� ��� ������� ������
    public void PickUpWeapon()
    {
            
            
            isHoldingWeapon = true;  // ������ ����� � �����
            weaponRb.isKinematic = true;  // ��������� ������
            weapon.transform.SetParent(holdPosition);  // ����������� ������ ������� � ���� ������
            weapon.transform.localPosition = Vector3.zero;  // ������������� ������� ������ ������������ ������
            weapon.transform.localRotation = Quaternion.identity;
            countBulletText.enabled = true;
            TargetQustionText.SetTextTargetQustion("����� ����� �������");
            audiSource.PlayOneShot(audioClips[0]);
            OnWeaponPickedUp?.Invoke();
            // ���������, ��� ������ ������ � ������
            PlayerPrefs.SetInt("IsWeaponPickedUp", 1); // 1 - ������ � ������
            PlayerPrefs.Save();  


    }

    
}
