using System;
using UnityEngine;

public class HealthKitCollector : MonoBehaviour
{
    public event Action<float> HealthKitCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<HealthKit>(out HealthKit healthKit))
        {
            healthKit.Collect();
            HealthKitCollected?.Invoke(healthKit.Health);
        }
    }
}
