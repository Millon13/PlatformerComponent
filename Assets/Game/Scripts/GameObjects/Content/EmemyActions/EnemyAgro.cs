using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Monkey
{
    public class EnemyAgro : MonoBehaviour

    {
        [SerializeField] private Transform _charectorTransform;
        [SerializeField] private PatrolComponent _patrolComponent;
        [SerializeField] private LookComponent _lookComponent;
        private bool isTargetFollowing;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Character")
                TryFollow();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.tag == "Character")
                TryFollow();
        }

        private void Awake()
        {
            if (_patrolComponent != null)
                _patrolComponent.CanPatrol = true;
        }

        private void TryFollow()
        {
            _patrolComponent.Patrol(_charectorTransform);
            isTargetFollowing = true;
        }
    }
}