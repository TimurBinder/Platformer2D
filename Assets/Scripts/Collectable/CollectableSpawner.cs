using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

abstract public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] private float _repeatRate;

    protected Collectable Prefab;
    protected ObjectPool<Collectable> Pool;
    protected WaitForSeconds Wait;
    protected int MaxSize;
    protected int DefaultCapacity;

    protected virtual void Awake()
    {
        Pool = new ObjectPool<Collectable>(
            createFunc: () => OnActionCreate(),
            actionOnGet: (obj) => OnActionGet(obj),
            actionOnRelease: (obj) => OnActionRelease(obj),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            maxSize: MaxSize,
            defaultCapacity: DefaultCapacity
        );

        Wait = new WaitForSeconds(_repeatRate);
    }

    protected virtual Collectable OnActionCreate()
    {
        return Instantiate(Prefab);
    }

    protected virtual void OnActionGet(Collectable collectable)
    {
        collectable.Collected += Pool.Release;
        collectable.gameObject.SetActive(true);
    }

    protected virtual void OnActionRelease(Collectable collectable)
    {
        collectable.Collected -= Pool.Release;
        collectable.gameObject.SetActive(false);
    }

    protected virtual IEnumerator Getting()
    {
        yield return Wait;
        Pool.Get();
    }
}
