using UnityEngine;

public class EnemyMover : CharacterMover
{
    [SerializeField] private TargetPoint[] _targets;

    private int _currentTargetIndex;
    private TargetPoint _currentTarget;

    private void OnEnable()
    {
        _currentTargetIndex = 0;
        _currentTarget = _targets[_currentTargetIndex];
    }

    private void Update()
    {
        float direction = Mathf.Sign(_currentTarget.transform.position.x - transform.position.x);
        Move(direction);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider == _currentTarget.Collider)
            SetNextTarget();
    }

    private void SetNextTarget()
    {
        _currentTargetIndex = ++_currentTargetIndex % _targets.Length;
        _currentTarget = _targets[_currentTargetIndex];
    }
}
