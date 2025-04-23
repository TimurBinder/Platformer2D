using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const  KeyCode JumpKey = KeyCode.UpArrow;
    private const string Horizontal = nameof(Horizontal);

    public event Action JumpInputReading;
    public event Action<float> HorizontalInputReading;

    private void Update()
    {
        bool verticalInput = Input.GetKeyDown(JumpKey);
        float horizontalInput = Input.GetAxis(Horizontal);

        if (verticalInput)
            JumpInputReading?.Invoke();

        if (horizontalInput != 0)
            HorizontalInputReading?.Invoke(horizontalInput);
    }
}
