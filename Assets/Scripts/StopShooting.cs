
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
        if (weapon != null) // ��������, ��� weapon �� null
        {
            // ���� ��������� �������, ��������� ������, ����� ��������
            weapon.enabled = setingsField.activeSelf ? false : true;
        }
    }
}
