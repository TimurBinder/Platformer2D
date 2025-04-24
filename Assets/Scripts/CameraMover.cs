using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private void Update()
    {
        Vector3 newPosition = _target.transform.position;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }
}
