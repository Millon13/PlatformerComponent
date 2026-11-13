using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Tent
{
    public class TentView : MonoBehaviour
    {
        [SerializeField] private Tent _tent;
        
        [SerializeField] private GameObject _rainbowObject;
        
        [SerializeField] AudioSource _audioSource;
        
        [SerializeField] private AudioClip _victorySound;

        private void OnEnable()
        {
            _tent.OnFinished += OnFinished;
        }

        private void OnDisable()
        {
            _tent.OnFinished -= OnFinished;
        }

        private void OnFinished(bool isFinished)
        {
            _rainbowObject.SetActive(isFinished);
            if (_audioSource != null && gameObject.activeSelf)
                _audioSource.PlayOneShot(_victorySound, 0.02f);
        }
    }
}