using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 direction;
   [SerializeField] float speedBullet;
    void Start()
    {
        direction = transform.forward;
    }

    
    void Update()
    {
        transform.position += direction * speedBullet * Time.deltaTime;
    }

    public float damage = 10f; // Урон от пули

    private void OnCollisionEnter(Collision collision)
    {
        // Пуля уничтожается при любом столкновении
        Destroy(gameObject,1f);
    }
}
