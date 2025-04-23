using UnityEngine;

public class CharacterAnimatorData : MonoBehaviour
{
    public static readonly int Speed = Animator.StringToHash(nameof(Speed));
    public static readonly int Jumping = Animator.StringToHash(nameof(Jumping));

    private Animator _animator;
    private CharacterMover _mover;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _mover = GetComponent<CharacterMover>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _mover.Moving += Move;
    }

    private void Move(float speed)
    {
        _animator.SetFloat(Speed, speed);
        _renderer.flipX = speed < 0f;
    }
}
