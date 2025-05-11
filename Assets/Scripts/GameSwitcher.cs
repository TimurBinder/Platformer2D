using UnityEngine;

public class GameSwitcher : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.Died += Quit;
    }

    private void Quit()
    {
        _player.Died -= Quit;
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
