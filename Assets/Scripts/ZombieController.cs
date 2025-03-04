using System;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    NavMeshAgent agent;
    AudioSource audioSource;
    public AudioClip[] audioClips;
    public PlayerUIControl playerUIControl;
    public Transform targetPlayer; // �����
    public bool isHasTarget = false;
    Animator animator;
    [SerializeField] float detectionRadius = 10f; // ������ ����������� ������
    [SerializeField] float stoppingDistance = 3f; // ���������� ��� �����
    [SerializeField] GameObject outPosRaycast; // ����� ��� Raycast (���� �����)
    public bool isChasing = false; // ���������� �� ����� ������
    public bool isAttacking = false; // ������� �� �����
    [SerializeField] float patrolWaitTime = 2f; // ����� �������� �� �����
    float patrolTimer = 0f;
    DamageEnemyInBullet damageEnemyInBullet;


    [SerializeField] int patrolPointCount = 4; // ���������� ����� ��������������
    [SerializeField] float patrolRadius = 10f; // ������, � ������� ������������ ����� ��������������
    Vector3[] patrolPoints; // ������ ��������������� ����� ��������������
    int currentPatrolIndex = 0; // ������� ����� ��������������
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        damageEnemyInBullet = GetComponent<DamageEnemyInBullet>();

    }
    void Start()
    {
        GeneratePatrolPoints(); // ���������� ����� ��������������
         // �������� ��������������
        
        agent.updateRotation = true;
        if (!isHasTarget) Patrol();
        
        if (targetPlayer == null)
        {
            
        }
        Patrol();

        if (targetPlayer == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                targetPlayer = playerObject.transform;
            }
        }
    }

    void GeneratePatrolPoints()
    {
        patrolPoints = new Vector3[patrolPointCount];
        for (int i = 0; i < patrolPointCount; i++)
        {
            // ���������� ��������� ����� � ������� patrolRadius
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * patrolRadius;
            randomDirection.y = 0; // ������� �������� �� ��� Y, ����� ����� ���������� �� �����
            patrolPoints[i] = transform.position + randomDirection;

            // ���������, ����� ����� ���� ��������� ��� NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(patrolPoints[i], out hit, patrolRadius, NavMesh.AllAreas))
            {
                patrolPoints[i] = hit.position; // ��������� �����, ������� ����� �� NavMesh
                Debug.Log($"Patrol Point {i}: {hit.position}");
            }
            else
            {
                patrolPoints[i] = transform.position; // ���� ����� ����������, ��������� � �� ����� �����
                Debug.LogWarning($"Failed to find NavMesh position for point {i}");
            }
        }
    }

    void Update()
    {
        if (targetPlayer == null) return;
        if (playerUIControl.isDead)
        {
            IsDeadPlayer();
        }
        if (damageEnemyInBullet.isDeathEnemy)
        {
            agent.isStopped = true;
            agent.ResetPath();
            return;
        }
            
        float distance = Vector3.Distance(targetPlayer.position, transform.position);

        // ���� ����� � ������� �����������
        if (distance <= detectionRadius)
        {
            ChasePlayer(distance);
           
        }
        else
        {
            StopChasing();
            isHasTarget = false;
        }

        if (!isHasTarget || playerUIControl.isDead)
        {
            Patrol(); // ���� ��� ���� ��� ����� ����, �����������
            
            return;
        }
    }

    void ChasePlayer(float distance)
    {
        // ���������, ���� ����� ����
        if (playerUIControl.isDead)
        {
           
            if (!animator.GetBool("isDeadPlayer")) // ������������� �������� ������ ���� ���
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttack", false);
                animator.SetBool("isDeadPlayer", true);
                agent.ResetPath(); // ������������� �������� ������
            }
            return; // ������� �� ������, ����� �� ��������� ������ ��������
        }

        // ���� ����� ��� ���, ���������� ������������
        if (!isChasing)
        {
            isChasing = true;
            isHasTarget = true;
            animator.SetBool("idle", false);
            animator.SetBool("isAttack", false);
            animator.SetBool("isWalking", true);
        }

        agent.SetDestination(targetPlayer.position);

        // ���� ����� � �������� ���������� ��� �����
        if (distance <= stoppingDistance)
        {
            StartAttack();
        }
        else
        {
            StopAttack();
        }
    }

    void StartAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            isHasTarget = true;
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttack", true);
            agent.ResetPath(); // ���������� �������� ������
            

        }
    }

    void StopAttack()
    {
        if (isAttacking)
        {
            isAttacking = false;
            animator.SetBool("isAttack", false);
            animator.SetBool("isWalking", true);
            agent.SetDestination(targetPlayer.position); // ����������� �������� � ������
        }
    }

    void StopChasing()
    {
        if (isChasing)
        {
            isChasing = false;
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttack", false);
            agent.ResetPath(); // �������� ���� ������
            isHasTarget = false;
        }
    }

    public void PlayDamagePlayer()
    {
        float distance = Vector3.Distance(transform.position, targetPlayer.transform.position);
        if(distance <= stoppingDistance)
        playerUIControl.DamagePlayer(10, transform.position);
        
        
    }

    void Patrol()
    {
        if (playerUIControl.isDead) return;
        if (patrolPoints.Length == 0) return; // ���� ����� �������������� �� ������, ������ �� ������

       // animator.SetBool("isWalking", true); // �������� �������� ������

        // ���� ����� ������ ������� ���������� �����
        if (!agent.pathPending && agent.remainingDistance < agent.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;

           
            // ��� �� ����� �������� �����
            if (patrolTimer >= patrolWaitTime)
            {
                // ��������� � ��������� �����
                animator.SetBool("isWalking", false);
                animator.SetBool("idle", true);

                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                agent.SetDestination(patrolPoints[currentPatrolIndex]);
                patrolTimer = 0f;

            }
        }
        else if(!playerUIControl.isDead)
        {
            // ���������� �������� � ������� �����
            agent.SetDestination(patrolPoints[currentPatrolIndex]);
           
            animator.SetBool("idle", false);
            animator.SetBool("isDeadPlayer", false);
            animator.SetBool("isWalking", true);
        } else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("idle", true);
        }
            
    } 

    void IsDeadPlayer()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttack", false);
        animator.SetBool("isDeadPlayer", true);
       // Time.timeScale = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // ������ �����������

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }
}
