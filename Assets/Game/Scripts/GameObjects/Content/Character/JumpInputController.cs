using UnityEngine;

namespace Game
{
    public class JumpInputController : MonoBehaviour
    {
        [SerializeField] private GameObject _character;

        private void Update()
        {
            HandleKeyboard();
        }

        private void HandleKeyboard()
        {
            if (Input.GetKey(KeyCode.Space))
                _character.GetComponent<JumpRequestComponent>().Jump();
        }
    }
}