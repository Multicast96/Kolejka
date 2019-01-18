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
    public ShoppingList productsEquipment = new ShoppingList(null, "",0,0,0,0,0);

    public List<ManipulationCard> manipulationCards = new List<ManipulationCard>();    //wszystkie karty manipulacji
    public List<ManipulationCard> avlManipulationCards = new List<ManipulationCard>(); //dostępne karty (max. 3)
    public List<Sprite> myManipulationCardsImages = new List<Sprite>();
    public bool fold = false;


    public Player(Color color, int numberOfPlayer, ShoppingList shoppinglist, List<Sprite> myManipulationCardsImages)
    {
        this.pawnColor = color;
        this.pawnsInHand = maxPawns;
        this.numberOfPlayer = numberOfPlayer;
        this.shoppinglist = shoppinglist;

        this.manipulationCards = new List<ManipulationCard>();

        GameObject playerPawns = GameObject.Find(String.Format("Player {0}", numberOfPlayer));
        for(int i = 0; i < maxPawns; i++)
        {
            GameObject pawn = playerPawns.transform.Find(String.Format("Pawn {0}", i)).gameObject;
            pawn.GetComponent<Renderer>().material.color = pawnColor;
            pawns[i] = pawn;
            var pawnManager = pawn.GetComponent<PawnManager>();
            pawnManager.player = this;
        }

        this.myManipulationCardsImages = myManipulationCardsImages;

    }

    public int PutDownPawn(int pawnNumber)
    {
        if (pawnsInHand > 0)
        {
            --pawnsInHand;
            pawns[pawnNumber].GetComponent<PawnManager>().onBoard = true;
        }
        return pawnsInHand;
    }

    public int PickUpPawn(int pawnNumber)
    {
        if (pawnsInHand < 5)
        {
            pawnsInHand++;
            pawns[pawnNumber].GetComponent<PawnManager>().onBoard = false;
        }
        return pawnsInHand;
    }

    public int PickUpPawn(PawnManager pawn)
    {
        if (pawnsInHand < 5)
        {
            pawnsInHand++;
            pawn.onBoard = false;
        }
        return pawnsInHand;
    }

    public virtual void MakeMove (){}

    public void PlayManipulationCard(ManipulationCard manipulationCard)
    {
        //manipulationCard.PlayCard();

        for (int i = 0; i < this.manipulationCards.Count; i++)  //usuwanie karty z listy
        {
            if ((int)this.manipulationCards[i].getCardName() == (int)manipulationCard.getCardName())
                this.manipulationCards.RemoveAt(i);
        }

        for (int i = 0; i < this.avlManipulationCards.Count; i++)   //usuwanie karty z dostępnej trójki
        {
            if ((int)this.avlManipulationCards[i].getCardName() == (int)manipulationCard.getCardName())
                this.avlManipulationCards.RemoveAt(i);
        }
    }

    public void SetManipulationCards()
    {
        this.manipulationCards.Clear();

        for (int i = 0; i < 10; i++)
        {
            this.manipulationCards.Add(new ManipulationCard(myManipulationCardsImages[i], i));
        }
        this.manipulationCards = Shuffle(manipulationCards);
    }

    public void SetAvlManipulationCards()
    {
        this.avlManipulationCards.Clear();

        for (int i = 0; i < 3; i++)
        {
            if (i < this.manipulationCards.Count)
                this.avlManipulationCards.Add(this.manipulationCards[i]);
        }
    }

    public void ToFold()
    {
        this.fold = true;
    }

    public void UnFold()
    {
        this.fold = false;
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

    public void AddItemToMyProductsEquipment(GameManager.Shop shop)
    {
        //int value = 0;
        switch(shop)
        {
            case GameManager.Shop.Clothing:
                productsEquipment.items[GameManager.Shop.Clothing] += 1;
                Debug.Log(string.Format("Podniosłem typ ciuch, mam teraz: {0}", productsEquipment.items[GameManager.Shop.Clothing]));
                break;
            case GameManager.Shop.Electronic:
                productsEquipment.items[GameManager.Shop.Electronic] += 1;
                Debug.Log(string.Format("Podniosłem typ elektronika, mam teraz: {0}", productsEquipment.items[GameManager.Shop.Electronic]));
                break;
            case GameManager.Shop.Furniture:
                productsEquipment.items[GameManager.Shop.Furniture] += 1;
                Debug.Log(string.Format("Podniosłem typ mebel, mam teraz: {0}", productsEquipment.items[GameManager.Shop.Furniture]));
                break;
            case GameManager.Shop.Grocery:
                productsEquipment.items[GameManager.Shop.Grocery] += 1;
                Debug.Log(string.Format("Podniosłem typ spozywczy, mam teraz: {0}", productsEquipment.items[GameManager.Shop.Grocery]));
                break;
            case GameManager.Shop.Newsstand:
                productsEquipment.items[GameManager.Shop.Newsstand] += 1;
                Debug.Log(string.Format("Podniosłem typ kiosk, mam teraz: {0}", productsEquipment.items[GameManager.Shop.Newsstand]));
                break;
        }
    }
}
