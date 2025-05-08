using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(CharacterRotater))]
[RequireComponent(typeof(EnemyCollisionHandler))]
[RequireComponent(typeof(TargetIdentifier))]
[RequireComponent(typeof(Attacker))]
public class Enemy : Character
{
    [SerializeField] float _attackDelay = 2f;

    private CharacterMover _mover;
    private CharacterRotater _rotater;
    private EnemyCollisionHandler _colissionHandler;
    private TargetIdentifier _targetIdentifier;
    private Attacker _attacker;
    private Transform _currentTargetTransform;
    private WaitForSeconds _attackWait;
    private IEnumerator _attackCoroutine;
    private bool _isAttacking;

    protected override void Awake()
    {
        base.Awake();
        _mover = GetComponent<CharacterMover>();
        _rotater = GetComponent<CharacterRotater>();
        _targetIdentifier = GetComponent<TargetIdentifier>();
        _colissionHandler = GetComponent<EnemyCollisionHandler>();
        _attacker = GetComponent<Attacker>();
        _attackWait = new WaitForSeconds(_attackDelay);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _targetIdentifier.TargetChanged += SetTarget;
        _colissionHandler.DamageableEntered += StartAttackTarget;
        _colissionHandler.DamageableExited += StopAttackTarget;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _targetIdentifier.TargetChanged -= SetTarget; 
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

    private void StartAttackTarget(Damageable damageable)
    {
        if (damageable == _targetIdentifier.AttackTarget)
        {
            _attackCoroutine = Attacking(damageable);
            StartCoroutine(_attackCoroutine);
            _isAttacking = true;
        }
    }

    private void StopAttackTarget(Damageable damageable)
    {
        if (_isAttacking && damageable == _targetIdentifier.AttackTarget)
        {
            StopCoroutine(_attackCoroutine);
            _isAttacking = false;
        }
    }

    private IEnumerator Attacking(Damageable damageable) 
    {
        while (enabled)
        {
            _attacker.Attack();
            yield return _attackWait;
        }
    }
}
