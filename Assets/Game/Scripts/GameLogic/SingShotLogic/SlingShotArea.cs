using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.GameLogic.SingShotLogic
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class SlingShotArea : MonoBehaviour
    {
        [SerializeField] private LayerMask _singShoLayerMask;

        private InputSystemAction _inputSystem;

        public event Action InputStarted;
        public event Action InputCanceled;

        public void Initialize(InputSystemAction inputSystemAction)
        {
            _inputSystem = inputSystemAction;
            
            _inputSystem.Player.Attack.started += CheckMouseInArea;
            _inputSystem.Player.Attack.canceled += UnClickedMouse;
        }

        private void OnEnable()
        {
            if (_inputSystem == null)
                return;
            
            _inputSystem.Player.Attack.started += CheckMouseInArea;
            _inputSystem.Player.Attack.canceled += UnClickedMouse;
        }

        private void OnDisable()
        {
            _inputSystem.Player.Attack.started -= CheckMouseInArea;
            _inputSystem.Player.Attack.canceled -= UnClickedMouse;
        }

        private void CheckMouseInArea(InputAction.CallbackContext obj)
        {
            Vector2 worldPosition = Vector2.zero;

            if (Mouse.current.enabled)
            {
                worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            }
            else if (Touchscreen.current.enabled)
            {
                worldPosition = Camera.main.ScreenToWorldPoint(Touchscreen.current.position.ReadValue());
            }


            if (Physics2D.OverlapPoint(worldPosition, _singShoLayerMask))
            {
                InputStarted?.Invoke();
            }
        }

        private void UnClickedMouse(InputAction.CallbackContext obj)
        {
            InputCanceled?.Invoke();
        }
    }
}