using UnityEngine;
using UnityEngine.UI;


public class PlayerInteractiveController : MonoBehaviour
{
    public Camera playerCamera; // ������ ������
    public GameObject buttonDoorOpen; // ������ ��������������
    public Button buttonCall;
    private bool buttonPressed = false;

    public float raycastDistance = 5f; // ��������� �������� ����

    private DSUKPhoneBox currentDoor; // ������ �� �����
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

        // ���������, �������� �� ��� � ������ � ����� "Door"
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Door") )
            {
                // �������� ��������� �����
                currentDoor = hit.collider.GetComponentInParent<DSUKPhoneBox>();


                if (currentDoor != null)
                {
                    buttonDoorOpen.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                        OpenDoor();
                }

                return; // �������, ��� ��� ����� �����
            }
    
        }
        // ���� ��� �� ����� � �����, �������� ������
        buttonDoorOpen.SetActive(false);
       
    }

    private void HandlePhoneInteraction()
    {
        RaycastHit hit;

        // ���������, �������� �� ��� � ������ � ����� "Door"
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Phone") && !policeControl.isHasTargetPolice &&!hasCallingTopolice)
            {

                    callPhone.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E) || buttonPressed)
                {
                    // ������ ������������
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
        // ���� ��� �� ����� � �����, �������� ������
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
