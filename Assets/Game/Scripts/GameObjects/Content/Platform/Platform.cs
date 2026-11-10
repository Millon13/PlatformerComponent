using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Platform
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private PatrolComponent _patrolComponent;

        private void Update()
        {
            _patrolComponent.CanPatrol = true;
        }
    }
}