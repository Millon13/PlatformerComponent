using Game.Scripts.GameObjects.Components.ControlComponent;
using Game.Scripts.GameObjects.Components.Move;
using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Platform
{
    public class Platform : MonoBehaviour, MoveComponent.IAction, MoveComponent.ICondition
    {
        [SerializeField] private PatrolComponent _patrolComponent;

        [SerializeField] private MoveComponent _moveComponent;
        bool MoveComponent.ICondition.Evaluate() => true;

        private void Awake()
        {
            _moveComponent.SetAction(this);
            _moveComponent.SetCondition(this);
        }

        void MoveComponent.IAction.Invoke(Vector3 direction)
        {
            _moveComponent.MoveStep(direction);
        }

        private void Update()
        {
            _patrolComponent.CanPatrol = true;
        }
    }
}