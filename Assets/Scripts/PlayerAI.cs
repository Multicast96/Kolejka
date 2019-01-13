using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAI : Player
{
    private GameManager gameManager;
    private Hashtable pawnsInQueue = new Hashtable();
    public PlayerAI(GameManager gameManager, Color color, int numberOfPlayer, ShoppingList shoppinglist, List<Sprite> myManipulationCardsImages) : 
        base(color, numberOfPlayer, shoppinglist, myManipulationCardsImages)
    {
        this.gameManager = gameManager;
    }

    public override void MakeMove()
    {
        Debug.Log(String.Format("AI player with ID: {0} makes move", numberOfPlayer));


        switch (gameManager.phase)
        {
            case GameManager.Phase.PawnsPlacing:
            {
                PlacingPawnsPhase();
                break;
            }
        }
    }
    //aa
    //Przepis na zwycięstwo w tej fazie
    // +10 za "nowy" pionek w kolejce
    // +n*5 za każdy (n) pionek który trzeba jeszcze dołożyć do tej kolejki (mamy na liście zakupów przedmiot z tego sklepu)
    // +8-(n) za (n-te) miejsce w kolejce
    // +15 jeśli wszyscy gracze zrobią ruch i nie będzie już można dołożyć pionka do tej kolejki. (wyrzucone)
    private void PlacingPawnsPhase()
    {
        var results = new Dictionary<GameManager.Shop, int>();
        foreach (var queue in gameManager.queues)
        {
            int thisQueueResult = 0;
            if (!pawnsInQueue.Contains(queue.Key)) pawnsInQueue[queue.Key] = 0;

            if (!shoppinglist.items.ContainsKey(queue.Key))
            {
                results[queue.Key] = 0;
                continue;
            }

            if ((int)pawnsInQueue[queue.Key] == 0)
                thisQueueResult += 10 + 5 * shoppinglist.items[queue.Key];
            else
                thisQueueResult += 5 * (shoppinglist.items[queue.Key] - (int)pawnsInQueue[queue.Key]);


            FieldManager[] children = queue.Value.gameObject.GetComponentsInChildren<FieldManager>();
            int placesTaken = 0;
            foreach (FieldManager child in children) { if (child.isTaken) placesTaken++; };
            thisQueueResult += 8 - placesTaken;

            results[queue.Key] = thisQueueResult;
        }

        var sortedResults = (from kv in results orderby kv.Value descending select kv).ToList();

        foreach (var result in sortedResults)
        {
            if (!shoppinglist.items.ContainsKey(result.Key)) continue;
            Debug.Log(String.Format("Kolejka:{0} Ilość produktów:{1} Ilość pionków:{2} Wynik:{3}", result.Key, shoppinglist.items[result.Key], pawnsInQueue[result.Key], result.Value));
        }

        foreach (var result in sortedResults)
        {
            if (gameManager.queues[result.Key].PutPawn())
            {
                Debug.Log(String.Format("Placing in: {0}", result.Key.ToString()));
                pawnsInQueue[result.Key] = (int)pawnsInQueue[result.Key] + 1;
                break;
            }
        }

    }
}
