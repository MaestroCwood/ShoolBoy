using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParolSafe : MonoBehaviour
{
    public Transform playerTransform;
    public Safe safe;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            
            safe.isHasKey = true;
            
            Destroy(gameObject);
        }
    }
}
