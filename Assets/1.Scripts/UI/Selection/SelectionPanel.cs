using UnityEngine;
using UnityEngine.UI;
using Michsky.UI.Shift;
[RequireComponent(typeof(Button))]
public class SelectionPanel : MonoBehaviour
{
    [SerializeField]
    private MainButton mainButton = null;
    [SerializeField]
    private Button button = null;
    private int index = -1;
    public int Index => index;
    public Button Button => button;
    public void SetSelection(string text, int index = -1)
    {
        this.index = (index != -1) ? index : this.index;
        mainButton.SetButtonText(text);
    }
}
