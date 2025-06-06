using UnityEngine;

[RequireComponent(typeof(Animator))]
public class VampirismAnimator : MonoBehaviour
{
    public static readonly int IsEnable = Animator.StringToHash(nameof(IsEnable));

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Toggle(bool isEnable)
    {
        _animator.SetBool(IsEnable, isEnable);
    }
}
