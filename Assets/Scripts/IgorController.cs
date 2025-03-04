
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class IgorController : MonoBehaviour
{
    public Transform targetPlayer;
    NavMeshAgent agent;
    Animator animator;
    AudioSource audioSource;
    public AudioClip[] audioClips;
   public bool isHasTriggered = false;
    bool isPlayning = false;
    bool rotateIgor = false;

    public float followDistance = 2f; // ���������, �� ������� ������ ������ ������������
    public float stopThreshold = 0.5f; // ����� ��� �����������, ��� ������ ��� ������ � ������

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayning)
        {
            isPlayning = true;
            isHasTriggered = true;
            if (audioClips != null)
            {
                audioSource.PlayOneShot(audioClips[0]);
            }

        }
    }

    private void Update()
    {

        if (isHasTriggered)
        {
            float distance = Vector3.Distance(transform.position, targetPlayer.position);

            // ���� ������ ������ �������� ���������, ��������� � ������
            if (distance > followDistance)
            {
                StartMoveToPlayer();

            }
            else if (distance <= followDistance)
            {
                StopMoving();
            }

            if (!rotateIgor)
            {
                RotateOgor();
            }
        }
        UpdateAnimation();

        
    }

    void StartMoveToPlayer()
    {

        agent.SetDestination(targetPlayer.position);
       
        
        
    }

    void StopMoving()
    {
        agent.ResetPath(); // ������������� �������� ������
       
    }


    void UpdateAnimation()
    {
        // ��������� �������� ������
        float speed = agent.velocity.magnitude;

        if (speed > 0.1f) // ���� ������ ��������
        {
            if (!animator.GetBool("isWalk"))
            {
                animator.SetBool("isWalk", true);
            }
        }
        else // ���� ������ �����������
        {
            if (animator.GetBool("isWalk"))
            {
                animator.SetBool("isWalk", false);
            }
        }
    }


    void RotateOgor()
    {
        
        Vector3 directionToTarget = targetPlayer.position - transform.position;
        directionToTarget.y = 0; // ���������� ��� Y ��� ��������������� ��������

        // ���������, ��� ����������� ���������
        if (directionToTarget != Vector3.zero)
        {
            // ������������ ������ � ������� ����
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2f);
            Invoke("RotateIgorFalse", 2f);
            
        }
    }

    void RotateIgorFalse()
    {
        rotateIgor = true;
    }
}   
