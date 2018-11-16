using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    GameObject[] pawns = new GameObject[5];
    List<GameObject> pawnsInHand = new List<GameObject>();
    public Color pawnColor;

    public int numberOfPlayer;
    public bool isPlayerAI;
    public ShoppingList shoppinglist;

    public Player(Color color, int numberOfPlayer, bool isPlayerAI, ShoppingList shoppinglist)
    {
        this.pawnColor = color;
        this.numberOfPlayer = numberOfPlayer;
        this.isPlayerAI = isPlayerAI;
        this.shoppinglist = shoppinglist;
    }

    public void MakeMove ()
    {

    }
}
