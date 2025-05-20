using UnityEngine;

public abstract class HealthIndicator : MonoBehaviour
{
    [SerializeField] protected Health Health;

    protected virtual void Awake()
    {
        SetMax();
        SetCurrent();
    }

    private void OnEnable()
    {
        Health.Changed += SetCurrent;
    }

    private void OnDisable()
    {
        Health.Changed -= SetCurrent;
    }

    public virtual void SetHealth(Health health)
    {
        Health = health;
        SetMax();
        SetCurrent();
    }

    protected virtual void SetMax() {}

    protected virtual void SetCurrent() {}
}
