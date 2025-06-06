using UnityEngine;

public class SkillsActivator : MonoBehaviour
{
    [SerializeField] private Skill[] _skills;
    [SerializeField] private SkillsInputReader _inputReader;

    private void OnEnable() =>
        _inputReader.SkillInputReading += ActivateSkill;

    private void OnDisable() =>
        _inputReader.SkillInputReading -= ActivateSkill;

    private void ActivateSkill(int index)
    {
        if (index < _skills.Length)
            _skills[index].Activate();
    }
}
