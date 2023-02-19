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
        public event EventHandler OnAttackReleaseAction;
        public event EventHandler OnInteractAction;

        public bool walkEnabled = true;

        private void Awake()
        {
            _inputManager = new InputManager();
            _inputManager.Enable();

            _inputManager.Active.Interact.performed += OnInteractPerformed;
            _inputManager.Active.Attack.performed += OnAttackPerformed;
            _inputManager.Active.Attack.canceled += OnAttackReleased;

            walkEnabled = true;
        }

        public Vector2 GetMovementVector()
        {
            Vector2 input = _inputManager.Active.Walk.ReadValue<Vector2>();

            return !walkEnabled ? Vector2.zero : input;
        }
        
        private void OnAttackPerformed(InputAction.CallbackContext context)
        {
            OnAttackAction?.Invoke(this, EventArgs.Empty);
        }

        private void OnAttackReleased(InputAction.CallbackContext context)
        {
            OnAttackReleaseAction?.Invoke(this, EventArgs.Empty);
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
                walkEnabled = true;
            else
                walkEnabled = false;
        }
    }
}