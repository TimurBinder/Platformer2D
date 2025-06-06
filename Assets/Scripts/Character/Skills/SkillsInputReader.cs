using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillsInputReader : MonoBehaviour
{
    private const string Skill1 = nameof(Skill1);
    private const string Skill2 = nameof(Skill2);
    private const string Skill3 = nameof(Skill3);

    private List<string> _skillsInputNames;

    public event Action<int> SkillInputReading;

    private void Awake()
    {
        _skillsInputNames = new List<string>()
        {
            Skill1, Skill2, Skill3
        };
    }

    private void Update()
    {
        for (var i = 0; i < _skillsInputNames.Count; i++) 
        {
            string skillName = _skillsInputNames[i];
            bool skillInput = Input.GetButtonDown(skillName);

            if (skillInput)
                SkillInputReading?.Invoke(i);
        }
    }
}
