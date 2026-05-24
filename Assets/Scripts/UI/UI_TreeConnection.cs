using UnityEngine;

public class UI_TreeConnection : MonoBehaviour
{
    [SerializeField] private RectTransform rotationPoint;
    [SerializeField] private RectTransform connectionLength;
    [SerializeField] private RectTransform childNoteConnectionPoint;

    public void DirectConnection(NodeDirectionType direction, float length)
    {
        bool shouldBeActive = direction != NodeDirectionType.None;
        float finalLength = shouldBeActive ? length : 0;
        float angle = GetDirectionAngle(direction);

        rotationPoint.localRotation = Quaternion.Euler(0, 0, angle);
        connectionLength.sizeDelta = new Vector2(finalLength, connectionLength.sizeDelta.y);
    }

    public Vector2 GetConnectionPoint(RectTransform rect)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                rect.parent as RectTransform,
                childNoteConnectionPoint.position,
                null,
                out var localPosition
            );

        return localPosition;
    }

    private float GetDirectionAngle(NodeDirectionType type)
    {
        switch (type)
        {
            case NodeDirectionType.UpRight: return 45f;
            case NodeDirectionType.Up: return 90f;
            case NodeDirectionType.UpLeft: return 135f;
            case NodeDirectionType.Right: return 0f;
            case NodeDirectionType.Left: return 180f;
            case NodeDirectionType.DownRight: return -45f;
            case NodeDirectionType.Down: return -90f;
            case NodeDirectionType.DownLeft: return -135f;
            default: return 0f;
        }
    }
}

public enum NodeDirectionType
{
    None,
    UpLeft,
    Up,
    UpRight,
    Left,
    Right,
    DownLeft,
    Down,
    DownRight,
}