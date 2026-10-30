using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class SystemInput:MonoBehaviour
    {
        [SerializeField] private InputActionProperty _actionProperty;

        private void Awake()
        {
            
             //_jumpAction = new InputAction("Jump",InputActionType.Button);
           //  _jumpAction.AddBinding("<KeyBoard>/space");
                   
        }

        private void OnEnable()
        {var action=_actionProperty.action;
            if (action != null)
            {
                action.started += OnAct;
                action.performed += OnAct;
                action.canceled += OnAct;
                action?.Enable();
            }
        }

        private void OnDisable()
        {
            var action=_actionProperty.action;
            if (action != null)
            {
                action.started -= OnAct;
                action.performed -= OnAct;
                action.canceled -= OnAct;
                action?.Disable();
            }
        }

        private void OnDestroy()
        {
            _actionProperty.action?.Dispose();
        }

        private void OnAct(InputAction.CallbackContext context)
        {
            Debug.Log($"Update\n\t{context.ReadValue<float>().ToString(CultureInfo.CurrentCulture)}");
        }
    }
}