using System;
using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(CharacterRotater))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(Collector))]
[RequireComponent(typeof(Health))]
public class Player : Character
{
    private CharacterMover _mover;
    private CharacterRotater _rotater;
    private Attacker _attacker;
    private InputReader _inputReader;
    private Collector _collector;
    private Health _health;

    public event Action Died;

    private void Awake()
    {
        _mover = GetComponent<CharacterMover>();
        _rotater = GetComponent<CharacterRotater>();
        _inputReader = GetComponent<InputReader>();
        _attacker = GetComponent<Attacker>();
        _collector = GetComponent<Collector>();
        _health = GetComponent<Health>();
    }
    private void OnEnable()
    {
        _inputReader.JumpInputReading += _mover.Jump;
        _inputReader.HorizontalInputReading += _mover.Move;
        _inputReader.HorizontalInputReading += _rotater.Rotate;
        _inputReader.AttackInputReading += _attacker.Attack;
        _collector.HealthKitCollected += _health.Add;
    }

    private void OnDisable()
    {
        _inputReader.JumpInputReading -= _mover.Jump;
        _inputReader.HorizontalInputReading -= _mover.Move;
        _inputReader.HorizontalInputReading -= _rotater.Rotate;
        _inputReader.AttackInputReading -= _attacker.Attack;
        _collector.HealthKitCollected -= _health.Add;
    }

    private void OnDestroy()
    {
        Died?.Invoke();
    }
}
