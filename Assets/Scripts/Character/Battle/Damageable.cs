using System;
using UnityEngine;

[RequireComponent (typeof(CharacterAnimator))]
public class Damageable : MonoBehaviour
{
    private CharacterAnimator _animator;

    public event Action<float> TakedDamage;

    private void Awake()
    {
        _animator = GetComponent<CharacterAnimator>();
    }

    public void TakeDamage(float damage)
    {
        _animator.TakeDamage();
        TakedDamage?.Invoke(damage);
    }
}
