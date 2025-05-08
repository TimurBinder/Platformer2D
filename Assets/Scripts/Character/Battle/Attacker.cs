using UnityEngine;

[RequireComponent (typeof(Animator))]
[RequireComponent(typeof(CharacterSensor))]
public class Attacker : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _delay = 0.5f;
    [SerializeField] private float _distance = 2.5f;

    private CharacterAnimator _animator;
    private CharacterSensor _sensor;
    private float _delayTimer;

    private void Awake()
    {
        _animator = GetComponent<CharacterAnimator>();
        _sensor = GetComponent<CharacterSensor>();
    }

    public void Update()
    {
        _delayTimer += Time.deltaTime;
    }

    public void Attack()
    {
        if (_delayTimer < _delay)
            return;

        _animator.Attack();
        _animator.AttackEnding += CauseDamage;
    }

    private void CauseDamage()
    {
        if (_sensor.TryGetDamageable(_distance, out Damageable damageable))
            damageable.TakeDamage(_damage);

        _animator.AttackEnding -= CauseDamage;
        _delayTimer = 0;
    }
}
