using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class MoveTransformComponentView : MonoBehaviour
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        [SerializeField] private MoveRequestComponent _moveComponent;

        [SerializeField] private Animator _animator;

        private void Awake()
        {
           // _moveTransformComponent = GetComponent<MoveTransformComponent>();
        }

        private void OnEnable()
        {
            _moveComponent.OnMoved += OnMove;
        }

        private void OnDisable()
        {
            _moveComponent.OnMoved -= OnMove;
        }

        private void OnMove(Vector3  moveDirection)
        {
            if (moveDirection != Vector3.zero)
            {
                _animator.SetBool(IsMoving, _moveComponent.IsMoving);
            }
            
        }
    }
}