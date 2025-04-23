using System;
using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    public event Action<Coin> Collected;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<CoinCollector>(out var collector))
            Collected.Invoke(this);
    }
}
