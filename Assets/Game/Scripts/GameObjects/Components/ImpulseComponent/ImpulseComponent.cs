using System;
using System.Collections;
using UnityEngine;

namespace Game.ImpulseComponent
{
    public abstract class ImpulseComponent:MonoBehaviour
    {
   [SerializeField] protected PushComponent _pushComponent;
   [SerializeField] protected TossComponent _tossComponent;
        
    [SerializeField] protected KeyCode InputButton = KeyCode.None;
    [SerializeField] protected bool isPlayerCharecter = true;

    [SerializeField] protected float cooldown = 0.35f;
    [SerializeField] protected float delay = 0.15f;
    
    [SerializeField] protected float forceX = 1f;
    [SerializeField] protected float forceY = 10f;

    public Action<bool> OnImpulse;

   
    public bool CanImpulse;

    protected float _nextImpulseTime;
    protected bool _isPreparingImpulse;
    protected bool _wasButtonHeld;

    protected virtual void Update()
    {
        if (isPlayerCharecter && InputButton != KeyCode.None)
        {
            HandlePlayerInput();
        }
        else
        {
            HandleAiInput();
        }
    }

    private void HandlePlayerInput()
    {
        bool held = Input.GetKey(InputButton);

        if (held && !_wasButtonHeld && CanExecute())
        {
            var target = GetTarget();
            if (target != null)
                StartCoroutine(ExecuteWithDelay(target));
        }

        if (held != _wasButtonHeld)
        {
            OnImpulse?.Invoke(held);
        }
            

        _wasButtonHeld = held;
    }

    private void HandleAiInput()
    {
        if (!CanExecute()) return;

        var target = GetTarget();
        if (target != null)
            StartCoroutine(ExecuteWithDelay(target));
    }

    protected virtual bool CanExecute()
    {
        return CanImpulse
            && Time.time >= _nextImpulseTime
            && !_isPreparingImpulse;
    }

    
    protected abstract Rigidbody2D GetTarget();

    
    protected virtual void ApplyImpulse(Rigidbody2D rb)
    {
        if (rb == null) return;
        if (forceX != 0f) rb.AddForceX(forceX, ForceMode2D.Impulse);
        if (forceY != 0f) rb.AddForceY(forceY, ForceMode2D.Impulse);
    }

    protected virtual IEnumerator ExecuteWithDelay(Rigidbody2D rb)
    {
        _isPreparingImpulse = true;
        yield return new WaitForSeconds(delay);

        if (rb != null) ApplyImpulse(rb);

        _nextImpulseTime = Time.time + cooldown;
        _isPreparingImpulse = false;
    }
    
    public virtual void Impulse(float fx, float fy, Rigidbody2D rb)
    {
        if (rb == null) return;
        if (fx != 0f) rb.AddForceX(fx, ForceMode2D.Impulse);
        if (fy != 0f) rb.AddForceY(fy, ForceMode2D.Impulse);
    }
    }
}