
using UnityEngine;

public class TriggerGirl1 : MonoBehaviour
{
    [SerializeField]VikaConroller vikaConroller;

    bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {   
            if (hasTriggered) return;
            hasTriggered = true;
            vikaConroller.isPlay = true;
            vikaConroller.StartCorutinePlay();


        }
    }
}
