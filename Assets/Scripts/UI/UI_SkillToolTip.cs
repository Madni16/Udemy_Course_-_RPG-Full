using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class UI_SkillToolTip : UI_ToolTip
{
    private UI ui;
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
    private string skillPointsKey = "skill_cost_text";
    private string locksOutKey = "locks_out";
    private string lockedSkillKey = "locked_skill";

    [Space]
    [SerializeField] private string metConditionHex;
    [SerializeField] private string notMetConditionHex;
    [SerializeField] private string importantInfoHex;
    [SerializeField] private Color exampleColor;

    private Coroutine textEffectCoroutine;

    protected override void Awake()
    {
        base.Awake();
        requirementsString = new(skillsLocalizationTable, "");
        skillPointsString = new(skillsLocalizationTable, "");
        locksOutString = new(skillsLocalizationTable, locksOutKey);
        lockedSkillString = new(skillsLocalizationTable, lockedSkillKey);

        ui = GetComponentInParent<UI>();
        skillTree = ui.GetComponentInChildren<UI_SkillTree>(true);
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

        string skillLockedText = GetColouredText(importantInfoHex, lockedSkillString.GetLocalizedString());
        string requirements = node.isLocked ? skillLockedText : GetRequirements(node.skillData.cost, node.neededNodes, node.conflictingNodes);

        skillRequirements.text = requirements;
    }

    public void LockedSkillEffect()
    {
        if(textEffectCoroutine != null)
            StopCoroutine(textEffectCoroutine);

        textEffectCoroutine = StartCoroutine(TextBlinkEffectCoroutine(skillRequirements, .15f, 3));
    }

    private IEnumerator TextBlinkEffectCoroutine(TextMeshProUGUI text, float blinkInterval, int blinkCount)
    {
        for (int i = 0; i < blinkCount; i++)
        {
            text.text = GetColouredText(notMetConditionHex, lockedSkillString.GetLocalizedString());
            yield return new WaitForSeconds(blinkInterval);

            text.text = GetColouredText(importantInfoHex, lockedSkillString.GetLocalizedString());
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    private string GetRequirements(int skillCost, UI_TreeNode[] neededNodes, UI_TreeNode[] conflicNodes)
    {
        StringBuilder sb = new StringBuilder();

        int totalRequirements = 1 + neededNodes.Length;
        requirementsString.TableEntryReference = requirementsKey; // If neededNodes has entries it means string should be plural (0 or >1 = plural)

        sb.AppendLine(requirementsString.GetLocalizedString(totalRequirements));

        string costColor = skillTree.EnoughSkillPoints(skillCost) ? metConditionHex : notMetConditionHex;
        skillPointsString.TableEntryReference = skillPointsKey;

        sb.AppendLine(GetColouredText(costColor, skillPointsString.GetLocalizedString(skillCost)));

        foreach (var node in neededNodes)
        {
            string nodeColor = node.isUnlocked ? metConditionHex : notMetConditionHex;
            sb.AppendLine(GetColouredText(nodeColor, $"- {node.skillData.displayName.GetLocalizedString()}"));
        }

        if (conflicNodes.Length <= 0)
            return sb.ToString();

        sb.AppendLine();
        sb.AppendLine(GetColouredText(importantInfoHex, locksOutString.GetLocalizedString()));

        foreach (var node in conflicNodes)
        {
            sb.AppendLine(GetColouredText(importantInfoHex, $"- {node.skillData.displayName.GetLocalizedString()}"));
        }

        return sb.ToString();
    }   
}
