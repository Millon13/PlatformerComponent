using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class MoveTransformComponentView: MonoBehaviour
    {
        [SerializeField] private MoveTransformComponent _moveTransformComponent;

        [SerializeField] private Animator _animator;
        
        private void Awake()
        {
            _moveTransformComponent = GetComponent<MoveTransformComponent>();
        }

        private void OnEnable()
        {
            _moveTransformComponent.OnMove+=OnMove;
        }

        private void OnDisable()
        {
            _moveTransformComponent.OnMove-=OnMove;
        }

        private void OnMove(bool IsMove)
        {
            _animator.SetBool("IsMoving", IsMove);
        }
        
    }
}