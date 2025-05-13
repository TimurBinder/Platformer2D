using System;
using UnityEngine;

[RequireComponent(typeof(CharacterSensor))]
public class PersecutionTargetIdentifier : MonoBehaviour
{
    [SerializeField] private float _sensorDistance = 5f;
    [SerializeField] private float _persecutionDuration = 5f;

    private CharacterSensor _sensor;
    private float _persecutionTimer;
    private bool _isPersecuting;

    public event Action<Damageable> PersecutionStarted;
    public event Action PersecutionStoped;

    public Damageable Target { get; private set; }

    private void Awake()
    {
        _sensor = GetComponent<CharacterSensor>();
    }

    private void Update()
    {
        if (TryStartPersecution() == false && _isPersecuting)
        {
            _persecutionTimer += Time.deltaTime;
            TryStopPersecution();
        }
    }

    private bool TryStartPersecution()
    {
        if (TryGetTarget(out Damageable target) == false)
            return false;

        PersecutionStarted?.Invoke(target);
        Target = target;
        _isPersecuting = true;
        _persecutionTimer = 0;
        return true;
    }

    private void TryStopPersecution()
    {
        if (_persecutionTimer >= _persecutionDuration)
        {
            _isPersecuting = false;
            PersecutionStoped?.Invoke();
            Target = null;
        }
    }

    private bool TryGetTarget(out Damageable target)
    {
        if (_sensor.TryGetDamageable(_sensorDistance, out target))
            return target.TryGetComponent<Player>(out Player player);

        return false;
    }
}
