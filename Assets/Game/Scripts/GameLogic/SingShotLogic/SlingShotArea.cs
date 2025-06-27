using System;
using Game.Scripts.GameLogic.InputLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.SingShotLogic
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class SlingShotArea : MonoBehaviour
    {
        [SerializeField] GameObject _objectToSpawn;
        private IInputClickHandlerService _inputClickHandler;
        private Camera _camera;

        public event Action InputStarted;
        public event Action InputCanceled;

        public void Initialize(IInputClickHandlerService inputClickHandler, Camera camera)
        {
            _inputClickHandler = inputClickHandler;
            _camera = camera;
            _inputClickHandler = inputClickHandler;
            _inputClickHandler.ClickPressed += CheckMouseInArea;
            _inputClickHandler.ClickCanceled += CancelClick;
        }

        private void OnEnable()
        {
            if (_inputClickHandler != null)
            {
                _inputClickHandler.ClickPressed += CheckMouseInArea;
                _inputClickHandler.ClickCanceled += CancelClick;
            }
        }
        
        private void OnDisable()
        {
            if (_inputClickHandler != null)
            {
                _inputClickHandler.ClickPressed -= CheckMouseInArea;
                _inputClickHandler.ClickCanceled -= CancelClick;
            }
        }

        private void CheckMouseInArea(Vector2 mousePosition)
        {
            Vector2 worldPoint = _camera.ScreenToWorldPoint(mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(worldPoint);

            if (hit != null && hit.gameObject == this.gameObject)
            {
                InputStarted?.Invoke();
            }
        }

        private void CancelClick()
        {
            InputCanceled?.Invoke();
        }
    }
}