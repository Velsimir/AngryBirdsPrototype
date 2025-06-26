using System;
using System.Collections;
using Game.Scripts.GameLogic.BirdsLogic;
using Game.Scripts.SpawnerLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.SingShotLogic
{
    [RequireComponent(typeof(SlingShotArea))]
    public class SlingShot : MonoBehaviour
    {
        [SerializeField] private SlingShotArea  _slingShotArea;
        [SerializeField] private RubberBand _rubberBand;
        [SerializeField] private Transform _leftBranchPosition;
        [SerializeField] private Transform _rightBranchPosition;
        [SerializeField] private Transform _centerOfSingleShotPosition;        
        [SerializeField] private Transform _idlePosition;
        [SerializeField] private float _spawnBirdDelay;
        [SerializeField] private int _maxShots;
        
        private ISpawnerService<IBird>  _spawnerService;
        private IBird _currentBird;
        private WaitForSeconds _waitSpawnBirdDelay;
        private Coroutine _spawnBirdCoroutine;
        private int _currentShots;

        private void Awake()
        {
            _waitSpawnBirdDelay = new WaitForSeconds(_spawnBirdDelay);
        }

        public void Initialize(InputSystemAction inputSystemAction, ISpawnerService<IBird>  spawnerService)
        {
            _slingShotArea.Initialize(inputSystemAction);
            _rubberBand.Initialize(_leftBranchPosition, _rightBranchPosition, _centerOfSingleShotPosition);
            
            _spawnerService = spawnerService;

            SpawnNewBird();
        }

        private void SpawnNewBird()
        {
            if (_spawnBirdCoroutine != null)
            {
                StopCoroutine(_spawnBirdCoroutine);
                _spawnBirdCoroutine = null;
            }

            if (_currentShots >= _maxShots)
                return;

            _spawnBirdCoroutine = StartCoroutine(SpawnWithDelay());
        }

        private IEnumerator SpawnWithDelay()
        {
            yield return _waitSpawnBirdDelay;

            if (_currentBird != null)
                _currentBird.Launched -= SpawnNewBird;
            
            _currentBird = _spawnerService.SpawnAs<IBird>(_idlePosition);
            _rubberBand.SetNewBird(_currentBird);
            _currentBird.Launched += SpawnNewBird;
            _currentShots++;
        }
    }
}