using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CharacterSensor : MonoBehaviour
{
    [SerializeField] private CharacterRenderer _renderer;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    public bool TryGetDamageable(float raycastDistance, out Damageable damageable)
    {
        Vector2 direction = Vector2.right;

        if (_renderer.transform.rotation.eulerAngles.y != 0)
            direction = Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(_collider.bounds.center, direction, raycastDistance);

        if (hit.collider == null)
        {
            damageable = null;
            return false;
        }
        else
        {
            return hit.collider.TryGetComponent<Damageable>(out damageable);
        }
    }
}
