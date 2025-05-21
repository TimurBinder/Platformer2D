using System.Collections;
using UnityEngine;

public class HealthKitSpawner : CollectableSpawner
{
    [SerializeField] private int _maxSize = 3;
    [SerializeField] private int _defaultCapacity = 3;
    [SerializeField] private HealthKit _prefab;
    [SerializeField] private Ground _platform;

    private IEnumerator _coroutine;
    private bool _isRunningCoroutine;

    protected override void Awake()
    {
        MaxSize = _maxSize;
        DefaultCapacity = _defaultCapacity;
        Prefab = _prefab;

        base.Awake();
        _coroutine = Getting();
    }

    private void OnEnable()
    {
        if (_coroutine != null)
            StartCoroutine(_coroutine);

        _isRunningCoroutine = true;
    }

    protected override void OnActionGet(Collectable health)
    {
        float marginY = 2f;
        Vector3 position = _platform.TilesPositions[Random.Range(0, _platform.TilesPositions.Count)];
        position.y += marginY;
        health.transform.position = position;
        base.OnActionGet(health);

        if (Pool.CountActive >= _maxSize && _isRunningCoroutine)
        {
            StopCoroutine(_coroutine);
            _isRunningCoroutine = false;
        }
    }

    protected override void OnActionRelease(Collectable health)
    {
        base.OnActionRelease(health);

        if (Pool.CountActive < _maxSize && _isRunningCoroutine == false)
        {
            StartCoroutine(_coroutine);
            _isRunningCoroutine = true;
        }
    }

    protected override IEnumerator Getting()
    {
        while (enabled)
            yield return base.Getting();
    }
}
