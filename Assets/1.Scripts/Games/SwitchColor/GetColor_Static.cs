using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GetColor
{
    public static Color GetColorEnumToColor(ColorEnum color)
    {
        switch (color)
        {
            case ColorEnum.Red:
                return Color.red;
            case ColorEnum.Blue:
                return Color.blue;
            case ColorEnum.Green:
                return Color.green;
            case ColorEnum.Yellow:
                return Color.yellow;
            default: return Color.black;
        }
    }

    public static ColorEnum GetRandomColorEnum()
    {
        int randomColorIndex = Random.Range(1, (int)ColorEnum.Count);

        return (ColorEnum)randomColorIndex;
    }

    public static bool IsSameColor(ColorEnum colorType, FloorBlock floor)
    {
        return colorType == floor.ColorType;
    }

}
