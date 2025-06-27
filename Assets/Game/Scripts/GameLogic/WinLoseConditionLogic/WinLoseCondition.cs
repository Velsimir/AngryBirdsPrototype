using System.Collections.Generic;
using Game.Scripts.GameLogic.PigLogic;
using Game.Scripts.SpawnerLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.WinLoseConditionLogic
{
    public class WinLoseCondition : MonoBehaviour
    {
        private IPig[] _allPigsOnLevel;
        private List<IPig> _currentPigsOnLevel;
        
        private void Awake()
        {
            _allPigsOnLevel = FindObjectsOfType<Pig>();

            foreach (var pig in _allPigsOnLevel)
            {
                _currentPigsOnLevel.Add(pig);
               
            }
        }

        private void RemovePig(ISpawnable<IPig> pig)
        {

        }
    }
}