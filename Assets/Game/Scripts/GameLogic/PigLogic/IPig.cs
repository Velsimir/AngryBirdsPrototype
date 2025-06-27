using Game.Scripts.SpawnerLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.PigLogic
{
    public interface IPig : ISpawnable<IPig>
    {
        public Rigidbody2D RigidBody { get; }
        public Collider2D Collider2D { get; }
    }
}