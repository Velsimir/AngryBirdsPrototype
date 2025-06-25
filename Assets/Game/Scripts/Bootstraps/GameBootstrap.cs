using Game.Scripts.Extention;
using Game.Scripts.GameLogic;
using Game.Scripts.GameLogic.BirdsLogic;
using Game.Scripts.GameLogic.SingShotLogic;
using Game.Scripts.SpawnerLogic;
using UnityEngine;

namespace Game.Scripts.Bootstraps
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private SlingShot _slingShot;
        [RequireInterface(typeof(IBird)),SerializeField] private MonoBehaviour _birdPrefab;
            
        private InputSystemAction _inputSystemAction;
        private ISpawnerService<IBird> _birdSpawnerService;

        private void Awake()
        {
            _inputSystemAction = new InputSystemAction();
            _inputSystemAction.Enable();
            _birdSpawnerService = new SpawnerService<IBird>((IBird)_birdPrefab);
            _slingShot.Initialize(_inputSystemAction, _birdSpawnerService);
        }
    }
}
