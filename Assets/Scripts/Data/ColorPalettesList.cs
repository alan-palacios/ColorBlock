using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColorPalettesList : ScriptableObject
{
    public ColorPaletteData [] colorPaletteData;
}

[System.Serializable]
public struct ColorPaletteData
{
    public string name;
      public Color [] blockColors;
      public int price;
      public int order;
}
