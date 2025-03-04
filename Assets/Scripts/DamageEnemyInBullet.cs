
using UnityEngine;

public class DamageEnemyInBullet : MonoBehaviour
{

    public float health; // �������� �����
    float maxHeat;
    public AudioClip hitSound;
    public AudioClip deathSound;// ���� ���������
    public GameObject hitEffect; // ������ �����
    private AudioSource audioSource;
    private Animator animator;
    private Transform heatBarEnemy;
    [SerializeField]GameObject healthBarObject;
    public string enemyID;
    [SerializeField]
    MenegerEnemy menegerEnemy;
    SaveSystem saveSystem;




    public bool isDeathEnemy = false;

    private void Awake()
    {
        maxHeat = health;
    }

    private void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        animator = GetComponentInChildren<Animator>();

        if (FindObjectOfType<MenegerEnemy>() == null)
        {
            Debug.LogError("EnemyManager не найден на сцене!");
            return;
        }

        if (healthBarObject != null)
        {
            heatBarEnemy = healthBarObject.transform;

            SetHeatBarScaleEnemy();
        }
        else Debug.Log("heatBarEnemy ne nauden");

       
    }

    public void TakeDamage(float damage)
    {
        if (!isDeathEnemy)
        {
            if (health > 0)

            {
                health -= damage;
               
                SetHeatBarScaleEnemy();
                //animator.SetTrigger("isDamage");
                if (hitSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(hitSound);
                }
            }
        }
        

        if (health <= 0f)
        {
            GetComponent<Collider>().enabled = false;
            Destroy(healthBarObject);
            isDeathEnemy = true;         
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttack", false);
            animator.SetBool("isDeadZombie", true);
            audioSource.PlayOneShot(deathSound);
            Invoke("Die", 4f);
            menegerEnemy.EnemyKilled(enemyID);
            
            Debug.Log(isDeathEnemy);

        }

    }

    private void Die()
    {
        
        Debug.Log("Enemy is dead!");
        Destroy(gameObject);
    }

    void SetHeatBarScaleEnemy()
    {
        float healthPercentage = health / maxHeat;
        heatBarEnemy.localScale = new Vector3(healthPercentage, heatBarEnemy.localScale.y, heatBarEnemy.localScale.z);
    }

    
}
