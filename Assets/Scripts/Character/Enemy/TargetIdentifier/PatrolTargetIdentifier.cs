using System;
using UnityEngine;

[RequireComponent(typeof(EnemyCollisionHandler))]
public class PatrolTargetIdentifier : MonoBehaviour
{
    [SerializeField] private TargetPoint[] _targets;

    private int _currentTargetIndex;
    private EnemyCollisionHandler _collisionHandler;

    public event Action<Transform> TargetChanged;

    public TargetPoint CurrentTarget { get; private set; }

    private void Awake()
    {
        _collisionHandler = GetComponent<EnemyCollisionHandler>();
        _currentTargetIndex = 0;
        CurrentTarget = _targets[_currentTargetIndex];
    }

    private void OnEnable()
    {
        _collisionHandler.TargetEntered += SwitchTarget;
    }

    private void OnDisable()
    {
        _collisionHandler.TargetEntered -= SwitchTarget;
    }

    private void SwitchTarget(TargetPoint targetPoint)
    {
        if (targetPoint == CurrentTarget)
        {
            _currentTargetIndex = ++_currentTargetIndex % _targets.Length;
            CurrentTarget = _targets[_currentTargetIndex];
            TargetChanged?.Invoke(CurrentTarget.transform);
        }
    }
}
