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
    [SerializeField] TimaerScript timer;

    //Variables
    [SerializeField] Vegtable vegtable;
    SpriteRenderer spriteRenderer;
    public Vegtable[] vegtables;
    List<ActionButton> existingActions = new List<ActionButton>();
    bool isOpenForInput = true; //Makes sure that the player cant press actions when they are not supossed to

    //Unity callbacks
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Invoke("ChooseRandomVegtable", 1.0f);
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
        if (!isOpenForInput) //Cancel if we dont accept input
        {
            return;
        }
        spriteRenderer.sprite = clue.foundSprite;
        CreateActionButton(clue.action);
    }

    void CreateActionButton(SavingAction action) //Creates and initializes a new action button with the given action
    {
        foreach(ActionButton _existingAction in existingActions) //If a button already exists for the given action. Cancel
        {
            if (_existingAction.GetAction().name == action.name)
            {
                return;
            }
        }

        GameObject newButton = Instantiate(actionButtonPrefab, actionbuttonParent);

        ActionButton actionButton = newButton.GetComponent<ActionButton>();
        if (actionButton != null)
        {
            actionButton.Initialize(action, this);
            existingActions.Add(actionButton);
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
        if(!isOpenForInput) //Cancel if we dont accept input
        {
            return;
        }

        if (Action == vegtable.correctAction)
        {
            highScore.AddScore(vegtable.Price);
        }
        else
        {
            highScore.ReduceScore(vegtable.Price);
        }

        timer.Pause();
        ClearActionButtons();
        isOpenForInput = false;
        Invoke("ChooseRandomVegtable", 1.0f);
    }

    void ChooseRandomVegtable()
    {
        isOpenForInput = true;
        vegtable = vegtables[Random.Range(0, vegtables.Length)];
        spriteRenderer.sprite = vegtable.deafaultSprite;

        timer.UnPause();

    }

    void ClearActionButtons()
    {
        foreach(ActionButton actionButton in existingActions)
        {
            Destroy(actionButton.gameObject);
        }
        existingActions.Clear();
    }
}


