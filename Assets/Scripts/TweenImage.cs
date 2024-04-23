using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenImage : MonoBehaviour
{
    public float tweenTime;
    [SerializeField] GameObject imageGameObject;

     private void Awake()
    {
        imageGameObject.SetActive(false);
    }

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

     IEnumerator showX() // Kører samtidigt med en Void Update
    {
        imageGameObject.SetActive (true); // shows the image because it is true

        yield return new WaitForSeconds(0.55f); // has a tiny pause, return type yield

        imageGameObject.SetActive(false); // then the image is false again
    }
}
