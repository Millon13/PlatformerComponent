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
        public bool MoveTowards(Vector3 target, float speed, float arriveThreshold = 0.05f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                speed * Time.fixedDeltaTime
            );

            return (transform.position - target).sqrMagnitude <= arriveThreshold * arriveThreshold;
        }
    }
}