using System.Collections;
using UnityEngine;

public class Vampirism : Skill
{
    [SerializeField] private float _damage;
    [SerializeField] private float _damageRepeatRate;
    [SerializeField] private VampirismArea _area;
    [SerializeField] private VampirismAnimator _animator;
    [SerializeField] private Health _health;

    private IEnumerator _executingCoroutine;
    private WaitForSeconds _damageRepeatWait;

    private void Awake()
    {
        _damageRepeatWait = new WaitForSeconds(_damageRepeatRate);
        _executingCoroutine = Executing();
    }

    protected override void Update()
    {
        base.Update();

        if (DurationTimer <= 0)
            Deactivate();
    }

    public override void Activate()
    {
        if (IsActivated == false && ReloadTimer > RepeatRate)
        {
            IsActivated = true;
            _animator.Toggle(true);
            ReloadTimer = 0;
            DurationTimer = Duration;
            StartCoroutine(_executingCoroutine);
        }
    }

    private void Deactivate()
    {
        if (IsActivated)
        {
            _animator.Toggle(false);
            StopCoroutine(_executingCoroutine);
            IsActivated = false;
        }
    }

    private IEnumerator Executing()
    {
        while (IsActivated)
        {
            if (_area.TryGetDamageable(out Damageable damageable))
            {
                damageable.TakeDamage(_damage, false);
                _health.Add(_damage);
            }

            yield return _damageRepeatWait;
        }
    }
}