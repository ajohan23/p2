using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenImage : MonoBehaviour
{
    public float tweenTime;

    // Start is called before the first frame update
    void Start()
    {
        Tween();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tween()
    {
        LeanTween.cancel(gameObject);

        LeanTween.scale(gameObject, Vector3.one*2, tweenTime)
        .setEasePunch();
    }
}
