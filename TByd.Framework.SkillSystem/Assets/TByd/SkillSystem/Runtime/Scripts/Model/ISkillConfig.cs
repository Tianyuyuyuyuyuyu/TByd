using System.Collections;
using System.Collections.Generic;

/// <summary>
/// �������� ֻ�г��˱���ļ���
/// </summary>
public partial interface ISkillConfig 
{
    int id { get; }
    string name { get; }
    int release_effect_id { get; }

    float duration { get; }
    float cd { get; }
}