
using System.Collections;
using TMPro;
using UnityEngine;

public class SetUpDinamite : MonoBehaviour
{
    public Transform holdPosition;
    public GameObject buttonDinamiye;
    public DinamiteControl dinamiteControl;
    public WeaponThrowAndPickUp raycastPlayer;
    public TextMeshProUGUI textTimer;
    ExplosenBarriel explosenBarriel;

    private void Start()
    {
        explosenBarriel = GetComponent<ExplosenBarriel>();
    }
    private void Update()
    {
        if (raycastPlayer.hit.collider != null && dinamiteControl.isHawPlayerDinamit)
        {
            if (raycastPlayer.hit.collider.gameObject.name == "Body")
            {
                buttonDinamiye.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    HoldDinamite();
                }

            }
            else buttonDinamiye.SetActive(false);
        }
    }

    public void HoldDinamite()
    {
        dinamiteControl.SetUpDinamite();
        buttonDinamiye.SetActive(false);
        textTimer.enabled = true;
        StartCoroutine(nameof(TimerExplosen));
        Invoke("Explosen", 10f);
    }

    void Explosen()
    {
        explosenBarriel.Explosen();
    }

    IEnumerator TimerExplosen()
    {
        int time = 10;
        while(time >0)
        {
            textTimer.text = "До взрыва осталось: " + time;
            if(time <=5) textTimer.color = Color.red;
            yield return new WaitForSeconds(1f);

            time--;
        }

        textTimer.enabled = false;
    }
}
