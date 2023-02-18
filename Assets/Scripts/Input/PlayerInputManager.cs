using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Input
{
    public class PlayerInputManager : MonoBehaviour
    {
        private InputManager _inputManager;
        public event EventHandler OnAttackAction;

        private void Awake()
        {
            _inputManager = new InputManager();
            _inputManager.Enable();
        }

        public Vector2 GetMovementVector()
        {
            Vector2 input = _inputManager.Active.Walk.ReadValue<Vector2>();

            return input;
        }
        
        private void OnAttackPerformed(InputAction.CallbackContext context)
        {
            OnAttackAction?.Invoke(this, EventArgs.Empty);
        }
    }
}