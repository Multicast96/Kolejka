using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingList : MonoBehaviour {

    [SerializeField] string target;
    [SerializeField] int electronic;
    [SerializeField] int grocery;
    [SerializeField] int newsman;
    [SerializeField] int cloth;
    [SerializeField] int furniture;

    public ShoppingList (string target, int electronic, int grocery, int newsman, int cloth, int furniture)
    {
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
