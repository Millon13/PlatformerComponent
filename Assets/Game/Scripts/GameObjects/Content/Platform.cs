using UnityEngine;

namespace Game.Scripts.GameObjects.Content
{
    public class Platform: MonoBehaviour
    {
        [SerializeField] private TriggerComponent _trigger;
        private bool keepWorldPosition = true;
        private void OnEnable()
        {
            _trigger.OnEntered += OnTriggerEntered;
            _trigger.OnExited  += OnTriggerExited;
        }

        private void OnDisable()
        {
            _trigger.OnEntered -= OnTriggerEntered;
            _trigger.OnExited  -= OnTriggerExited;
        }

        private void OnTriggerEntered(Collider2D other)
        {
            other.transform.SetParent(transform, keepWorldPosition);
        }

        private void OnTriggerExited(Collider2D other)
        {
            other.transform.SetParent(null, keepWorldPosition);
        }
    }
}