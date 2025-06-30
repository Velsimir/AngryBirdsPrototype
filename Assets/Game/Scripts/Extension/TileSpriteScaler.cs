using UnityEngine;

namespace Game.Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class TileSpriteScaler : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Camera _mainCamera;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _mainCamera = Camera.main;

            if (_spriteRenderer.drawMode != SpriteDrawMode.Tiled)
            {
                Debug.LogWarning("TileSpriteScaler: Draw Mode должен быть 'Tiled'. Установлю автоматически.");
                _spriteRenderer.drawMode = SpriteDrawMode.Tiled;
            }
        }

        void Start()
        {
            UpdateSize();
        }

        private void UpdateSize()
        {
            float screenWidthWorld = 2f * _mainCamera.orthographicSize * _mainCamera.aspect;

            float spriteHeight = _spriteRenderer.size.y; // оставим высоту как есть
            _spriteRenderer.size = new Vector2(screenWidthWorld, spriteHeight);
        }
    }
}