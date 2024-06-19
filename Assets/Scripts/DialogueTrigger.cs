using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool triggerOnStart = false; // in the start.
    public Dialogue dialogue;

    private void Start()
    {
        if(triggerOnStart) // starts dialogue
        {
            TriggerDialogue(); 
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogManager>().StartDialogue(dialogue);
    }
       
}
