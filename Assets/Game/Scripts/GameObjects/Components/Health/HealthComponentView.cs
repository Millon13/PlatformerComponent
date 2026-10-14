using System;
using UnityEngine;

namespace Game
{
    public class HealthComponentView: MonoBehaviour
    {
        [SerializeField] HealthComponent _healthComponent;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] private AudioClip _damageSound;
        [SerializeField] private TakeDamageColorComponent _damageColorComponent;

        private void Awake()
        {
            if (_healthComponent == null)
                _healthComponent = GetComponent<HealthComponent>();
        }

        private void OnEnable()
        {
            
            _healthComponent.OnTakedDamage += OnTakeDamage;
            _healthComponent.OnTakedDamage += _damageColorComponent.TakeDamage;
            _healthComponent.OnDied += _damageColorComponent.TakeDamage;
            _healthComponent.OnDied += OnDied;
            
            
            
        }
        private void OnDisable()
        {
            
            _healthComponent.OnTakedDamage -= OnTakeDamage;
            _healthComponent.OnTakedDamage -= _damageColorComponent.TakeDamage;
             _healthComponent.OnDied -= _damageColorComponent.TakeDamage;
             _healthComponent.OnDied -= OnDied;
           
            
        }

        private void OnTakeDamage()
        {
            if (_damageSound != null && _audioSource != null)
            {
                _audioSource.PlayOneShot(_damageSound);
            }
        }

        private void OnDied()
        {
            Destroy(this.gameObject);
        }
    }
}