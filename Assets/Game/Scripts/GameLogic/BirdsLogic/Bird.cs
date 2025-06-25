using UnityEngine;

namespace Game.Scripts.GameLogic
{
    [RequireComponent(
        typeof(Rigidbody2D), 
        typeof(Animator), 
        typeof(CircleCollider2D))]
    public class Bird : MonoBehaviour
    {
        private Animator _animator;
        private CircleCollider2D _collider;
        private Rigidbody2D _rigidbody2D;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _collider = GetComponent<CircleCollider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
}