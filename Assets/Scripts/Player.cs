using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
    public const int maxPawns = 5;
    public int pawnsInHand { get; private set; }
    public Color pawnColor;

    public int numberOfPlayer;
    public bool isPlayerAI;
    public ShoppingList shoppinglist;

    public Player(Color color, int numberOfPlayer, bool isPlayerAI, ShoppingList shoppinglist)
    {
        this.pawnColor = color;
        this.pawnsInHand = maxPawns;
        this.numberOfPlayer = numberOfPlayer;
        this.isPlayerAI = isPlayerAI;
        this.shoppinglist = shoppinglist;
    }

    public int PutDownPawn()
    {
        if (pawnsInHand > 0)
        {
            --pawnsInHand;
        }
        return pawnsInHand;
    }

    public int PickUpPawn()
    {
        if (pawnsInHand < 5)
        {
            pawnsInHand++;
        }
        return pawnsInHand;
    }

    public void MakeMove ()
    {

    }
}
