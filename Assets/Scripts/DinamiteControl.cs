
using UnityEngine;

public class DinamiteControl : MonoBehaviour
{

    
    public Transform holdPosition;
    public Transform holdPositionBariles;
    public GameObject buttonDinamiye;
    public ExplosenBarriel explosenBarriel;
    public Safe safe;
    public WeaponThrowAndPickUp raycastPlayer;
    public bool isHawPlayerDinamit = false;
    public ParticleSystem fireDinamete;

    public void SetUpDinamite()
    {

        transform.SetParent(holdPositionBariles);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        fireDinamete.Play();
        isHawPlayerDinamit = false;
        explosenBarriel.setUpDinamite = true;


    }

    public void PickUpDinamite()
    {
        transform.SetParent(holdPosition);  // ѕрив€зываем динамит к левой руке
        transform.localPosition = Vector3.zero;  // ”станавливаем позицию динамита относительно игрока
        transform.localRotation = Quaternion.identity;
        isHawPlayerDinamit = true;  



    }

    private void Update()
    {
       
        if(raycastPlayer.hit.collider != null)
        {
            if(raycastPlayer.hit.collider.CompareTag("Dinamite"))
            {
                buttonDinamiye.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && safe.isOpenSafe)
                {
                    PickUpDinamite();
                }
            } else buttonDinamiye.SetActive(false);
        }
        
    }
}
