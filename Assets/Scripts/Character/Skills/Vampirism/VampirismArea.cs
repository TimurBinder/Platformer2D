using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class VampirismArea : MonoBehaviour
{
    [SerializeField] private float _range;

    private int _enemyMask;

    private void Awake()
    {
        _enemyMask = LayerMask.GetMask("Enemy");
        float diameter = _range * 2;
        transform.localScale = new Vector2(diameter, diameter);
    }

    public bool TryGetDamageable(out Damageable damageable)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _range, _enemyMask);
        Collider2D collider;

        if (colliders.Length == 0)
        {
            damageable = null;
            return false;
        }

        if (colliders.Length == 1)
            collider = colliders[0];
        else
            collider = GetNearestCollider(colliders);

        damageable = collider.GetComponent<Damageable>();
        return true;
    }

    private Collider2D GetNearestCollider(Collider2D[] colliders)
    {
        float minDistance = float.MaxValue;
        Collider2D nearestCollider = null;

        foreach (var collider in colliders)
        {
            if (collider == transform.parent)
                continue;

            float distance = (transform.position - collider.transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestCollider = collider;
            }
        }

        return nearestCollider;
    }
}
