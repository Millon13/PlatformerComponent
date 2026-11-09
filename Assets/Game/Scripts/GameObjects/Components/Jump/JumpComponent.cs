using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class JumpComponent : MonoBehaviour
    {
        [SerializeField] private float force;

        [SerializeField] Rigidbody2D _rigidbody2D;

        
        public void Jump()
        { 
            if (force == 0 || _rigidbody2D == null) return;
            _rigidbody2D.AddForceY(force, ForceMode2D.Impulse);
        }
    }
}