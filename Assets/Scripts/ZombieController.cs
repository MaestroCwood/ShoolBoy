using System;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    NavMeshAgent agent;
    AudioSource audioSource;
    public AudioClip[] audioClips;
    public PlayerUIControl playerUIControl;
    public Transform targetPlayer; // Игрок
    public bool isHasTarget = false;
    Animator animator;
    [SerializeField] float detectionRadius = 10f; // Радиус обнаружения игрока
    [SerializeField] float stoppingDistance = 3f; // Расстояние для атаки
    [SerializeField] GameObject outPosRaycast; // Точка для Raycast (если нужна)
    public bool isChasing = false; // Преследует ли зомби игрока
    public bool isAttacking = false; // Атакует ли зомби
    [SerializeField] float patrolWaitTime = 2f; // Время ожидания на точке
    float patrolTimer = 0f;
    DamageEnemyInBullet damageEnemyInBullet;


    [SerializeField] int patrolPointCount = 4; // Количество точек патрулирования
    [SerializeField] float patrolRadius = 10f; // Радиус, в котором генерируются точки патрулирования
    Vector3[] patrolPoints; // Массив сгенерированных точек патрулирования
    int currentPatrolIndex = 0; // Текущая точка патрулирования
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        damageEnemyInBullet = GetComponent<DamageEnemyInBullet>();

    }
    void Start()
    {
        GeneratePatrolPoints(); // Генерируем точки патрулирования
         // Начинаем патрулирование
        
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
            // Генерируем случайную точку в радиусе patrolRadius
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * patrolRadius;
            randomDirection.y = 0; // Убираем смещение по оси Y, чтобы точки оставались на земле
            patrolPoints[i] = transform.position + randomDirection;

            // Проверяем, чтобы точка была доступной для NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(patrolPoints[i], out hit, patrolRadius, NavMesh.AllAreas))
            {
                patrolPoints[i] = hit.position; // Сохраняем точку, которая лежит на NavMesh
                Debug.Log($"Patrol Point {i}: {hit.position}");
            }
            else
            {
                patrolPoints[i] = transform.position; // Если точка недоступна, оставляем её на месте зомби
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

        // Если игрок в радиусе обнаружения
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
            Patrol(); // Если нет цели или игрок мёртв, патрулируем
            
            return;
        }
    }

    void ChasePlayer(float distance)
    {
        // Проверяем, если игрок мёртв
        if (playerUIControl.isDead)
        {
           
            if (!animator.GetBool("isDeadPlayer")) // Устанавливаем анимацию только один раз
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttack", false);
                animator.SetBool("isDeadPlayer", true);
                agent.ResetPath(); // Останавливаем движение агента
            }
            return; // Выходим из метода, чтобы не обновлять другие действия
        }

        // Если игрок ещё жив, продолжаем преследовать
        if (!isChasing)
        {
            isChasing = true;
            isHasTarget = true;
            animator.SetBool("idle", false);
            animator.SetBool("isAttack", false);
            animator.SetBool("isWalking", true);
        }

        agent.SetDestination(targetPlayer.position);

        // Если зомби в пределах расстояния для атаки
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
            agent.ResetPath(); // Остановить движение агента
            

        }
    }

    void StopAttack()
    {
        if (isAttacking)
        {
            isAttacking = false;
            animator.SetBool("isAttack", false);
            animator.SetBool("isWalking", true);
            agent.SetDestination(targetPlayer.position); // Возобновить движение к игроку
        }
    }

    void StopChasing()
    {
        if (isChasing)
        {
            isChasing = false;
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttack", false);
            agent.ResetPath(); // Сбросить путь агента
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
        if (patrolPoints.Length == 0) return; // Если точки патрулирования не заданы, ничего не делаем

       // animator.SetBool("isWalking", true); // Включаем анимацию ходьбы

        // Если агент достиг текущей патрульной точки
        if (!agent.pathPending && agent.remainingDistance < agent.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;

           
            // Ждём на точке заданное время
            if (patrolTimer >= patrolWaitTime)
            {
                // Переходим к следующей точке
                animator.SetBool("isWalking", false);
                animator.SetBool("idle", true);

                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                agent.SetDestination(patrolPoints[currentPatrolIndex]);
                patrolTimer = 0f;

            }
        }
        else if(!playerUIControl.isDead)
        {
            // Продолжаем движение к текущей точке
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
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // Радиус обнаружения

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }
}
