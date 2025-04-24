using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class GroundChecker : MonoBehaviour
{
    private int _groundEnterCount = 0;

    public bool HasGround => _groundEnterCount > 0;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Ground>(out var ground))
            _groundEnterCount++;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Ground>(out var ground))
            _groundEnterCount--;
    }
}
