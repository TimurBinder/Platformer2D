using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Character))]
public class Damageable : MonoBehaviour
{
    [SerializeField] private CharacterAnimator _animator;

    private Health _health;
    private Character _character;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    public event Action<float> TakedDamage;
    public event Action Died;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _character = GetComponent<Character>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        _health.Overed += Die;
    }

    private void OnDisable()
    {
        _health.Overed -= Die;
    }

    public void TakeDamage(float damage, bool isStunning=true)
    {
        TakedDamage?.Invoke(damage);
        _health.Reduce(damage);
        _animator.TakeDamage();

        if (isStunning)
        {
            if (_health.IsAlive)
                _animator.TakeStunningDamage();
        }
    }

    private void Die()
    {
        Died?.Invoke();
        _animator.Die();
        _character.enabled = false;
        _rigidbody.bodyType = RigidbodyType2D.Static;
        _collider.enabled = false;
    }
}
