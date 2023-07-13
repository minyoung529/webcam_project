using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOs", menuName = "Data/SheetSOHolder")]
public class SheetSOHolder : ScriptableObject
{
    [SerializeField]
    private List<SheetSO> sheets;

    public List<SheetSO> Sheets => sheets;

    public SheetSO Get(string name)
    {
        return sheets.Find(x => x.SheetName == name);
    }
}
