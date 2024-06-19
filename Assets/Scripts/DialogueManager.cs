using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText; 

    public Animator animator;
    [SerializeField] SoundPlayer soundPlayer; // not used anymore

    public Queue<string> sentences = new Queue<string>(); // first in, first out.

    // Update is called once per frame
    public void StartDialogue (Dialogue dialogue)
    {
        // soundPlayer.SoundsGibberish();
        animator.SetBool("IsOpen", true); // animates in, see the animator for more details
        nameText.text = dialogue.name; // the name in the dialouge 
        sentences.Clear(); // clears the sentences if there is a dialouge happening at the same time.
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); // sets the in the queue 
        }
        DisplayNextSentences();
    }

    public void DisplayNextSentences ()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue(); // new sentence in, Dequeue aka. cheked out at the grocery store :)
        StopAllCoroutines(); // stops it, delets it
        StartCoroutine(TypeSentence(sentence)); // the text animation
        
    }

    IEnumerator TypeSentence(string sentence) // text animation.
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) // every letter adds it to ToCharArray
        {
            dialogueText.text += letter; // adds a letter
            yield return new WaitForSeconds(0.03f); // waits..
        }
    }
    public void EndDialogue() // stops dialouge, and removes the text box.
    {
        StopAllCoroutines();
        animator.SetBool("IsOpen", false);
    }

}

