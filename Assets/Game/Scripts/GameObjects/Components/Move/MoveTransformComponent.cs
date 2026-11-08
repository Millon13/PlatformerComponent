using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Game

{
    [RequireComponent(typeof(LookComponent))]
    public sealed class MoveTransformComponent : MonoBehaviour
    {
        
        [SerializeField] private float _moveSpeed = 4.5f;
        public void MoveStep(Vector3 moveDirection)
        {
            this.transform.position += moveDirection * _moveSpeed * Time.fixedDeltaTime;
        }
    }
}