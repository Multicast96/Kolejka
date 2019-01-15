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
        }

        this.myManipulationCardsImages = myManipulationCardsImages;

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

    public void AddItemToMyProductsEquipment(ProductCard card)
    {
        int value = 0;
        switch(card.MyType)
        {
            case ProductCard.Type.CLOTHING:
                Debug.Log("Podniosłem typ ciuch");
                value = productsEquipment.items[GameManager.Shop.Clothing];
                value++;                
                productsEquipment.items.Remove(GameManager.Shop.Clothing);
                productsEquipment.items.Add(GameManager.Shop.Clothing, value);
                break;
            case ProductCard.Type.ELECTRONICS:
                Debug.Log("Podniosłem typ elektronika");
                value = productsEquipment.items[GameManager.Shop.Electronic];
                value++;
                productsEquipment.items.Remove(GameManager.Shop.Electronic);
                productsEquipment.items.Add(GameManager.Shop.Electronic, value);
                break;
            case ProductCard.Type.FURNITURE:
                Debug.Log("Podniosłem typ mebel");
                value = productsEquipment.items[GameManager.Shop.Furniture];
                value++;
                productsEquipment.items.Remove(GameManager.Shop.Furniture);
                productsEquipment.items.Add(GameManager.Shop.Clothing, value);
                break;
            case ProductCard.Type.GROCERY:
                Debug.Log("Podniosłem typ spozywczy");
                value = productsEquipment.items[GameManager.Shop.Grocery];
                value++;
                productsEquipment.items.Remove(GameManager.Shop.Grocery);
                productsEquipment.items.Add(GameManager.Shop.Grocery, value);
                break;
            case ProductCard.Type.NEWSSTAND:
                Debug.Log("Podniosłem typ kiosk");
                value = productsEquipment.items[GameManager.Shop.Newsstand];
                value++;
                productsEquipment.items.Remove(GameManager.Shop.Newsstand);
                productsEquipment.items.Add(GameManager.Shop.Newsstand, value);
                break;
        }
    }
}
