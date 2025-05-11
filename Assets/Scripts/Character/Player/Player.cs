using System;
using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(CharacterRotater))]
[RequireComponent(typeof(InputReader))]
[RequireComponent (typeof(Attacker))]
[RequireComponent(typeof(Collector))]
public class Player : Character
{
    private CharacterMover _mover;
    private CharacterRotater _rotater;
    private Attacker _attacker;
    private InputReader _inputReader;
    private Collector _collector;

    public event Action Died;

    protected override void Awake()
    {
        base.Awake();
        _mover = GetComponent<CharacterMover>();
        _rotater = GetComponent<CharacterRotater>();
        _inputReader = GetComponent<InputReader>();
        _attacker = GetComponent<Attacker>();
        _collector = GetComponent<Collector>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        _inputReader.JumpInputReading += _mover.Jump;
        _inputReader.HorizontalInputReading += _mover.Move;
        _inputReader.HorizontalInputReading += _rotater.Rotate;
        _inputReader.AttackInputReading += _attacker.Attack;
        _collector.HealthKitCollected += AddHealth;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _inputReader.JumpInputReading -= _mover.Jump;
        _inputReader.HorizontalInputReading -= _mover.Move;
        _inputReader.HorizontalInputReading -= _rotater.Rotate;
        _inputReader.AttackInputReading -= _attacker.Attack;
        _collector.HealthKitCollected -= AddHealth;
    }

    private void OnDestroy()
    {
        Died?.Invoke();
    }

    private void AddHealth(float health)
    {
        Health += health;

        if (Health > MaxHealth)
            Health = MaxHealth;
    }
}
