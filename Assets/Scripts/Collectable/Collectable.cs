using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectable : MonoBehaviour
{
    public virtual event Action<Collectable> Collected;

    public void Collect()
    {
        Collected?.Invoke(this);
    }
}
