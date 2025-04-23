using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : CharacterMover
{
    [SerializeField] InputReader _inputReader;

    private void OnEnable()
    {
        _inputReader.JumpInputReading += Jump;
        _inputReader.HorizontalInputReading += Move;
    }

    private void OnDisable()
    {
        _inputReader.JumpInputReading -= Jump;
        _inputReader.HorizontalInputReading -= Move;
    }
}
    