using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class VegtableController : MonoBehaviour
{
    [SerializeField] Vegtable vegtable;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (vegtable != null)
        {
            spriteRenderer.sprite = vegtable.deafaultSprite;
        }
    }

    public void Smell()
    {
        foreach(Clue clue in vegtable.clues)
        {
            if (clue.smellable)
            {
                spriteRenderer.sprite = clue.foundSprite;
            }
        }
    }
}


