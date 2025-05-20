using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : HealthIndicator
{
    protected Slider Slider;

    protected override void Awake()
    {
        Slider = GetComponent<Slider>();
        Slider.maxValue = 1;
        Slider.minValue = 0;
        Slider.value = Slider.maxValue;
    }

    public override void SetHealth(Health health)
    {
        Health = health;
        SetCurrent();
    }

    protected override void SetCurrent()
    {
        Slider.value = Health.Value / Health.MaxValue;
    }
}
