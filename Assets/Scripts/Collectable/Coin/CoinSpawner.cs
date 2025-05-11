using System.Collections;
using UnityEngine;

public class CoinSpawner : CollectableSpawner
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private Transform[] _startPoints;

    protected override void Awake()
    {
        MaxSize = _startPoints.Length;
        DefaultCapacity = _startPoints.Length;
        Prefab = _prefab;

        base.Awake();

        for (int i = 0; i < _startPoints.Length; i++)
            Pool.Get();
    }

    protected override Collectable OnActionCreate()
    {
        return Instantiate(Prefab, _startPoints[Pool.CountActive].position, Quaternion.identity);
    }

    protected override void OnActionRelease(Collectable coin)
    {
        base.OnActionRelease(coin);
        StartCoroutine(Getting());
    }
}
