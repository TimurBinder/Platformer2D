using UnityEngine;

[RequireComponent (typeof(Animator))]
[RequireComponent(typeof(CharacterSensor))]
public class Attacker : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _distance = 2.5f;

    private CharacterAnimator _animator;
    private CharacterSensor _sensor;
    private bool _canAttack;

    private void Awake()
    {
        _animator = GetComponent<CharacterAnimator>();
        _sensor = GetComponent<CharacterSensor>();
        _canAttack = true;
    }

    public void Attack()
    {
        if (_canAttack)
        {
            _canAttack = false;
            _animator.Attack();
            _animator.AttackEnding += CauseDamage;
        }
    }

    private void CauseDamage()
    {
        if (_sensor.TryGetDamageable(_distance, out Damageable damageable))
            damageable.TakeDamage(_damage);

        _animator.AttackEnding -= CauseDamage;
        _canAttack = true;
    }
}
