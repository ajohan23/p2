using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SavedVegtableUI : MonoBehaviour
{
    public TMP_Text textTotal;
    public TMP_Text textSaved;

    // Start is called before the first frame update
    void Start()
    {
        textSaved.text = $"You saved {PlayerPrefs.GetInt("Money Saved")}kr";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
