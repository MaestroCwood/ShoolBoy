
using UnityEngine;

public class LastKeyControl : MonoBehaviour
{
    public bool isHawKeyPlayer = false;
    public Transform holdPosition;
    public Transform holdKeyDoor;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

            PickUpKey();
        }
    }

    public void PickUpKey()
    {
        transform.SetParent(holdPosition);  
        transform.localPosition = Vector3.zero;  
        transform.localRotation = Quaternion.identity;
        isHawKeyPlayer = true;

    }

    public void PickUpKeyToDoor()
    {
        transform.SetParent(holdKeyDoor);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Collider>().enabled = false;
        isHawKeyPlayer = true;

    }

    public void Destroykey()
    {
        Destroy(gameObject);
    }
}
