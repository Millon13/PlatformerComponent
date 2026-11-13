using Game.Scripts.GameObjects.Components.Physics;
using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Lava
{
    public sealed class LavaView : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private TriggerComponent _triggerComponent;

        private void Awake() => _triggerComponent = this.GetComponentInParent<TriggerComponent>();

        private void OnEnable() => _triggerComponent.OnEntered += this.OnTrigger;

        private void OnDisable() => _triggerComponent.OnEntered -= this.OnTrigger;

        private void OnTrigger(Collider2D obj) => _audioSource.Play();
    }
}