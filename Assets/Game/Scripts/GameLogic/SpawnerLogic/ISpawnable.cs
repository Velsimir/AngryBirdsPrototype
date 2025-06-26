using System;
using UnityEngine;

namespace Game.Scripts.SpawnerLogic
{
    public interface ISpawnable
    {
        public MonoBehaviour MonoBehaviour { get; }
        public event Action<ISpawnable> Disappeared;
        public void Disappear();
    }
}