using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vegtable", menuName = "Vegtable")] // makes it in Unity
public class Vegtable : ScriptableObject // multiple use
{
    public string Name = "Name Vegtable";
    public int Price = 0;
    public Sprite deafaultSprite;
    public Clue[] clues; // 
    public string correctAction;

    public string keepComment = "";
    public string trashComment = "";
}
// vegetable can be used multiple places, but the clue is not a scriptable object.

[System.Serializable] // simple data types and given unity stuff.
public class Clue // one time
{
    public Sprite foundSprite;
    public bool smellable = false;
    public bool visible = false;
    public bool tasteable = false;
    public bool feelable = false;

    // used in the NOT AVATAR version. So something else is written here.
    public string smellComment = "Smells fine";
    public string seeComment = "Looks fine";
    public string tasteComment = "Tastes fine";
    public string feelComment = "Feels fine";

    public SavingAction action;
}

[System.Serializable]
public class SavingAction
{
    public string name = "Cut";
    public Sprite actionSprite;
    public string actionComment;
}