using System;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(Health))]
public class Damageable : MonoBehaviour
{
    private CharacterAnimator _animator;
    private Health _health;

    public event Action<float> TakedDamage;

    private void Awake()
    {
        _animator = GetComponent<CharacterAnimator>();
        _health = GetComponent<Health>();
    }

    public void TakeDamage(float damage)
    {
        _animator.TakeDamage();
        TakedDamage?.Invoke(damage);
        _health.Reduce(damage);
    }
}
