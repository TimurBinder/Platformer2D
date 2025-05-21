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
        if (TryGetTarget(out Damageable target))
        {
            if (_isPersecuting)
                _persecutionTimer = 0;
            else
                StartPersecution(target);
        }
        else if (_isPersecuting)
        {
            _persecutionTimer += Time.deltaTime;

            if (_persecutionTimer >= _persecutionDuration)
                StopPersecution();
        }
    }

    private void StartPersecution(Damageable target)
    {
        PersecutionStarted?.Invoke(target);
        Target = target;
        Target.Died += StopPersecution;
        _isPersecuting = true;
        _persecutionTimer = 0;
    }

    private void StopPersecution()
    {
        _isPersecuting = false;
        PersecutionStoped?.Invoke();
        Target.Died -= StopPersecution;
        Target = null;
    }

    private bool TryGetTarget(out Damageable target)
    {
        if (_sensor.TryGetDamageable(_sensorDistance, out target))
            return target.TryGetComponent<Player>(out Player player);

        return false;
    }
}
