using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class JumpComponent : MonoBehaviour
    {
        [SerializeField] private float force = 12f;

        [SerializeField] Rigidbody2D _rigidbody2D;

//[SerializeField] float cooldown = 0.25f;

       // [SerializeField] float delay = 0.05f;
        
        public void Jump()
        { 
            if (force == 0 || _rigidbody2D == null) return;
            _rigidbody2D.AddForceY(force, ForceMode2D.Impulse);
        }
    }
}