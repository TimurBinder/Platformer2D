using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _value;

    public float MaxValue => _value;
    public float Value { get; private set; }    
    public bool IsAlive => Value > 0;

    public event Action Changed;
    public event Action Overed;

    private void Awake()
    {
        Value = MaxValue;
    }

    public void Reduce(float health)
    {
        if (health < 0)
            return;

        Value -= health;

        if (Value < 0)
            Value = 0;

        Changed?.Invoke();

        if (IsAlive == false)
            Overed?.Invoke();
    }

    public void Add(float health)
    {
        if (health < 0)
            return;

        Value += health;

        if (Value > MaxValue)
            Value = MaxValue;

        Changed?.Invoke();
    }
}
