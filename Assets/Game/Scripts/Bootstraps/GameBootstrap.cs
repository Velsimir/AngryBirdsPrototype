using Game.Scripts.Extension;
using Game.Scripts.GameLogic.BirdsLogic;
using Game.Scripts.GameLogic.InputLogic;
using Game.Scripts.GameLogic.SingShotLogic;
using Game.Scripts.GameLogic.SpawnerLogic;
using Game.Scripts.GameLogic.UiLogic;
using Game.Scripts.GameLogic.WinLoseConditionLogic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Bootstraps
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SlingShot _slingShot;
        [RequireInterface(typeof(IBird)),SerializeField] private MonoBehaviour _birdPrefab;
        [SerializeField] private LeftShotsUi _leftShotsUi;
        [SerializeField] private GameObject _loseScreen;
        [SerializeField] private GameObject _winScreen;
        [SerializeField] private WinLoseCondition _winLoseCondition;
            
        private IInputClickHandlerService _inputClickHandler;
        private ISpawnerService<IBird> _birdSpawnerService;

        private void Awake()
        {
            _inputClickHandler = new InputClickHandler();
            _birdSpawnerService = new SpawnerService<IBird>((IBird)_birdPrefab);
            _slingShot.Initialize(_inputClickHandler, _birdSpawnerService, _camera);
            _leftShotsUi.Initialize(_slingShot);
            _winLoseCondition.Initialize(_slingShot, _loseScreen, _winScreen);
            
            Application.targetFrameRate = 60;
        }
        
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
