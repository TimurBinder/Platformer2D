using System;
using UnityEngine;

[RequireComponent (typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    public static readonly int Speed = Animator.StringToHash(nameof(Speed));
    public static readonly int Attacking = Animator.StringToHash(nameof(Attacking));
    public static readonly int TakingDamage = Animator.StringToHash(nameof(TakingDamage));
    public static readonly int IsAlive = Animator.StringToHash(nameof(IsAlive));

    private Animator _animator;

    public event Action AttackEnding;
    public event Action TakeDamageEnding;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Move(float speed)
    {
        _animator.SetFloat(Speed, speed);
    }

    public void Attack()
    {
        _animator.SetTrigger(Attacking);
    }

    public void EndAttack()
    {
        AttackEnding?.Invoke();
    }

    public void TakeDamage()
    {
        _animator.SetTrigger(TakingDamage);
    }

    public void EndTakeDamage()
    {
        TakeDamageEnding?.Invoke();
    }

    public void Die()
    {
        _animator.SetBool(IsAlive, false);
    }
}
