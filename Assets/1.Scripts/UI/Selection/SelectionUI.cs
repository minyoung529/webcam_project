using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectionUI : MonoBehaviour
{
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private SelectionPanel selectionPrefab;
    private List<SelectionPanel> selectionList = new();

    public int selectionIndex { get; private set; }

    private void Start()
    {
        InitSelection();
    }

    private void InitSelection()
    {
        selectionIndex = -1;
        Transform[] children = content.GetComponentsInChildren<Transform>();
        if (children != null)
        {
            for (int i = 1; i < children.Length; i++)
            {
                if (children[i] == content) continue;
                selectionList.Remove(children[i].GetComponent<SelectionPanel>());
                Destroy(children[i]);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            selectionList.Add(InstantiatePanel(i));
        }
    }
    private SelectionPanel InstantiatePanel(int index)
    {
        SelectionPanel panel = Instantiate(selectionPrefab, content.transform);
        panel.gameObject.SetActive(false);
        panel.SetSelection("", index);
        panel.Button.onClick.AddListener(()=>SetSelectionIndex(panel));
        return panel;
    }
    private void SetSelectionIndex(SelectionPanel panel)
    {
        selectionIndex = panel.Index;
    }
    public void SetSelection(List<string> selection)
    {
        for (int i = 0; i < selection.Count; i++)
        {
            if (selectionList[i] == null)
            {
                selectionList.Add(InstantiatePanel(i));
            }
            selectionList[i].SetSelection(selection[i]);
            selectionList[i].gameObject.SetActive(true);
        }
    }
    public void ResetSelection()
    {
        selectionIndex = -1;
        selectionList.All((x) => { 
            x.gameObject.SetActive(false);
            x.SetSelection(string.Empty);
            return true; 
        });
    }
}
