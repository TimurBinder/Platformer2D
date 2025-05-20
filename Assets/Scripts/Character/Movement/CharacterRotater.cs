using UnityEngine;

public class CharacterRotater : MonoBehaviour
{
    [SerializeField] private CharacterRenderer _renderer;

    public void Rotate(float direction)
    {
        float turnAngle = 180.0f;

        if (direction < 0)
            _renderer.transform.rotation = Quaternion.Euler(0, turnAngle, 0);
        else if (direction > 0)
            _renderer.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
