using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObject : MonoBehaviour
{

    private Rigidbody rb;

    public void Initialize(float delay)
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            // ��������� ����� �������������� ����� �������� �����
            Invoke(nameof(EnableKinematic), delay);
        }
    }

    private void EnableKinematic()
    {
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // ������� ���� ������ ����� ����������
        Destroy(this);
    }

}
