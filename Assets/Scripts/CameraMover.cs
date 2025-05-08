using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _positionY;

    private void Update()
    {
        Vector3 newPosition = new Vector3(_target.transform.position.x, _target.transform.position.y + _positionY, transform.position.z);
        transform.position = newPosition;
    }
}
