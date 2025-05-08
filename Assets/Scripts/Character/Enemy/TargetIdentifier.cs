using System;
using UnityEngine;

[RequireComponent(typeof(CharacterSensor))]
[RequireComponent(typeof(EnemyCollisionHandler))]
public class TargetIdentifier : MonoBehaviour
{
    [SerializeField] private float _sensorDistance = 5f;
    [SerializeField] private float _persecutionDuration = 5f;
    [SerializeField] private TargetPoint[] _targets;
    [SerializeField] private Damageable _attackTarget;

    private int _currentTargetIndex;
    private bool _isPersecution;
    private float _persecutionTimer;
    private CharacterSensor _sensor;
    private EnemyCollisionHandler _collisionHandler;

    public event Action<Transform> TargetChanged;

    public Damageable AttackTarget => _attackTarget;
    public TargetPoint CurrentTarget { get; private set; }

    private void Awake()
    {
        _collisionHandler = GetComponent<EnemyCollisionHandler>();
        _sensor = GetComponent<CharacterSensor>();
        _isPersecution = false;
    }

    private void OnEnable()
    {
        _collisionHandler.TargetEntered += SwitchTarget;
    }

    private void Start()
    {
        _currentTargetIndex = 0;
        CurrentTarget = _targets[_currentTargetIndex];
        TargetChanged?.Invoke(CurrentTarget.transform);
    }

    private void OnDisable()
    {
        _collisionHandler.TargetEntered -= SwitchTarget;
    }

    private void Update()
    {
        bool hasDamageable = false;

        if (_sensor.TryGetDamageable(_sensorDistance, out Damageable damageable))
        {
            if (damageable == _attackTarget)
            {
                _isPersecution = true;
                hasDamageable = true;
                _persecutionTimer = 0;
                TargetChanged?.Invoke(damageable.transform);
            }
        }

        if (hasDamageable == false && _isPersecution)
        {
            _persecutionTimer += Time.deltaTime;
            
            if (_persecutionTimer >= _persecutionDuration)
            {
                _isPersecution = false;
                SwitchTarget(CurrentTarget);
            }
        }
    }

    private void SwitchTarget(TargetPoint targetPoint)
    {
        if (_isPersecution)
            return;

        if (targetPoint == CurrentTarget)
        {
            _currentTargetIndex = ++_currentTargetIndex % _targets.Length;
            CurrentTarget = _targets[_currentTargetIndex];
            TargetChanged?.Invoke(CurrentTarget.transform);
        }
    }
}
