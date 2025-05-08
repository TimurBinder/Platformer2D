using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Jump = nameof(Jump);
    private const string Attack = nameof(Attack);
    private const string Horizontal = nameof(Horizontal);

    public event Action JumpInputReading;
    public event Action AttackInputReading;
    public event Action<float> HorizontalInputReading;

    private void Update()
    {
        bool jumpInput = Input.GetButtonDown(Jump);
        bool attackInput = Input.GetButtonDown(Attack);
        float horizontalInput = Input.GetAxis(Horizontal);

        if (jumpInput)
            JumpInputReading?.Invoke();

        if (attackInput)
            AttackInputReading?.Invoke();

        if (horizontalInput != 0)
            HorizontalInputReading?.Invoke(horizontalInput);
    }
}
