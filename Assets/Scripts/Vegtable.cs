using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vegtable", menuName = "Vegtable")]
public class Vegtable : ScriptableObject
{
    public string Name = "Name Vegtable";
    public int Price = 0;
    public Sprite deafaultSprite;
}
