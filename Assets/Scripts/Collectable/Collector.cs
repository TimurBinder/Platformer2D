using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collector : MonoBehaviour
{
    public event Action CoinCollected;
    public event Action<float> HealthKitCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Collectable>(out Collectable collectable))
        {
            collectable.Collect();

            if (collectable is HealthKit healthKit)
                HealthKitCollected?.Invoke(healthKit.Health);
            else if (collectable is Coin coin)
                CoinCollected?.Invoke();
        }
    }
}
