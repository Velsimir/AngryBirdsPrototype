using UnityEngine;
using UnityEngine.InputSystem;


namespace Game.Scripts.GameLogic.SingleShotLogic
{
    public class SingleShot : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRendererLeft;
        [SerializeField] private LineRenderer _lineRendererRight;
        [SerializeField] private Transform _leftBranch;
        [SerializeField] private Transform _rightBranch;

        private void Update()
        {
            if (Mouse.current.leftButton.isPressed)
            {
                DrawBranch();
            }
        }

        private void DrawBranch()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            DrawBranсhLinesToPoint(mousePosition);
        }

        private void DrawBranсhLinesToPoint(Vector2 mousePosition)
        {
            _lineRendererLeft.SetPosition(0, mousePosition);
            _lineRendererLeft.SetPosition(1, _leftBranch.position);
            
            _lineRendererRight.SetPosition(0, mousePosition);
            _lineRendererRight.SetPosition(1, _rightBranch.position);
        }
    }
}