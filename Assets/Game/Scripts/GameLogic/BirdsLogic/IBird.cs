using System;
using Game.Scripts.GameLogic.SpawnerLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.BirdsLogic
{
    public interface IBird : ISpawnable<IBird>
    {
        public Rigidbody2D Rigidbody2D { get; }
        
        public void Launch(Vector3 direction, float force);
    }
}