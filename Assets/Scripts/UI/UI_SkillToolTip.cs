using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class UI_SkillToolTip : UI_ToolTip
{
    private UI_SkillTree skillTree;

    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private TextMeshProUGUI skillRequirements;

    private string skillsLocalizationTable = "Skills";
    private LocalizedString requirementsString;
    private LocalizedString skillPointsString;
    private LocalizedString locksOutString;
    private LocalizedString lockedSkillString;
    private string requirementsKey = "requirements";
    private string skillPointsKey = "skill_points";
    private string locksOutKey = "locks_out";
    private string lockedSkillKey = "locked_skill";

    [Space]
    [SerializeField] private string metConditionHex;
    [SerializeField] private string notMetConditionHex;
    [SerializeField] private string importantInfoHex;
    [SerializeField] private Color exampleColor;

    protected override void Awake()
    {
        base.Awake();
        requirementsString = new(skillsLocalizationTable, "");
        skillPointsString = new(skillsLocalizationTable, "");
        locksOutString = new(skillsLocalizationTable, locksOutKey);
        lockedSkillString = new(skillsLocalizationTable, lockedSkillKey);

        skillTree = GetComponentInParent<UI_SkillTree>();
    }
    public override void ShowToolTip(bool show, RectTransform targetRect)
    {
        base.ShowToolTip(show, targetRect);
    }

    public void ShowToolTip(bool show, RectTransform targetRect, UI_TreeNode node)
    {
        base.ShowToolTip(show, targetRect);

        if (show == false)
            return;

        skillName.text = node.skillData.displayName.GetLocalizedString();
        skillDescription.text = node.skillData.description.GetLocalizedString();

        string skillLockedText = $"<color={importantInfoHex}>{lockedSkillString.GetLocalizedString()} </color>";
        string requirements = node.isLocked ? skillLockedText : GetRequirements(node.skillData.cost, node.neededNodes, node.conflictingNodes);

        skillRequirements.text = requirements;
    }

    private string GetRequirements(int skillCost, UI_TreeNode[] neededNodes, UI_TreeNode[] conflicNodes)
    {
        StringBuilder sb = new StringBuilder();

        int totalRequirements = 1 + neededNodes.Length;

        requirementsString.TableEntryReference = requirementsKey;
        requirementsString.Arguments = new object[] { totalRequirements }; // If neededNodes has entries it means string should be plural

        sb.AppendLine(requirementsString.GetLocalizedString());

        string costColor = skillTree.EnoughSkillPoints(skillCost) ? metConditionHex : notMetConditionHex;

        skillPointsString.TableEntryReference = skillPointsKey;
        skillPointsString.Arguments = new object[] { skillCost };

        sb.AppendLine($"<color={costColor}>- {skillCost} {skillPointsString.GetLocalizedString()}</color>");

        foreach (var node in neededNodes)
        {
            string nodeColor = node.isUnlocked ? metConditionHex : notMetConditionHex;
            sb.AppendLine($"<color={nodeColor}>- {node.skillData.displayName.GetLocalizedString()}</color>");
        }

        if (conflicNodes.Length <= 0)
            return sb.ToString();

        sb.AppendLine();
        sb.AppendLine($"<color={importantInfoHex}>{locksOutString.GetLocalizedString()} </color>");

        foreach (var node in conflicNodes)
        {
            sb.AppendLine($"<color={importantInfoHex}>- {node.skillData.displayName.GetLocalizedString()}</color>");
        }

        return sb.ToString();
    }
}
