using UnityEngine;

public class HealthKit : Collectable
{
    [SerializeField] private float _health;

    public float Health => _health;
}
