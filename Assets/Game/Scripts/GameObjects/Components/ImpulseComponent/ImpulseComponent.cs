using System;
using System.Collections;
using UnityEngine;

namespace Game.ImpulseComponent
{
    public class ImpulseComponent:MonoBehaviour
    {
    public void Impulse(float fx, float fy, Rigidbody2D rb)
    {
        if (rb == null) return;
        if (fx != 0f) rb.AddForceX(fx, ForceMode2D.Impulse);
        if (fy != 0f) rb.AddForceY(fy, ForceMode2D.Impulse);
    }
    }
}