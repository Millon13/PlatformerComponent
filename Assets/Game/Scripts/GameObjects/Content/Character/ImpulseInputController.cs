using UnityEngine;

namespace Game.ImpulseComponent
{
    public class ImpulseInputController : MonoBehaviour
    {
        [SerializeField] private GameObject _character;
        [SerializeField] private KeyCode _impulse;

        private void Update()
        {
            HandleKeyboard();
        }

        private void HandleKeyboard()
        {
            if (Input.GetKey(_impulse))
                _character.GetComponent<ImpulseRequestComponent>().Impulse();
        }
    }
}