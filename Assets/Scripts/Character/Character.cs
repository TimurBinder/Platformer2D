using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class Character : MonoBehaviour
{
    [SerializeField] private float _health;

    protected Damageable Damageable;

    public float MaxHealth => _health;
    public float Health { get; protected set; }
    public bool IsAlive => Health > 0;

    protected virtual void Awake()
    {
        Health = MaxHealth;
        Damageable = GetComponent<Damageable>();
    }

    protected virtual void OnEnable()
    {
        Damageable.TakedDamage += ReduceHealth;
    }

    protected virtual void OnDisable()
    {
        Damageable.TakedDamage -= ReduceHealth;
    }

    private void ReduceHealth(float health)
    {
        Health -= health;

        if (IsAlive == false)
            Destroy(gameObject);
    }
}
