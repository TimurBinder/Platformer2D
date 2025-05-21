using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(CharacterRotater))]
[RequireComponent(typeof(EnemyCollisionHandler))]
[RequireComponent(typeof(PatrolTargetIdentifier))]
[RequireComponent(typeof(PersecutionTargetIdentifier))]
[RequireComponent(typeof(Attacker))]
public class Enemy : Character
{
    [SerializeField] float _attackDelay = 2f;

    private CharacterMover _mover;
    private CharacterRotater _rotater;
    private EnemyCollisionHandler _colissionHandler;
    private PatrolTargetIdentifier _patrulTargetIdentifier;
    private PersecutionTargetIdentifier _persecutionTargetIdentifier;
    private Attacker _attacker;
    private Transform _currentTargetTransform;
    private WaitForSeconds _attackWait;
    private IEnumerator _attackCoroutine;
    private bool _isAttacking;

    private void Awake()
    {
        _mover = GetComponent<CharacterMover>();
        _rotater = GetComponent<CharacterRotater>();
        _colissionHandler = GetComponent<EnemyCollisionHandler>();
        _attacker = GetComponent<Attacker>();
        _patrulTargetIdentifier = GetComponent<PatrolTargetIdentifier>();
        _persecutionTargetIdentifier = GetComponent<PersecutionTargetIdentifier>();
        _attackWait = new WaitForSeconds(_attackDelay);
    }

    private void OnEnable()
    {
        _patrulTargetIdentifier.TargetChanged += SetTarget;
        _persecutionTargetIdentifier.PersecutionStarted += StartPersecuting;
        _persecutionTargetIdentifier.PersecutionStoped += StopPersecuting;
        _colissionHandler.DamageableEntered += StartAttackTarget;
        _colissionHandler.DamageableExited += StopAttackTarget;
        _currentTargetTransform = _patrulTargetIdentifier.CurrentTarget.transform;
    }

    private void OnDisable()
    {
        _patrulTargetIdentifier.TargetChanged -= SetTarget;
        _persecutionTargetIdentifier.PersecutionStarted -= StartPersecuting;
        _persecutionTargetIdentifier.PersecutionStoped -= StopPersecuting;
        _colissionHandler.DamageableEntered -= StartAttackTarget;
        _colissionHandler.DamageableExited -= StopAttackTarget;
    }

    private void Update()
    {
        float direction = 0;

        if (_isAttacking == false)
            direction = Mathf.Sign(_currentTargetTransform.position.x - transform.position.x);

        _mover.Move(direction);
        float rotation = _currentTargetTransform.position.x - transform.position.x;
        _rotater.Rotate(rotation);
    }

    private void SetTarget(Transform targetTransform)
    {
        _currentTargetTransform = targetTransform;
    }

    private void StartPersecuting(Damageable damageable)
    {
        _currentTargetTransform = damageable.transform;
        _patrulTargetIdentifier.TargetChanged -= SetTarget;
    }

    private void StopPersecuting()
    {
        _patrulTargetIdentifier.enabled = true;
        _patrulTargetIdentifier.TargetChanged += SetTarget;
        _currentTargetTransform = _patrulTargetIdentifier.CurrentTarget.transform;
    }

    private void StartAttackTarget(Damageable damageable)
    {
        if (damageable == _persecutionTargetIdentifier.Target)
        {
            _attackCoroutine = Attacking();
            StartCoroutine(_attackCoroutine);
            _isAttacking = true;
        }
    }

    private void StopAttackTarget(Damageable damageable)
    {
        if (_attackCoroutine != null)
            StopCoroutine(_attackCoroutine);

        _isAttacking = false;
    }

    private IEnumerator Attacking() 
    {
        while (enabled)
        {
            _attacker.Attack();
            yield return _attackWait;
        }
    }
}
