using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private CharacterAnimator _animator;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float direction)
    {
        float distance = direction * _speed * Time.deltaTime;
        transform.Translate(Vector2.right * distance);
        _animator.Move(Mathf.Abs(direction));
    }

    public void Jump()
    {
        if (_groundChecker.HasGround)
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}
