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
        private bool _hasBeenLaunched = false;
        private bool _isHitSomething = false;
        
        public event Action<IBird> Disappeared;
        
        public Rigidbody2D Rigidbody2D { get; private set; }
        public MonoBehaviour MonoBehaviour => this;

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

        private void FixedUpdate()
        {
            if (_hasBeenLaunched && _isHitSomething == false)
                transform.right = Rigidbody2D.velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _isHitSomething = true;
        }

        public void Launch(Vector3 direction, float force)
        {
            Rigidbody2D.isKinematic = false;
            _isHitSomething = false;
            _collider.enabled = true;
            _hasBeenLaunched = true;
            
            Rigidbody2D.AddForce(direction * force, ForceMode2D.Impulse);
        }

        public void Disappear()
        {
            Disappeared?.Invoke(this);
        }
    }
}