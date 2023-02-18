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
        public event EventHandler OnInteractAction;

        private void Awake()
        {
            _inputManager = new InputManager();
            _inputManager.Enable();

            _inputManager.Active.Interact.performed += OnInteractPerformed;
            _inputManager.Active.Attack.performed += OnAttackPerformed;
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

        private void OnInteractPerformed(InputAction.CallbackContext context)
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }

        public Vector2 GetMousePosition()
        {
            Vector2 input = _inputManager.Active.MousePosition.ReadValue<Vector2>();

            return input;
        }

        public void SetInputState(bool state)
        {
            if (state)
                _inputManager.Active.Enable();
            else
                _inputManager.Active.Disable();
        }
    }
}