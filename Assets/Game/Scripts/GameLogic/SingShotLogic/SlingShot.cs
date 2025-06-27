using System;
using System.Collections;
using Game.Scripts.GameLogic.BirdsLogic;
using Game.Scripts.GameLogic.SpawnerLogic;
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

        private ISpawnerService<IBird>  _spawnerService;
        private WaitForSeconds _waitSpawnBirdDelay;
        private Coroutine _spawnBirdCoroutine;
        private int _currentShots;

        public void Initialize(InputSystemAction inputSystemAction, ISpawnerService<IBird>  spawnerService)
        {
            _slingShotArea.Initialize(inputSystemAction);
            _rubberBand.Initialize(_leftBranchPosition, _rightBranchPosition, _centerOfSingleShotPosition);
            
            _spawnerService = spawnerService;

            SpawnBird();
        }

        public event Action BirdLaunched;
        public event Action LastBirdLaunched;
        
        [field: SerializeField] public int MaxShots;

        private void Awake()
        {
            _waitSpawnBirdDelay = new WaitForSeconds(_spawnBirdDelay);
        }

        private void OnEnable()
        { 
            _rubberBand.BirdLaunched += StartSpawnNewBirdCoroutine;
        }

        private void OnDisable()
        {
            _rubberBand.BirdLaunched -= StartSpawnNewBirdCoroutine;
        }

        private void StartSpawnNewBirdCoroutine()
        {
            BirdLaunched?.Invoke();
            
            if (_spawnBirdCoroutine != null)
            {
                StopCoroutine(_spawnBirdCoroutine);
                _spawnBirdCoroutine = null;
            }

            if (_currentShots >= MaxShots)
            {
                LastBirdLaunched?.Invoke();
                return;
            }


            _spawnBirdCoroutine = StartCoroutine(SpawnWithDelay());
        }

        private IEnumerator SpawnWithDelay()
        {
            yield return _waitSpawnBirdDelay;

            SpawnBird();
        }

        private void SpawnBird()
        {
            _rubberBand.SetNewBird(_spawnerService.SpawnAs<IBird>(_idlePosition));
            _currentShots++;
        }
    }
}