using UnityEngine;
using UnityEngine.Events;

public class Characte : StateMachineBehaviour
{
    public event UnityAction AttackEnding;

    private void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Attack"))
            AttackEnding?.Invoke();
    }
}
