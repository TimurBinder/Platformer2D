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
        Move(GetDirection());
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

    private int GetDirection()
    {
        float direction = _currentTarget.transform.position.x - transform.position.x;

        if (direction > 0)
            return 1;
        else if (direction < 0) 
            return -1;
        else return 0;
    }
}
