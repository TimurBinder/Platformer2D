using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class GroundChecker : MonoBehaviour
{
    public bool HasGround { get; private set; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Ground>(out var ground))
            HasGround = true;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Ground>(out var ground))
            HasGround = false;
    }
}
