using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 技能配置 只列出了必须的几项
/// </summary>
public partial interface ISkillConfig 
{
    int id { get; }
    string name { get; }
    int release_effect_id { get; }

    float duration { get; }
    float cd { get; }
}