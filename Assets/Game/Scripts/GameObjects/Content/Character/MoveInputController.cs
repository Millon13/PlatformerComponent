using Game.Scripts.GameObjects.Components.Move;
using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Character
{
    public class MoveInputController : MonoBehaviour
    {
        [SerializeField] private GameObject _character;

        private void Update()
        {
            HandleKeyboard();
        }

        private void HandleKeyboard()
        {
            Vector3 direction = Vector3.zero;

            if (Input.GetKey(KeyCode.UpArrow))
                direction.z = 1;
            else if (Input.GetKey(KeyCode.DownArrow))
                direction.z = -1;

            if (Input.GetKey(KeyCode.LeftArrow))
                direction.x = -1;
            else if (Input.GetKey(KeyCode.RightArrow))
                direction.x = 1;

            if (direction != Vector3.zero)
                _character.GetComponent<MoveComponent>().Move(direction);
        }
    }
}