using UnityEngine;

namespace Game.ImpulseComponent
{
    public class ImpulseInputController  : MonoBehaviour
    {
        [SerializeField] private GameObject _character;

        private void Update()
        {
            HandleKeyboard();
        }

        private void HandleKeyboard()
        {
        
            if (Input.GetKey(KeyCode.Mouse0))
                _character.GetComponent<PushRequestComponent>().Push();
            if (Input.GetKey(KeyCode.Mouse1))
                _character.GetComponent<TossRequestComponent>().Toss();
        }
    }
}