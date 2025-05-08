using System;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    public event Action<TargetPoint> TargetEntered;
    public event Action<Damageable> DamageableEntered;
    public event Action<Damageable> DamageableExited;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<TargetPoint>(out TargetPoint targetPoint))
            TargetEntered.Invoke(targetPoint);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Damageable>(out Damageable damageable))
            DamageableEntered?.Invoke(damageable);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Damageable>(out Damageable damageable))
            DamageableExited?.Invoke(damageable);
    }
}
