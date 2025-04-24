using UnityEngine;

public class CharacterRotater : MonoBehaviour
{
    public void SetDirection(float direction)
    {
        float turnAngle = 180.0f;

        if (direction < 0)
            transform.rotation = Quaternion.Euler(0, turnAngle, 0);
        else if (direction > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
