using Game.Scripts.GameObjects.Components.Jump;
using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Character
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
            if (Input.GetKeyDown(KeyCode.Space))
                _character.GetComponent<JumpComponent>().Jump();
        }
    }
}