using System;
using Game.Scripts.SpawnerLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.BirdsLogic
{
    [RequireComponent(
        typeof(Rigidbody2D),
        typeof(CircleCollider2D))]
    public class Bird : MonoBehaviour, IBird
    {
        private CircleCollider2D _collider;
        public Rigidbody2D Rigidbody2D { get; private set; }
        public MonoBehaviour MonoBehaviour => this;
        public event Action<ISpawnable> Disappeared;

        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            Rigidbody2D.isKinematic = true;
            _collider.enabled = false;
        }

        public void Launch(Vector3 direction, float force)
        {
            Rigidbody2D.isKinematic = false;
            _collider.enabled = true;
            
            Rigidbody2D.AddForce(direction * force, ForceMode2D.Impulse);
        }

        public void Disappear()
        {
            Disappeared?.Invoke(this);
        }
    }
}