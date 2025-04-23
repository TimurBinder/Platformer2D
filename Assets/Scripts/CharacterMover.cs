using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(CharacterAnimatorData))]
public class CharacterMover : MonoBehaviour
{
    public event Action<float> Moving;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private GroundChecker _groundChecker;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected void Move(float direction)
    {
        float distance = direction * _speed * Time.deltaTime;
        transform.Translate(Vector2.right * distance);
        Moving?.Invoke(direction);
    }

    protected void Jump()
    {
        if (_groundChecker.HasGround)
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}
