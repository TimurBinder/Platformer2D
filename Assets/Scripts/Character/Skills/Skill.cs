using UnityEngine;

abstract public class Skill : MonoBehaviour
{
    [SerializeField] private float _duration = 0;
    [SerializeField] private float _repeatRate;

    public float Duration => _duration;
    public float RepeatRate => _repeatRate;
    public float DurationTimer { get; protected set; }
    public float ReloadTimer { get; protected set; }
    public bool IsActivated { get; protected set; }

    protected virtual void Update()
    {
        if (IsActivated)
            DurationTimer -= Time.deltaTime;
        else
            ReloadTimer += Time.deltaTime;
    }

    public virtual void Activate() { }
}
