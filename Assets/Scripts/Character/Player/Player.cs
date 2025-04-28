using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(CharacterRotater))]
[RequireComponent(typeof(InputReader))]
public class Player : MonoBehaviour
{
    private CharacterMover _mover;
    private CharacterRotater _rotater;
    private InputReader _inputReader;

    private void Awake()
    {
        _mover = GetComponent<CharacterMover>();
        _rotater = GetComponent<CharacterRotater>();
        _inputReader = GetComponent<InputReader>();
    }
    private void OnEnable()
    {
        _inputReader.JumpInputReading += _mover.Jump;
        _inputReader.HorizontalInputReading += _mover.Move;
        _inputReader.HorizontalInputReading += _rotater.Rotate;
    }

    private void OnDisable()
    {
        _inputReader.JumpInputReading -= _mover.Jump;
        _inputReader.HorizontalInputReading -= _mover.Move;
        _inputReader.HorizontalInputReading -= _rotater.Rotate;
    }
}
