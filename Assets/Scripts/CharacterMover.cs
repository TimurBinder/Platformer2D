using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(CharacterAnimator))]
[RequireComponent(typeof(CharacterRotater))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private GroundChecker _groundChecker;

    private CharacterAnimator _animator;
    private Rigidbody2D _rigidbody;
    private CharacterRotater _rotater;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<CharacterAnimator>();
        _rotater = GetComponent<CharacterRotater>();
    }

    protected void Move(float direction)
    {
        float distance = Mathf.Abs(direction) * _speed * Time.deltaTime;
        transform.Translate(Vector2.right * distance);
        _animator.Move(direction);
        _rotater.SetDirection(direction);
    }

    protected void Jump()
    {
        if (_groundChecker.HasGround)
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}
