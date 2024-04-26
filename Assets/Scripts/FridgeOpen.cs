using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeOpen : MonoBehaviour
{
    public Animator ani;
    void Start () 
    {
        ani.enabled = false;
    }

    
    public void Press() 
        
    {
        ani.enabled = true;
    }
}
