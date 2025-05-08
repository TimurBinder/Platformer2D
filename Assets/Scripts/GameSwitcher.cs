using UnityEngine;

public class GameSwitcher : MonoBehaviour
{
    [SerializeField] Player _player;

    private void Awake()
    {
        _player.Died += Quit;
    }

    private void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
