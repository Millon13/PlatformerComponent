using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class MoveComponentView : MonoBehaviour
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        [SerializeField] private MoveComponent moveTransformComponent;

        [SerializeField] private Animator _animator;

        private void Awake()
        {
            moveTransformComponent = GetComponent<MoveComponent>();
        }

        private void OnEnable()
        {
            moveTransformComponent.OnMoving += OnMoveTransform;
        }

        private void OnDisable()
        {
            moveTransformComponent.OnMoving -= OnMoveTransform;
        }

        private void OnMoveTransform(bool isMoving)
        {
            _animator.SetBool(IsMoving, isMoving);
        }
    }
}