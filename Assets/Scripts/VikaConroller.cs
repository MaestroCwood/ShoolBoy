
using System.Collections;

using UnityEngine;
using UnityEngine.AI;

public class VikaConroller : MonoBehaviour
{
    public AudioClip[] audioClips;
    public Transform playerPos;
    public Transform parolPos;
   
    AudioSource audioSource;
    Collider triggerHelp;
    public bool isPlay = false;
    bool hasTriggered = false;
    NavMeshAgent agent;
    Animator animator;
    bool finishTrigger = false;
    public IgorController igorController;
    public bool finals = false; //Проблемные комментарийы

    public float followDistance = 2f; // ���������, �� ������� ������ ������ ������������
    public float stopThreshold = 0.5f;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        triggerHelp = GetComponentInChildren<Collider>();
        agent = GetComponentInChildren<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

      

    }

   

    private void Update()
    {
        if(!agent.hasPath && !finishTrigger && agent.enabled)
        {
            finishTrigger = true;
            AnimateFalse();
            audioSource.PlayOneShot(audioClips[5]);
            
        }

        if (igorController.isHasTriggered && finals)
        {
            
            GoFolovVika();
            UpdateAnimation();
          
        }
        
    }
    
    public void PlayGolosVika()
    {
        audioSource.PlayOneShot(audioClips[Random.Range(0,3)]);
    }

    IEnumerator SoundHelp()
    {   
        while(isPlay && !hasTriggered)
        {
            PlayGolosVika();
            yield return new WaitForSeconds(Random.Range(5f, 15f));
        }     
    }

    public void StartCorutinePlay()
    {
        StartCoroutine(nameof(SoundHelp));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (!hasTriggered)
            {
                hasTriggered = true;
                isPlay = false;
                audioSource.PlayOneShot(audioClips[4]);
                agent.enabled = true;
                animator.SetBool("isWalk", true);
                agent.SetDestination(playerPos.position);
                Invoke("AnimateFalse", 1f);
                Invoke("GoVikaToParol", 4f);
            }
            
           
        }

        if(igorController.isHasTriggered && !finals)
        {
            // ��������� ������  
            finals = true;
            agent.stoppingDistance = 3f;
            FinalGolosVika(6);
            Debug.Log("final Trigger");
        }
    }
    public void GoVikaToParol()
    {
        agent.SetDestination(parolPos.position);
        animator.SetBool("isWalk", true);
        agent.stoppingDistance = 0f;
    }

   void AnimateFalse()
    {
        animator.SetBool("isWalk", false);
    }

    void StartMoveToPlayer()
    {

        agent.SetDestination(playerPos.position);



    }

    void StopMoving()
    {
        agent.ResetPath(); 

    }

    void GoFolovVika ()
    {
     
            float distance = Vector3.Distance(transform.position, playerPos.position);
            
            // ���� ������ ������ �������� ���������, ��������� � ������
            if (distance > followDistance)
            {
                StartMoveToPlayer();

            }
            else if (distance <= followDistance)
            {
                StopMoving();
            }

        
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

    public void FinalGolosVika(int nameClip)
    {
        audioSource.PlayOneShot(audioClips[nameClip]);
    }
}
