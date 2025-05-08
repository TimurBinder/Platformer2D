using System;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    [SerializeField] private float _health;

    public event Action<HealthKit> Collected;

    public float Health => _health;

    public void Collect()
    {
        Collected?.Invoke(this);
    }
}
