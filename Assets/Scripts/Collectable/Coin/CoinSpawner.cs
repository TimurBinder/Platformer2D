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
        {
            Collectable coin = Pool.Get();
            coin.transform.position = _startPoints[i].position;
        }
    }

    protected override Collectable OnActionCreate()
    {
        return Instantiate(Prefab);
    }

    protected override void OnActionRelease(Collectable coin)
    {
        base.OnActionRelease(coin);
        StartCoroutine(Getting());
    }
}
