using System;
using Game.Scripts.GameLogic.BirdsLogic;
using Game.Scripts.GameLogic.InputLogic;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Game.Scripts.GameLogic.SingShotLogic
{
    public class RubberBand : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRendererLeft;
        [SerializeField] private LineRenderer _lineRendererRight;
        [SerializeField] private float _maxRubberBandLenght;
        [SerializeField] private SlingShotArea _slingShotArea;
        [SerializeField] private float _birdOffsetX;
        [SerializeField] private float _birdOffsetY;
        
        private Transform _leftBranchPosition;
        private Transform _rightBranchPosition;
        private Transform _centerOfSingleShotPosition;
        
        private Vector2 _rubberBandLinesPosition;
        private Vector3 _direction;
        private bool _isClickedWithinArea;

        private IBird _currentBird;

        private IInputClickHandlerService _clickHandler;
        private Camera _camera;

        public void Initialize(IInputClickHandlerService clickHandler, Camera camera,Transform leftBranchPosition, Transform rightBranchPosition, Transform centerOfSingleShotPosition)
        {
            _clickHandler = clickHandler;
            _camera =  camera;
            _leftBranchPosition = leftBranchPosition;
            _rightBranchPosition = rightBranchPosition;
            _centerOfSingleShotPosition = centerOfSingleShotPosition;
            DrawLinesToPoint(_centerOfSingleShotPosition.position);
        }

        public event Action BirdLaunched; 

        private void OnEnable()
        {
            _slingShotArea.InputStarted += Activate;
            _slingShotArea.InputCanceled += Deactivate;
        }

        private void Update()
        {
            if (_isClickedWithinArea == false)
                return;
            
            DrawRubberLines();
            MoveBird();
        }

        private void OnDisable()
        {
            _slingShotArea.InputStarted -= Activate;
            _slingShotArea.InputCanceled -= Deactivate;
        }

        public void SetNewBird(IBird bird)
        {
            _currentBird = bird;
            ResetBirdPosition();
        }

        private void Activate()
        {
            if (_currentBird == null)
                return;
            
            _isClickedWithinArea = true;
        }

        private void Deactivate()
        {
            if (_isClickedWithinArea == false)
                return;
            
            _currentBird.Launch(_direction, 10f);
            BirdLaunched?.Invoke();
            _currentBird = null;
            _isClickedWithinArea = false;
            DrawLinesToPoint(_centerOfSingleShotPosition.position);
        }

        private void DrawRubberLines()
        {
            Vector3 inputPosition = _camera.ScreenToWorldPoint(_clickHandler.CurrentMousePosition);
            
            _rubberBandLinesPosition = 
                _centerOfSingleShotPosition.position + Vector3.ClampMagnitude(inputPosition - _centerOfSingleShotPosition.position, _maxRubberBandLenght);
            
            DrawLinesToPoint(_rubberBandLinesPosition);
            _direction = _centerOfSingleShotPosition.position - _currentBird.MonoBehaviour.transform.position;
        }

        private void DrawLinesToPoint(Vector2 mousePosition)
        {
            _lineRendererLeft.SetPosition(0, mousePosition);
            _lineRendererLeft.SetPosition(1, _leftBranchPosition.position);
            
            _lineRendererRight.SetPosition(0, mousePosition);
            _lineRendererRight.SetPosition(1, _rightBranchPosition.position);
        }

        private void MoveBird()
        {
            if (_currentBird == null) 
                return;
            
            _currentBird.MonoBehaviour.transform.position = _rubberBandLinesPosition + Vector2.right * _birdOffsetX + Vector2.up * _birdOffsetY;
            _currentBird.MonoBehaviour.transform.right = _centerOfSingleShotPosition.position - _currentBird.MonoBehaviour.transform.position;
        }

        private void ResetBirdPosition()
        {
            _currentBird.MonoBehaviour.transform.position = (Vector2)_centerOfSingleShotPosition.position + Vector2.right * _birdOffsetX + Vector2.up * _birdOffsetY;
        }
    }
}