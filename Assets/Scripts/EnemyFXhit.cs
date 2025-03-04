using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyFXhit : MonoBehaviour
{
    public GameObject hitFx;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            //Instantiate(hitFx, transform.position, Quaternion.identity);
        }
    }


}
