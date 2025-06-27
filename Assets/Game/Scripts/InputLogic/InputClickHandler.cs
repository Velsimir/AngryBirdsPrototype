using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.GameLogic.InputLogic
{
    public class InputClickHandler : IInputClickHandlerService, IDisposable
    {
        private readonly InputSystemAction _inputSystem;

        public InputClickHandler()
        {
            _inputSystem = new InputSystemAction();

            _inputSystem.Enable();

            _inputSystem.Player.Click.performed += HandleClick;
            _inputSystem.Player.Click.canceled += HandleCanceledClick;
        }

        public event Action<Vector2> ClickPressed;
        public event Action ClickCanceled;

        private void HandleCanceledClick(InputAction.CallbackContext obj)
        {
            ClickCanceled?.Invoke();
        }

        public void Dispose()
        {
            _inputSystem.Disable();
            _inputSystem.UI.Click.performed -= HandleClick;
            _inputSystem.Player.Attack.canceled -= HandleCanceledClick;
        }

        private void HandleClick(InputAction.CallbackContext obj)
        {
            Vector2 newMousePosition = _inputSystem.Player.ClickPosition.ReadValue<Vector2>();
            ClickPressed?.Invoke(newMousePosition);
        }
    }
}