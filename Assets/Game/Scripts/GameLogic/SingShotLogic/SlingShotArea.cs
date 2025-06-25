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

        public event Action MouseLeftStarted;
        public event Action MouseLeftCanceled;

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
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            if (Physics2D.OverlapPoint(worldPosition, _singShoLayerMask))
            {
                MouseLeftStarted?.Invoke();
            }
        }

        private void UnClickedMouse(InputAction.CallbackContext obj)
        {
            MouseLeftCanceled?.Invoke();
        }
    }
}