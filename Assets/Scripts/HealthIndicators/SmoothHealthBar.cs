using System.Collections;
using UnityEngine;

public class SmoothHealthBar : HealthBar
{
    [SerializeField] private float _speed = 2f;

    private bool _isChanging;
    private float _currentValue;

    protected override void SetCurrent()
    {
        _currentValue = Health.Value / Health.MaxValue;

        if (_isChanging == false)
            StartCoroutine(ChangingBar());
    }

    public override void SetHealth(Health health)
    {
        Health = health;

        if (Slider != null)
            Slider.value = health.Value / health.MaxValue;
    }

    private IEnumerator ChangingBar()
    {
        _isChanging = true;
        float step = 0.1f;
        float delta = step * _speed * Time.deltaTime;

        while (Mathf.Approximately(Slider.value, _currentValue) == false)
        {
            Slider.value = Mathf.MoveTowards(Slider.value, _currentValue, delta);
            yield return null;
        }

        _isChanging = false;

        if (Health.IsAlive == false)
            Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
