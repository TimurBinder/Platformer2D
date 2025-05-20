using TMPro;
using UnityEngine;

public class TextHealthIndicator : HealthIndicator
{
    [SerializeField] private TextMeshProUGUI _currentIndicatorText;
    [SerializeField] private TextMeshProUGUI _maxIndicatorText;

    protected override void SetMax()
    {
        _maxIndicatorText.text = Health.MaxValue.ToString();
    }

    protected override void SetCurrent()
    {
        _currentIndicatorText.text = Health.Value.ToString();
    }
}
