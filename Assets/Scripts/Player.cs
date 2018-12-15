using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
    public const int maxPawns = 5;
    public int pawnsInHand { get; private set; }
    public Color pawnColor;
    public GameObject[] pawns = new GameObject[5];

    public int numberOfPlayer;
    public ShoppingList shoppinglist;
    public List<ManipulationCard> manipulationCards;

    public Player(Color color, int numberOfPlayer, ShoppingList shoppinglist)
    {
        this.pawnColor = color;
        this.pawnsInHand = maxPawns;
        this.numberOfPlayer = numberOfPlayer;
        this.shoppinglist = shoppinglist;

        this.manipulationCards = new List<ManipulationCard>();

        for(int i=0; i<10; i++)
        {
            this.manipulationCards.Add(new ManipulationCard(null, i));
        }
        this.manipulationCards = Shuffle(manipulationCards);

        GameObject playerPawns = GameObject.Find(String.Format("Player {0}", numberOfPlayer));
        for(int i = 0; i < maxPawns; i++)
        {
            GameObject pawn = playerPawns.transform.Find(String.Format("Pawn {0}", i)).gameObject;
            pawn.GetComponent<Renderer>().material.color = pawnColor;
            pawns[i] = pawn;
        }

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

    public virtual void MakeMove (){}

    public void PlayManipulationCard(ManipulationCard manipulationCard)
    {
        manipulationCard.PlayCard();

        for (int i = 0; i < this.manipulationCards.Count; i++)
        {
            if ((int)this.manipulationCards[i].getCardName() == (int)manipulationCard.getCardName())
                this.manipulationCards.RemoveAt(i);
        }
    }

    public static List<T> Shuffle<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }


}
