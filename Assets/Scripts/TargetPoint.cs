using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class TargetPoint : MonoBehaviour
{
    public Collider2D Collider { get; private set; }

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
    }
}
