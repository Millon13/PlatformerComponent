using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Character
{
    public class ImpulseInputController : MonoBehaviour
    {
        [SerializeField] private GameObject _character;
        
        [SerializeField] private KeyCode _push;
        
        [SerializeField] private KeyCode _toss;

        private void Update()
        {
            HandleKeyboard();
        }

        private void HandleKeyboard()
        {
            if (Input.GetKeyDown(_push))
                _character.GetComponent<IPushComponent>().Push();
            if (Input.GetKeyDown(_toss))
                _character.GetComponent<ITossComponent>().Toss();
        }
    }
}