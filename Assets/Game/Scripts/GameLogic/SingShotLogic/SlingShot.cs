using Game.Scripts.GameLogic.BirdsLogic;
using Game.Scripts.SpawnerLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.SingShotLogic
{
    [RequireComponent(typeof(SlingShotArea))]
    public class SlingShot : MonoBehaviour
    {
        [SerializeField] private SlingShotArea  _slingShotArea;
        [SerializeField] private RubberBandDrawer _rubberBandDrawer;
        [SerializeField] private Transform _leftBranchPosition;
        [SerializeField] private Transform _rightBranchPosition;
        [SerializeField] private Transform _centerOfSingleShotPosition;        
        [SerializeField] private Transform _idlePosition;
        
        private ISpawnerService<IBird>  _spawnerService;
        private IBird _currentBird;
        
        public void Initialize(InputSystemAction inputSystemAction, ISpawnerService<IBird>  spawnerService)
        {
            _slingShotArea.Initialize(inputSystemAction);
            _rubberBandDrawer.Initialize(_leftBranchPosition, _rightBranchPosition, _centerOfSingleShotPosition);
            _spawnerService = spawnerService;
        }
    }
}