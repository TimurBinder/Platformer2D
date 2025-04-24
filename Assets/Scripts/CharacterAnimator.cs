using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public static readonly int Speed = Animator.StringToHash(nameof(Speed));

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Move(float speed)
    {
        _animator.SetFloat(Speed, speed);
    }
}
