using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WastedVegtableUI : MonoBehaviour
{
    public TMP_Text textTotal;
    public TMP_Text textSaved;

    // Start is called before the first frame update
    void Start()
    {
        textSaved.text = $"You Wasted {PlayerPrefs.GetInt("Money Saved")}kr";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
