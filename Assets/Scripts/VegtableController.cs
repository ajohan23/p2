using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class VegtableController : MonoBehaviour
{
    //Refrences
    [SerializeField] GameObject actionButtonPrefab;
    [SerializeField] Transform actionbuttonParent;
    [SerializeField] HighScore highScore;
    [SerializeField] TimaerScript timer;
    [SerializeField] DialogManager dialogManager;
    [SerializeField] SoundPlayer soundPlayer;
    [SerializeField] ParticleSystem stars; // staaaaars!
    public float tweenTime; // tween
    public GameObject cross;
    

    //Variables
    [SerializeField] Vegtable vegtable;
    [SerializeField] float readTime = 2f;
    SpriteRenderer spriteRenderer;
    public Vegtable[] vegtables;
    List<ActionButton> existingActions = new List<ActionButton>();
    bool isOpenForInput = true; //Makes sure that the player cant press actions when they are not supossed to
    [SerializeField] string CommentorName = "Owl"; //The name displayed when a comment is posted
    [SerializeField]
    Button[] buttons = new Button[0];

    //Datalogging
    LoggingManager logManager;
    int totalFruit = 0;
    int correctAnswers = 0;
    int sensesUsed = 0;
    int cluesFound = 0;
    string logName = "fruit_log";
    string totalFruitColumnName = "Total Fruit";
    string correctAnswerColumnName = "Correct Answers";
    string sensesUsedColumnName = "Senses Used";
    string cluesFoundColumnName = "Clues Found";

    //Unity callbacks
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cross.SetActive(false);
    }

    private void Start()
    {
        ChooseRandomVegtable();
        logManager = FindAnyObjectByType<LoggingManager>();
    }

    //Methods
    public void Smell() //Inspection method used when the player smells the vegtable
    {
        sensesUsed++;
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
        sensesUsed++;
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
        sensesUsed++;
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
        sensesUsed++;
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
        soundPlayer.SoundsCut();

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

        cluesFound++;
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

        Invoke("HideComment", readTime);
        spriteRenderer.sprite = clue.foundSprite;
        CreateActionButton(clue.action);
        pauseInput(readTime);
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
        soundPlayer.SoundsTrashBin();
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
            correctAnswers++;
            soundPlayer.SoundsSaved();
        }
        else
        {
            DisplayClueSprite(); //This is called to show the user that the vegtable was rotten in case they never found any clues
            highScore.ReduceScore(vegtable.Price);
            soundPlayer.SoundsWasted();
            StartCoroutine(showCross()); // used together with an IEnumerator,
        }

        timer.Pause();
        ClearActionButtons();
        pauseInput(readTime);
        Invoke("ChooseRandomVegtable", readTime);
    }

    void ChooseRandomVegtable()
    {
        tweenImage();
        totalFruit++;
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
        StopInput();
        Invoke("enableInput", time);
    }

    void enableInput()
    {
        EnableButtons(true);
        isOpenForInput = true;
    }

    public void StopInput()
    {
        EnableButtons(false);
        isOpenForInput = false;
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

    public void LogData()
    {
        if (logManager != null)
        {
            logManager.CreateLog(logName, headers: new List<string>() { totalFruitColumnName, correctAnswerColumnName, sensesUsedColumnName, cluesFoundColumnName});
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {totalFruitColumnName, totalFruit},
                {correctAnswerColumnName, correctAnswers},
                {sensesUsedColumnName, sensesUsed},
                {cluesFoundColumnName, cluesFound}
            };
            logManager.Log(logName, data);
            logManager.SaveLog(logName, clear:true, TargetType.CSV);
        }
        else
        {
            Debug.Log("Log Manager not found");
        }
    }

    void DisplayClueSprite()
    {
        if (vegtable.clues.Length > 0)
        {
            spriteRenderer.sprite = vegtable.clues[0].foundSprite;
        }
    }

    void EnableButtons(bool enable)
    {
        //Enable all normal buttons
        foreach (Button button in buttons)
        {
            button.interactable = enable;
        }

        //Enable all action buttons
        foreach (ActionButton actionButton in existingActions)
        {
            Button _button = actionButton.GetComponent<Button>();
            if (_button != null )
            {
                _button.interactable = enable;
            }
        }
    }
}


