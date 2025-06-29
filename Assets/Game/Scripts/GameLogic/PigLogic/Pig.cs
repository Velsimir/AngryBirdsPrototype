using System;
using UnityEngine;

namespace Game.Scripts.GameLogic.PigLogic
{
    [RequireComponent(typeof(CapsuleCollider2D), 
        typeof(Rigidbody2D))]
    public class Pig : MonoBehaviour, IPig
    {
        [SerializeField] private float _maxHealth = 3;
        [SerializeField] private float _damageTrashHold = 0.2f;
        [SerializeField] private ParticleSystem _deathEffect;
        
        private float _currentHealth;
        
        public event Action<IPig> Disappeared;
        
        public MonoBehaviour MonoBehaviour => this;
        public Rigidbody2D RigidBody { get; private set; }
        public Collider2D Collider2D { get; private set;}

        private void Awake()
        {
            RigidBody = GetComponent<Rigidbody2D>();
            Collider2D = GetComponent<Collider2D>();
            _currentHealth = _maxHealth;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            float impactVelocity = collision.relativeVelocity.magnitude;

            if (impactVelocity > _damageTrashHold)
            {
                TakeDamage(impactVelocity);
            }
        }

        private void TakeDamage(float amount)
        {
            if (amount < 0)
                return;
            
            _currentHealth -= amount;

            if (_currentHealth <= 0)
                Disappear();
        }

        public void Disappear()
        {
            Instantiate(_deathEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Disappeared?.Invoke(this);
        }
    }
}