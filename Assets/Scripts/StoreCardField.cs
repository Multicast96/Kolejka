using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreCardField : MonoBehaviour {

    public List<ProductCard> cards;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void AddCardToField(ProductCard card)
    {
        cards.Add(card);
    }

    void RemoveCardFromField()
    {
        cards.Remove(cards[cards.Count-1]);
    }
}
