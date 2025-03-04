using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class PoliceControl : MonoBehaviour
{
    public Canvas canvasPolice;
    public bool isHasTargetPolice = false;
    AudioSource audioSourcePolice;
    public AudioSource playerAudisource;
    public PlayerAudioControl playerAudioControl;
    public NavMeshAgent agent;
    public GameObject targetGoPolice;
    Vector3 startPosPolice;
    float stopDistance = 0.5f;
    Animator animatorPolice;
    bool isReturningToStart = false;
    bool isPlayningAudioPolice = false;
  
    Quaternion startRotation;

    private void LateUpdate()
    {
       
        Vector3 currentPos = transform.position;
        Vector3 targetPosition = targetGoPolice.transform.position;
        if (!isReturningToStart && Vector3.Distance(currentPos, targetPosition) < stopDistance)
        {
          
            animatorPolice.SetBool("isRun", false);
            isReturningToStart = true;
            Invoke("GoToStartPositionPolice", 5f);

           
        }
        
        if (isReturningToStart && Vector3.Distance(currentPos, startPosPolice) < stopDistance )
        {
            agent.ResetPath();
            agent.updateRotation = false;
            
            animatorPolice.SetBool("isRun", false);
            isHasTargetPolice = false;
            isReturningToStart = false;

            StartCoroutine(LerpRotation());

        }


    }
    private void Start()
    {
        audioSourcePolice = GetComponent<AudioSource>();
        startPosPolice = transform.position;
        animatorPolice = GetComponent<Animator>();
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isHasTargetPolice && !playerAudisource.isPlaying && !isPlayningAudioPolice)
        {
           
            canvasPolice.enabled = true;
            isPlayningAudioPolice = true;
            if (!audioSourcePolice.isPlaying)
            {
                audioSourcePolice.Play();
               
                Invoke("SoundPlayerPlay", 4f);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            canvasPolice.enabled = false;
            Invoke("SbrosAudioPolice", 7f);
        }
    }

    void SbrosAudioPolice()
    {
        isPlayningAudioPolice = false;
    }

    void SoundPlayerPlay()
    {
        playerAudioControl.PlayGolosPlayer(1);
    }

    public void GoToPolice()
    {   
        agent.SetDestination(targetGoPolice.transform.position);
        animatorPolice.SetBool("isRun", true);
        isHasTargetPolice = true;
        agent.updateRotation = true;
    }

    public void GoToStartPositionPolice()
    {
        agent.SetDestination(startPosPolice);
        animatorPolice.SetBool("isRun", true);
    }

     IEnumerator LerpRotation()
    {
       
        transform.rotation = Quaternion.Euler(0, -180, 0);
        yield return null;
    }
}
