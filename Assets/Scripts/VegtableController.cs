using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class VegtableController : MonoBehaviour
{
    //Refrences
    [SerializeField] GameObject actionButtonPrefab;
    [SerializeField] Transform actionbuttonParent;
    [SerializeField] HighScore highScore;

    //Variables
    [SerializeField] public Vegtable vegtable;
    SpriteRenderer spriteRenderer;

    //Unity callbacks
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (vegtable != null)
        {
            spriteRenderer.sprite = vegtable.deafaultSprite;
        }
    }

    //Methods
    public void Smell() //Inspection method used when the player smells the vegtable
    {
        foreach(Clue clue in vegtable.clues)
        {
            if (clue.smellable)
            {
                ClueFound(clue);
            }
        }
    }

    public void Taste () //Inspection method used when the player tastes the vegtable
    { 
        foreach (Clue clue in vegtable.clues)
        {
            if (clue.tasteable)
            {
                ClueFound(clue);
            }
        }
    }

    public void Feel() //Inspection method used when the player touches the vegtable
    {
        foreach (Clue clue in vegtable.clues)
        {
            if (clue.feelable)
            {
                ClueFound(clue);
            }
        }
    }

    public void See() //Inspection method used when the player looks at the vegtable
    {
        foreach (Clue clue in vegtable.clues)
        {
            if (clue.visible)
            {
                ClueFound(clue);
            }
        }
    }

    public void PerformSaveAction(SavingAction action) // Called when an action button is pressed
    {
        spriteRenderer.sprite = action.actionSprite;
        Action(action.name);
    }

    void ClueFound(Clue clue) // Updates the visauls of the vegtable and creates a new action button
    {
        spriteRenderer.sprite = clue.foundSprite;
        CreateActionButton(clue.action);
    }

    void CreateActionButton(SavingAction action) //Creates and initializes a new action button with the given action
    {
        GameObject newButton = Instantiate(actionButtonPrefab, actionbuttonParent);

        ActionButton actionButton = newButton.GetComponent<ActionButton>();
        if (actionButton != null)
        {
            actionButton.Initialize(action, this);
        }
        else
        {
            Debug.LogWarning("ActionButton script not found on new action button");
        }
    }
    public void Trash()
    {
        Action("Trash");
    }

    public void Keep()
    {
        Action("Keep");
    }
    void Action(string Action)
    {
        if (Action == vegtable.correctAction.name)
        {
            highScore.AddScore(vegtable.Price);
        }
        else
        {
            highScore.ReduceScore(vegtable.Price);
        }
    }
}


