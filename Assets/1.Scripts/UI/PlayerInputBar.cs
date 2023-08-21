using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputBar : MonoBehaviour
{
    [SerializeField]
    private Button expandButton;
    [SerializeField]
    private ChangeableRect chatPanelRect;

    private ChangeableRect selectionRect;
    private TestChatUIManager chatUIManager;
    private SelectionUI selectionUI;
    private bool isExpand = false;

    private void Start()
    {
        chatUIManager = FindObjectOfType<TestChatUIManager>();
        selectionUI = GetComponent<SelectionUI>();
        selectionRect = GetComponent<ChangeableRect>();
        ActivateSelection();
    }
    private void Expand()
    {
        if (isExpand)
        {
            print($"reset");
            selectionRect.ResetLength();
            chatPanelRect.ResetLength();
            isExpand = false;
            chatUIManager.ScrollDelay();
        }
        else
        {
            print($"expand!{selectionUI.GetContentLength()}");
            //selectionPanel.ChangeRectLength(selectionUI.GetContentLength());
            selectionRect.ChangeRectY(selectionUI.GetContentLength());
            if (chatUIManager.GetContentLength() > chatPanelRect.GetRectLength() - selectionUI.GetContentLength())
                chatPanelRect.ChangeRectY(selectionUI.GetContentLength());
            isExpand = true;
            chatUIManager.ScrollDelay();
        }
    }
    public void ActivateSelection()
    {
        expandButton.GetComponent<Image>().color = Color.white;
        expandButton.onClick.AddListener(Expand);
    }
    public void DeactivateSelection()
    {
        expandButton.GetComponent<Image>().color = Color.gray;
        expandButton.onClick.RemoveListener(Expand);
    }
}
