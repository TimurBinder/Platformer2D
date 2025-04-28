using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyCollisionHandler : MonoBehaviour
{
    private Enemy _enemy;

    public event Action TargetEntered;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider == _enemy.CurrentTarget.Collider)
            TargetEntered?.Invoke();    
    }
}
