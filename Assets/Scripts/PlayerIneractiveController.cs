using UnityEngine;
using UnityEngine.UI;


public class PlayerInteractiveController : MonoBehaviour
{
    public Camera playerCamera; // Камера игрока
    public GameObject buttonDoorOpen; // Кнопка взаимодействия
    public Button buttonCall;
    private bool buttonPressed = false;

    public float raycastDistance = 5f; // Дистанция проверки луча

    private DSUKPhoneBox currentDoor; // Ссылка на дверь
    public GameObject callPhone;
    public PoliceControl policeControl;
    public PlayerAudioControl playerAudioControl;
    bool hasCallingTopolice = false;

    private void Start()
    {
        buttonCall.onClick.AddListener(() => buttonPressed = true);
        
    }


    private void Update()
    {
        HandleDoorInteraction();
        HandlePhoneInteraction();
    }

    private void HandleDoorInteraction()
    {
        RaycastHit hit;

        // Проверяем, попадает ли луч в объект с тегом "Door"
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Door") )
            {
                // Получаем компонент двери
                currentDoor = hit.collider.GetComponentInParent<DSUKPhoneBox>();


                if (currentDoor != null)
                {
                    buttonDoorOpen.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                        OpenDoor();
                }

                return; // Выходим, так как нашли дверь
            }
    
        }
        // Если луч не попал в дверь, скрываем кнопку
        buttonDoorOpen.SetActive(false);
       
    }

    private void HandlePhoneInteraction()
    {
        RaycastHit hit;

        // Проверяем, попадает ли луч в объект с тегом "Door"
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Phone") && !policeControl.isHasTargetPolice &&!hasCallingTopolice)
            {

                    callPhone.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E) || buttonPressed)
                {
                    // Звоним полицийскому
                    hasCallingTopolice = true;
                    buttonPressed = false;
                    Invoke("GoToPoliceFromCall", 5f);
                    playerAudioControl.PlayGolosPlayer(3);
                    Invoke("GolosPlayerCalledPolice", 3);
                    Invoke("IsHasReplayCallToPolice", 5f);
                }              

                return; 
            }

        }
        // Если луч не попал в дверь, скрываем кнопку
        callPhone.SetActive(false);

    }


    public void GolosPlayerCalledPolice()
    {
        playerAudioControl.PlayGolosPlayer(2);
    }

    void GoToPoliceFromCall()
    {
        policeControl.GoToPolice();
    }

    public void OpenDoor()
    {
        currentDoor.ToggleDoor();
    }

    void IsHasReplayCallToPolice()
    {
        hasCallingTopolice = false;
    }
}
