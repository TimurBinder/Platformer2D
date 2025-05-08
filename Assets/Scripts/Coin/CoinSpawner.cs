using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private Transform[] _startPoints;
    [SerializeField] private float _repeatRate;

    private ObjectPool<Coin> _pool;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () => Instantiate(_prefab, _startPoints[_pool.CountActive].position, Quaternion.identity),
            actionOnGet: (obj) => OnActionGet(obj),
            actionOnRelease: (obj) => OnActionRelease(obj),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            maxSize: _startPoints.Length,
            defaultCapacity: _startPoints.Length
        );

        _wait = new WaitForSeconds(_repeatRate);

        for (int i = 0; i < _startPoints.Length; i++)
            _pool.Get();
    }

    private void OnActionGet(Coin coin)
    {
        coin.Collected += _pool.Release;
        coin.gameObject.SetActive(true);
    }

    private void OnActionRelease(Coin coin)
    {
        coin.Collected -= _pool.Release;
        coin.gameObject.SetActive(false);
        StartCoroutine(GetCoin());
    }

    private IEnumerator GetCoin()
    {
        yield return _wait;
        _pool.Get();
    }
}
