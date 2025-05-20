using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Damageable : MonoBehaviour
{
    [SerializeField] private CharacterAnimator _animator;
    private Health _health;

    public event Action<float> TakedDamage;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Overed += Die;
    }

    private void OnDisable()
    {
        _health.Overed -= Die;
    }

    public void TakeDamage(float damage)
    {
        _animator.TakeDamage();
        TakedDamage?.Invoke(damage);
        _health.Reduce(damage);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
