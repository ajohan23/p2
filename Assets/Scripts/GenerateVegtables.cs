using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateVegtables : MonoBehaviour
{
    // Structure definition for Vegetable
    public struct Vegetable
    {
        public string name;
        public float price;
        public string condition;
        public Sprite goodConditionSprite;
        public Sprite badConditionSprite;
    }

    // Array of Vegetable objects
    private Vegetable[] vegArray = new Vegetable[3];

    // Prefabs for vegetable GameObjects
    public GameObject vegetablePrefab;

    void Start()
    {
        // Initialization logic (could be loading from a file, database, etc.)
        InitializeVegetables();

        // Call the function to display vegetable information
        DisplayVegetables();
    }

    // Function to initialize vegetable data
    void InitializeVegetables()
    {
        vegArray[0].name = "Carrot";
        vegArray[0].price = 1.50f;
        vegArray[0].goodConditionSprite = Resources.Load<Sprite>("GoodCarrotSprite");
        vegArray[0].badConditionSprite = Resources.Load<Sprite>("BadCarrotSprite");
        GenerateRandomCondition(ref vegArray[0]);

        // Similarly initialize other vegetables...
    }

    // Function to display vegetable information
    void DisplayVegetables()
    {
        // Instantiate and position vegetable GameObjects
        for (int i = 0; i < vegArray.Length; i++)
        {
            GameObject vegObject = Instantiate(vegetablePrefab, new Vector3(i * 2, 0, 0), Quaternion.identity);
            vegObject.GetComponent<SpriteRenderer>().sprite = vegArray[i].condition == "good" ? vegArray[i].goodConditionSprite : vegArray[i].badConditionSprite;
        }
    }

    // Function to generate random condition for a vegetable
    void GenerateRandomCondition(ref Vegetable veg)
    {
        // Randomly set the condition to "good" or "bad"
        veg.condition = (Random.Range(0, 2) == 0) ? "good" : "bad";
    }
}

