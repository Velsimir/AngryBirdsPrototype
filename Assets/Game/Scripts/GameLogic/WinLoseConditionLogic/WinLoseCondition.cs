using System.Collections;
using System.Collections.Generic;
using Game.Scripts.GameLogic.PigLogic;
using Game.Scripts.GameLogic.SingShotLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.WinLoseConditionLogic
{
    public class WinLoseCondition : MonoBehaviour
    {
        [SerializeField] private float _loseScreenDelay;
        
        private IPig[] _allPigsOnLevel;
        private List<IPig> _currentPigsOnLevel;
        private GameObject _loseScreen;
        private GameObject _winScreen;
        private SlingShot _slingShot;
        private WaitForSeconds _waitDelay;
        private Coroutine _loseScreenCoroutine;
        
        public void Initialize(SlingShot slingShot,GameObject loseScreen,  GameObject winScreen)
        {
            _allPigsOnLevel = FindObjectsOfType<Pig>();
            _currentPigsOnLevel = new List<IPig>();
            _waitDelay = new WaitForSeconds(_loseScreenDelay);
            
            _slingShot = slingShot;
            _loseScreen = loseScreen;
            _winScreen = winScreen;

            _slingShot.LastBirdLaunched += StartLoseTimer;
            
            foreach (var pig in _allPigsOnLevel)
            {
                _currentPigsOnLevel.Add(pig);
                pig.Disappeared += RemovePig;
            }
        }

        private void StartLoseTimer()
        {
            if (_loseScreenCoroutine != null)
            {
                StopCoroutine(_loseScreenCoroutine);
                _loseScreenCoroutine = null;
            }
            
            _loseScreenCoroutine = StartCoroutine(ShowLoseScreenWithDelay());
        }

        private IEnumerator ShowLoseScreenWithDelay()
        {
            yield return _waitDelay;
            ShowLoseScreen();
        }

        private void RemovePig(IPig pig)
        {
            _currentPigsOnLevel.Remove(pig);
            pig.Disappeared -= RemovePig;

            if (_currentPigsOnLevel.Count <= 0)
            {
                ShowWinScreen();
            }
        }

        private void ShowWinScreen()
        {
            _winScreen.SetActive(true);
        }

        private void ShowLoseScreen()
        {
            _loseScreen.SetActive(true);
        }
    }
}