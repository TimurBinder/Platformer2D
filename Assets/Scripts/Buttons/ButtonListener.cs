using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonListener : MonoBehaviour
{
    [SerializeField] protected float Value;
    [SerializeField] protected Health Health;

    protected Button Button;

    private void Awake()
    {
        Button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        Button.onClick.AddListener(Attack);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(Attack);
    }

    protected virtual void Attack() { }
}
