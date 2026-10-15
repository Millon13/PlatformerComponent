using System;
using UnityEngine;

namespace Game
{
    public sealed class HealthComponent : MonoBehaviour
    {
        [SerializeField] private CollisionComponent _healthCollision;
        
        [SerializeField] private float _maxHealth;

        public float CurrentHealth { get; private set; }
        public bool IsAlive => CurrentHealth > 0;
        public bool IsDied => CurrentHealth <= 0;

        public event Action<float> OnHealthChanged;
        public event Action OnTakedDamage;
        public event Action OnDied;

        [SerializeField] private float _damage = 1f;

        private void Awake()
        {
            CurrentHealth = _maxHealth;
        }

        private void Update()
        {
            if (CurrentHealth <= 0)
                OnDied?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            if (!IsAlive || damage <= 0)
                return;

            CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
            OnHealthChanged?.Invoke(CurrentHealth);
            Debug.Log($"[Health]  HP теперь {CurrentHealth}");
            OnTakedDamage?.Invoke();

            if (CurrentHealth <= 0)
            {
                OnTakedDamage?.Invoke();
                OnDied?.Invoke();
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            HealthComponent damageable = collision.collider.GetComponentInParent<HealthComponent>();
            if (damageable == null || damageable == this)
                return;

            damageable.TakeDamage(_damage);
        }

        private void OnEnable()
        {
            _healthCollision.OnEntered += OnCollisionEnter2D;
        }

        private void OnDisable()
        {
            _healthCollision.OnEntered -= OnCollisionEnter2D;
        }
    }
}