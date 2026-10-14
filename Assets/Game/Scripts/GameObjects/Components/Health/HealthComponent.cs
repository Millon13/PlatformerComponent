using System;
using UnityEngine;

namespace Game
{
    public sealed class HealthComponent : MonoBehaviour
    {
        [SerializeField] private TriggerComponent _healthTrigger;
        [SerializeField]
        private float _maxHealth;

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

        public void TakeDamage(float damage)
        {
            if (!IsAlive || damage <= 0)
                return;

            CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
            OnHealthChanged?.Invoke(CurrentHealth);
            Debug.Log($"[Health]  HP теперь {CurrentHealth}");
            OnTakedDamage?.Invoke();

            if (CurrentHealth <= 0)
                OnDied?.Invoke();
        }
        

        public void SetZero() => 
            this.TakeDamage(this.CurrentHealth);

        
        private void OnTriggerEntered(Collider2D other)
        {
            if (other.TryGetComponent(out HealthComponent damageable))
            {
                damageable.TakeDamage(_damage);
            }

        }

        private void OnEnable()
        {
            _healthTrigger.OnEntered += OnTriggerEntered;
        }

        private void OnDisable()
        {
            _healthTrigger.OnEntered -= OnTriggerEntered;
        }

        
    }
}