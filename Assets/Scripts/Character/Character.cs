using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class Character : MonoBehaviour
{
    [SerializeField] private float _health;

    protected Damageable _damageable;

    public float MaxHealth => _health;
    public float Health { get; protected set; }
    public bool IsAlive => Health > 0;

    protected virtual void Awake()
    {
        Health = MaxHealth;
        _damageable = GetComponent<Damageable>();
    }

    protected virtual void OnEnable()
    {
        _damageable.TakedDamage += ReduceHealth;
    }

    protected virtual void OnDisable()
    {
        _damageable.TakedDamage -= ReduceHealth;
    }

    private void ReduceHealth(float health)
    {
        Health -= health;

        if (IsAlive == false)
            Destroy(gameObject);
    }
}
