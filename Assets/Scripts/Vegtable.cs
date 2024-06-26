using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vegtable", menuName = "Vegtable")]
public class Vegtable : ScriptableObject
{
    public string Name = "Name Vegtable";
    public int Price = 0;
    public Sprite deafaultSprite;
    public Clue[] clues;
    public string correctAction;

    public string keepComment = "";
    public string trashComment = "";
}

[System.Serializable]
public class Clue
{
    public Sprite foundSprite;
    public bool smellable = false;
    public bool visible = false;
    public bool tasteable = false;
    public bool feelable = false;

    public string smellComment = "MISSING COMMENT!";
    public string seeComment = "MISSING COMMENT!";
    public string tasteComment = "MISSING COMMENT!";
    public string feelComment = "MISSING COMMENT!";

    public SavingAction action;
}

[System.Serializable]
public class SavingAction
{
    public string name = "Cut";
    public Sprite actionSprite;
    public string actionComment;
}