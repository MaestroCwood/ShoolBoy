
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DSUKPhoneBox : MonoBehaviour {

	private Animator _animator;
	public bool isOpenDoor = false ;
    public TextMeshProUGUI buttonDoorText; // Текст кнопки


    void Start() {
		_animator = GetComponent<Animator> ();
        UpdateButtonText();
    }

	public void Open() {

		if (_animator != null) {
			_animator.SetBool ("isOpen", true);
            isOpenDoor = true;
            UpdateButtonText();
        }
       

    }

	public void Close() {

		if (_animator != null) {
			_animator.SetBool ("isOpen", false);
            isOpenDoor = false;
            UpdateButtonText();
        }
       
    }

	public void ToggleDoor() {

		if (_animator != null) {
            bool currentState = _animator.GetBool("isOpen");
            _animator.SetBool ("isOpen", !_animator.GetBool ("isOpen"));

            isOpenDoor = !currentState;
			UpdateButtonText();

        }

	}

    public void UpdateButtonText()
    {
        buttonDoorText.text = isOpenDoor ? "Закрыть\n[У]" : "Открыть\n[У]";
    }

}
