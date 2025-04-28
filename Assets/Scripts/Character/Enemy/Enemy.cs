using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(CharacterRotater))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private TargetPoint[] _targets;

    private int _currentTargetIndex;
    private CharacterMover _mover;
    private CharacterRotater _rotater;
    private EnemyCollisionHandler _colissionHandler;

    public TargetPoint CurrentTarget { get; private set; }

    private void Awake()
    {
        _mover = GetComponent<CharacterMover>();
        _rotater = GetComponent<CharacterRotater>();
        _colissionHandler = GetComponent<EnemyCollisionHandler>();
    }

    private void OnEnable()
    {
        _currentTargetIndex = 0;
        CurrentTarget = _targets[_currentTargetIndex];
        _colissionHandler.TargetEntered += SwitchTarget;
    }

    private void OnDisable()
    {
        _colissionHandler.TargetEntered -= SwitchTarget;
    }

    private void Update()
    {
        float direction = Mathf.Sign(CurrentTarget.transform.position.x - transform.position.x);
        _mover.Move(direction);
    }

    public void SwitchTarget()
    {
        _currentTargetIndex = ++_currentTargetIndex % _targets.Length;
        CurrentTarget = _targets[_currentTargetIndex];
        _rotater.Rotate(CurrentTarget.transform.position.x - transform.position.x);
    }
}
