using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.GameLogic.SingShotLogic
{
    public class RubberBandDrawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRendererLeft;
        [SerializeField] private LineRenderer _lineRendererRight;
        [SerializeField] private float _maxLenght;
        [SerializeField] private SlingShotArea _slingShotArea;
        
        private Transform _leftBranchPosition;
        private Transform _rightBranchPosition;
        private Transform _centerOfSingleShotPosition;
        
        private Vector2 _rubberBandLinesPosition;
        private bool _isClickedWithinArea;

        public void Initialize(Transform leftBranchPosition, Transform rightBranchPosition, Transform centerOfSingleShotPosition)
        {
            _leftBranchPosition = leftBranchPosition;
            _rightBranchPosition = rightBranchPosition;
            _centerOfSingleShotPosition = centerOfSingleShotPosition;
            DrawLinesToPoint(_centerOfSingleShotPosition.position);
        }

        private void OnEnable()
        {
            _slingShotArea.MouseLeftStarted += Activate;
            _slingShotArea.MouseLeftCanceled += Deactivate;
        }

        private void OnDisable()
        {
            _slingShotArea.MouseLeftStarted -= Activate;
            _slingShotArea.MouseLeftCanceled -= Deactivate;
        }

        private void Activate()
        {
            _isClickedWithinArea =  true;
        }

        private void Deactivate()
        {
            _isClickedWithinArea = false;
            DrawLinesToPoint(_centerOfSingleShotPosition.position);
        }

        private void Update()
        {
            if (_isClickedWithinArea == false)
            {
                return;
            }
            
            DrawRubberLines();
        }

        private void DrawRubberLines()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            
            _rubberBandLinesPosition = 
                _centerOfSingleShotPosition.position + Vector3.ClampMagnitude(mousePosition - _centerOfSingleShotPosition.position, _maxLenght);
            
            DrawLinesToPoint(_rubberBandLinesPosition);
        }

        private void DrawLinesToPoint(Vector2 mousePosition)
        {
            _lineRendererLeft.SetPosition(0, mousePosition);
            _lineRendererLeft.SetPosition(1, _leftBranchPosition.position);
            
            _lineRendererRight.SetPosition(0, mousePosition);
            _lineRendererRight.SetPosition(1, _rightBranchPosition.position);
        }
    }
}