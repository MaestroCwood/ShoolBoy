
using UnityEngine;
using UnityEngine.UIElements;

public class SavePlayerPos : MonoBehaviour
{
    public Transform player;

    public void SavePos()
    {
        Vector3 position = player.position;
        string positionString = JsonUtility.ToJson(position);
        PlayerPrefs.SetString("PlayerPosition", positionString);
        PlayerPrefs.Save();
    }

    public void LoadPos()
    {
        if (PlayerPrefs.HasKey("PlayerPosition"))
        {
            string positionString = PlayerPrefs.GetString("PlayerPosition");
            Vector3 position = JsonUtility.FromJson<Vector3>(positionString);

            // Работа с CharacterController
            CharacterController characterController = player.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false;
                player.position = position;
                characterController.enabled = true;
            }
            else
            {
                // Работа с Rigidbody
                Rigidbody rb = player.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                    rb.MovePosition(position);
                    rb.isKinematic = false;
                }
                else
                {
                    player.position = position;
                }
            }

            Debug.Log("Position loaded: " + player.position);
        }
        else
        {
            Debug.Log("No saved position found.");
        }
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteKey("PlayerPosition");
    }
}
