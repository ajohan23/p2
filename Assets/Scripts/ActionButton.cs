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
    public void Initialize (SavingAction _action, VegtableController _vegtableController)
    {
        action = _action;
        buttonText.text = action.name; // not used anymore / Before pictures
        vegtableController = _vegtableController;
    }

    public void PerformAction() // only on cut.
    {
        if (action != null && vegtableController != null)
        {
            vegtableController.PerformSaveAction(action); // destroyed everytime.
        }
    }

    public SavingAction GetAction()
    {
        return action;
    }
}
