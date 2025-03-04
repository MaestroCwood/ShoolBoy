
using UnityEngine;

public class SaveTriger1 : MonoBehaviour
{
    public SavePlayerPos savePos;
   
    bool isSave = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") & !isSave)
        {
            savePos.SavePos();
           
            Debug.Log("Save Pos Complitte");
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("PlayerPosition"))
        {
            savePos.LoadPos();
           
        }
       
    }
}
