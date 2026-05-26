using UnityEngine;

public class UI_ToolTip : MonoBehaviour
{
    private RectTransform tooltipRect;
    private Vector2 offset;

    protected virtual void Awake()
    {
        tooltipRect = GetComponent<RectTransform>();
    }

    public virtual void ShowToolTip(bool show, RectTransform targetRect)
    {
        if (!show)
        {
            tooltipRect.position = new Vector2(9999, 9999);
            return;
        }

        UpdatePosition(targetRect);
    }

    private void UpdatePosition(RectTransform targetRect)
    {
        float screenCenterX = Screen.width / 2f;
        float screenTop = Screen.height;
        float screenBottom = 0f;

        offset.x = Screen.width / 6.5f;
        offset.y = Screen.height / 54f;

        Vector2 targetPosition = targetRect.position;

        targetPosition.x = targetPosition.x > screenCenterX ? targetPosition.x - offset.x : targetPosition.x + offset.x;

        float tooltipHeight = tooltipRect.rect.height * tooltipRect.lossyScale.y;
        float tooltipVerticalHalf = tooltipHeight / 2f;

        float tooltipTopY = targetPosition.y + tooltipVerticalHalf;
        float tooltipBottomY = targetPosition.y - tooltipVerticalHalf;

        if (tooltipTopY > screenTop)
            targetPosition.y = screenTop - tooltipVerticalHalf - offset.y;
        else if (tooltipBottomY < screenBottom)
            targetPosition.y = screenBottom + tooltipVerticalHalf + offset.y;

        tooltipRect.position = targetPosition;
    }

    protected string GetColouredText(string color, string text)
    {
        return $"<color={color}>{text}</color>";
    }
}
