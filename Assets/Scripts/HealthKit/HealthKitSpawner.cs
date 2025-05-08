using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class HealthKitSpawner : MonoBehaviour
{
    [SerializeField] private float _repeatRate = 20f;
    [SerializeField] private int _maxSize = 3;
    [SerializeField] private int _defaultCapacity = 3;
    [SerializeField] private HealthKit _prefab;
    [SerializeField] private Ground _platform;

    private ObjectPool<HealthKit> _pool;
    private WaitForSeconds _wait;
    private IEnumerator _coroutine;
    private bool _isRunningCoroutine;

    private void Awake()
    {
        _pool = new ObjectPool<HealthKit>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => OnActionGet(obj),
            actionOnRelease: (obj) => OnActionRelease(obj),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            maxSize: _maxSize,
            defaultCapacity: _defaultCapacity
        );

        _wait = new WaitForSeconds(_repeatRate);
        _coroutine = GettingHealth();
    }

    private void OnEnable()
    {
        StartCoroutine(_coroutine);
        _isRunningCoroutine = true;
    }

    private void OnActionGet(HealthKit health)
    {
        float marginY = 2f;
        Vector3 position = _platform.TilesPositions[Random.Range(0, _platform.TilesPositions.Count)];
        position.y += marginY;
        health.transform.position = position;
        health.gameObject.SetActive(true);
        health.Collected += _pool.Release;

        if (_pool.CountActive >= _maxSize && _isRunningCoroutine)
        {
            StopCoroutine(_coroutine);
            _isRunningCoroutine = false;
        }
    }

    private void OnActionRelease(HealthKit health)
    {
        health.gameObject.SetActive(false);
        health.Collected -= _pool.Release;

        if (_pool.CountActive < _maxSize && _isRunningCoroutine == false)
        {
            StartCoroutine(_coroutine);
            _isRunningCoroutine = true;
        }
    }

    private IEnumerator GettingHealth()
    {
        while (enabled)
        {
            yield return _wait;
            _pool.Get();
        }
    }
}
