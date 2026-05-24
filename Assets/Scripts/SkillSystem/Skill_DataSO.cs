using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(menuName = "RPG Setup/Skill Data", fileName = "Skill Data - ")]
public class Skill_DataSO : ScriptableObject
{
    public int cost;
    public string inEditorName;

    [Header("Skill description")]
    public LocalizedString displayName;
    public LocalizedString description;
    public Sprite icon;
}
