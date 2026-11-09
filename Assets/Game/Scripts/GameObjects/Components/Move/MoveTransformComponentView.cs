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
            _moveComponent.OnMoving += OnMove;
        }

        private void OnDisable()
        {
            _moveComponent.OnMoving -= OnMove;
        }

        private void OnMove(bool isMoving)
        {

            _animator.SetBool(IsMoving, isMoving);


        }
    }
}