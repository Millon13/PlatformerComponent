using UnityEngine;

namespace Game
{
    public class TossComponentView:MonoBehaviour
    {
        [SerializeField] private TossComponent _tossComponent;
        [SerializeField] private Animator _animator;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] private AudioClip _tossSound;
        private void Awake()
        {
            if (_tossComponent == null)
                _tossComponent = GetComponent<TossComponent>();
        }
        private void OnEnable()
        {
            _tossComponent.OnToss += OnToss;

        }

        private void OnDisable()
        {
            _tossComponent.OnToss -= OnToss;

        }

        private void OnToss(bool isTossed)
        {
            _animator.SetBool("BlowUp",isTossed);
            _audioSource.PlayOneShot(_tossSound);
        }
    }
}