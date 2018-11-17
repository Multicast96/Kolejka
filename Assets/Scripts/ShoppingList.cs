using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingList : MonoBehaviour {

    public Sprite image;
    [SerializeField] string target;
    [SerializeField] int electronic;
    [SerializeField] int grocery;
    [SerializeField] int newsman;
    [SerializeField] int cloth;
    [SerializeField] int furniture;

    public ShoppingList (Sprite image, string target, int electronic, int grocery, int newsman, int cloth, int furniture)
    {
        this.image = image;
        this.target = target;
        this.electronic = electronic;
        this.grocery = grocery;
        this.newsman = newsman;
        this.cloth = cloth;
        this.furniture = furniture;
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
