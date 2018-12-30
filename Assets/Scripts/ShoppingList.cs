using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingList : MonoBehaviour {

    public Sprite image;
    string shoppingListName;
    public Dictionary<GameManager.Shop, int> items;

    public ShoppingList (Sprite image, string shoppingListName, int Electronic, int Grocery, int Newsstand, int Clothing, int Furniture)
    {
        this.image = image;
        this.shoppingListName = shoppingListName;
        items =  new Dictionary<GameManager.Shop, int>() {
            {GameManager.Shop.Electronic, Electronic},
            {GameManager.Shop.Grocery, Grocery},
            {GameManager.Shop.Newsstand, Newsstand},
            {GameManager.Shop.Clothing, Clothing},
            {GameManager.Shop.Furniture, Furniture} 
        };
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
