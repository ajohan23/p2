using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionButton : MonoBehaviour
{
    //Refrences
    public TextMeshProUGUI buttonText; // Refrence to the text on the button
    VegtableController vegtableController = null; // Reference to the vegtable controller that instantiated this button. This reference is set in Initialize()

    //Runtime
    SavingAction action = null; // The action that is performed when this button is pressed. This is set in Inititalize().

    //Methods
    public void Initialize(SavingAction _action, VegtableController vegtableController)
    {
        action = _action;
        buttonText.text = action.name;
    }

    public void PerformAction()
    {
        if (action != null && vegtableController != null)
        {

        }
    }
}
