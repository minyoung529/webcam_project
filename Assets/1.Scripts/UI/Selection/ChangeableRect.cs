using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class ChangeableRect : MonoBehaviour
{
    RectTransform rect;
    [SerializeField]
    private Ease easeType;

    private Vector2 defaultOffset = Vector2.zero;
    private Vector2 defaultPosition = Vector2.zero;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        defaultOffset = rect.offsetMin;
        defaultPosition = rect.anchoredPosition;
    }
    /// <summary>
    /// 음수값이면 아래로 양수값이면 위로
    /// </summary>
    /// <param name="changeVal">음수값이면 아래로 양수값이면 위로 </param>
    public void ChangeRectLength(float changeVal)
    {
        rect.DOKill();
        DOTween.To(() => rect.offsetMin, x => rect.offsetMin = x, new Vector2(rect.offsetMin.x, rect.offsetMin.y + changeVal), 0.5f).SetEase(easeType);
    }
    /// <summary>
    /// 음수값이면 아래로 양수값이면 위로
    /// </summary>
    /// <param name="changeVal">음수값이면 아래로 양수값이면 위로 </param>
    public void ChangeRectY(float changeVal)
    {
        rect.DOKill();
        rect.DOAnchorPos3DY(rect.anchoredPosition.y + changeVal, 0.5f).SetEase(easeType);
    }
    public void ResetLength()
    {
        rect.DOKill();
        rect.DOAnchorPos3DY(defaultPosition.y, 0.5f).SetEase(easeType);
        //DOTween.To(() => rect.offsetMin, x => rect.offsetMin = x, new Vector2(rect.offsetMin.x, defaultOffset.y), 0.5f).SetEase(easeType);
    }
    public float GetRectLength()
    {
        return rect.rect.height;
    }
}
[CustomEditor(typeof(ChangeableRect))]
public class ChangeableRectEditor : Editor
{
    private float num = 0;
    private ChangeableRect scroll;
    private void OnEnable()
    {
        scroll = target as ChangeableRect;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();
        num = EditorGUILayout.Slider(num, 0, 500);

        if (GUILayout.Button("늘이기", GUILayout.Width(80)))
        {
            scroll.ChangeRectLength(num);
            GUI.FocusControl(null);
        }
        if (GUILayout.Button("줄이기", GUILayout.Width(80)))
        {
            scroll.ChangeRectLength(-num);
            GUI.FocusControl(null);
        }
        EditorGUILayout.EndHorizontal();
    }
}