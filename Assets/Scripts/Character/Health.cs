using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health;

    public float MaxValue => _health;
    public float Value { get; private set; }    
    public bool IsAlive => Value > 0;

    protected virtual void Awake()
    {
        Value = MaxValue;
    }

    public void Reduce(float health)
    {
        if (health < 0)
            return;

        Value -= health;

        if (IsAlive == false)
            Destroy(gameObject);
    }

    public void Add(float health)
    {
        if (health < 0)
            return;

        Value += health;

        if (Value > MaxValue)
            Value = MaxValue;
    }
}
