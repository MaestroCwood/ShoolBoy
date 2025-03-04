using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatBarLookAt : MonoBehaviour
{
   [SerializeField] private Transform player; // ������ �� ������

    private void Start()
    {
        // ����� ������ �� ���� "Player"
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player object with tag 'Player' not found.");
        }
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            // ������������ ������� �������� � ������
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = lookRotation;
        }
    }
}
