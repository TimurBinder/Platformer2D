using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SkillBar : MonoBehaviour
{
    [SerializeField] private Skill _skill;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (_skill.IsActivated)
            _slider.value = _skill.DurationTimer / _skill.Duration;
        else
            _slider.value = _skill.ReloadTimer / _skill.RepeatRate;
    }
}
