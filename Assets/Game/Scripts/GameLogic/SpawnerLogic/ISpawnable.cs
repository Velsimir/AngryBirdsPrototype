using System;
using UnityEngine;

namespace Game.Scripts.GameLogic.SpawnerLogic
{
    public interface ISpawnable<T> where T : ISpawnable<T>
    {
        public MonoBehaviour MonoBehaviour { get; }
        public event Action<T> Disappeared;
        public void Disappear();
    }
}