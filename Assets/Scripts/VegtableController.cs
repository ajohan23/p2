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
    [SerializeField] DialogManager dialogManager;

    [SerializeField] ParticleSystem stars; // staaaaars!
    public float tweenTime; // tween
    public GameObject cross;

    //Variables
    [SerializeField] Vegtable vegtable;
    SpriteRenderer spriteRenderer;
    public Vegtable[] vegtables;
    List<ActionButton> existingActions = new List<ActionButton>();
    bool isOpenForInput = true; //Makes sure that the player cant press actions when they are not supossed to
    [SerializeField] string CommentorName = "Owl"; //The name displayed when a comment is posted

    //Unity callbacks
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cross.SetActive(false);
    }

    private void Start()
    {
        ChooseRandomVegtable();
    }

    //Methods
    public void Smell() //Inspection method used when the player smells the vegtable
    {
        foreach(Clue clue in vegtable.clues)
        {
            if (clue.smellable)
            {
                ClueFound(clue, "smell");
            }
        }
    }

    public void Taste () //Inspection method used when the player tastes the vegtable
    { 
        foreach (Clue clue in vegtable.clues)
        {
            if (clue.tasteable)
            {
                ClueFound(clue, "taste");
            }
        }
    }

    public void Feel() //Inspection method used when the player touches the vegtable
    {
        foreach (Clue clue in vegtable.clues)
        {
            if (clue.feelable)
            {
                ClueFound(clue, "feel");
            }
        }
    }

    public void See() //Inspection method used when the player looks at the vegtable
    {
        foreach (Clue clue in vegtable.clues)
        {
            if (clue.visible)
            {
                ClueFound(clue, "see");
            }
        }
    }

    public void PerformSaveAction(SavingAction action) // Called when an action button is pressed
    {
        if (!isOpenForInput) //Cancel if we dont accept input
        {
            return;
        }

        spriteRenderer.sprite = action.actionSprite;
        dialogManager.StartDialogue(new Dialogue(CommentorName, action.actionComment));
        Action(action.name);
    }

    void ClueFound(Clue clue, string sense) // Updates the visauls of the vegtable and creates a new action button
    {
        if (!isOpenForInput) //Cancel if we dont accept input
        {
            return;
        }

        switch(sense)
        {
            case "see":
                dialogManager.StartDialogue(new Dialogue(CommentorName, clue.seeComment));
                break;
            case "smell":
                dialogManager.StartDialogue(new Dialogue(CommentorName, clue.smellComment));
                break;
            case "feel":
                dialogManager.StartDialogue(new Dialogue(CommentorName, clue.feelComment));
                break;
            case "taste":
                dialogManager.StartDialogue(new Dialogue(CommentorName, clue.tasteComment));
                break;
            default:
                break;

        }

        pauseInput(1f);
        Invoke("HideComment", 1f);
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
        dialogManager.StartDialogue(new Dialogue(CommentorName, vegtable.trashComment));
        Action("Trash");
    }

    public void Keep()
    {
        dialogManager.StartDialogue(new Dialogue(CommentorName, vegtable.keepComment));
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
            tweenImage();
            stars.Play();
            
        }
        else
        {
            highScore.ReduceScore(vegtable.Price);
            StartCoroutine(showCross()); // used together with an IEnumerator,
        }

        timer.Pause();
        ClearActionButtons();
        pauseInput(1f);
        Invoke("ChooseRandomVegtable", 1.0f);
    }

    void ChooseRandomVegtable()
    {
        timer.UnPause();
        stars.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        HideComment();
        vegtable = vegtables[Random.Range(0, vegtables.Length)];
        spriteRenderer.sprite = vegtable.deafaultSprite;
    }

    void ClearActionButtons()
    {
        foreach(ActionButton actionButton in existingActions)
        {
            Destroy(actionButton.gameObject);
        }
        existingActions.Clear();
    }

    void HideComment()
    {
        dialogManager.EndDialogue();
    }

    void pauseInput(float time)
    {
        isOpenForInput = false;
        Invoke("enableInput", time);
    }

    void enableInput()
    {
        isOpenForInput = true;
    }

    public void tweenImage()
    {
        LeanTween.scale(gameObject, Vector3.one*2, tweenTime)
        .setEasePunch();
    }

    IEnumerator showCross() // Kører samtidigt med en Void Update
    {
        cross.SetActive (true); // shows the image because it is true
        yield return new WaitForSeconds(0.55f); // has a tiny pause, return type yield
        cross.SetActive(false); // then the image is false again
    }
}


