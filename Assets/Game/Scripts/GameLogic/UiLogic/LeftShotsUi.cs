using System.Collections.Generic;
using Game.Scripts.GameLogic.SingShotLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.GameLogic.UiLogic
{
    public class LeftShotsUi : MonoBehaviour
    {
        [SerializeField] private Color _usedColor;
        [SerializeField] private Image _birdImagePrefab;
        [SerializeField] private float _spacing = 100f;

        private Stack<Image> _birdIcons;
        private SlingShot _slingShot;

        public void Initialize(SlingShot slingShot)
        {
            _birdIcons = new Stack<Image>();
            _slingShot = slingShot;
            _slingShot.BirdLaunched += HandleShot;
            
            SpawnAllLeftShots(_slingShot.MaxShots);
        }

        private void OnEnable()
        {
            if (_slingShot != null)
                _slingShot.BirdLaunched += HandleShot;
        }

        private void OnDestroy()
        {
            _slingShot.BirdLaunched -= HandleShot;
        }

        private void SpawnAllLeftShots(int count)
        {
            foreach (var icon in _birdIcons)
            {
                Destroy(icon);
            }
            
            _birdIcons.Clear();
            
            for (int i = 0; i < count; i++)
            {
                Image icon = Instantiate(_birdImagePrefab, transform);
                icon.rectTransform.anchoredPosition = new Vector2(i * _spacing, 0f);

                _birdIcons.Push(icon);
            }
        }

        private void HandleShot()
        {
            Image icon = _birdIcons.Pop();
            icon.color = _usedColor;
        }
    }
}