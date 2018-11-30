using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : Player
{
    private GameManager gameManager;
    private Hashtable pawnsInQueue = new Hashtable();
    public PlayerAI(GameManager gameManager, Color color, int numberOfPlayer, ShoppingList shoppinglist) : 
        base(color, numberOfPlayer, shoppinglist)
    {
        this.gameManager = gameManager;
    }

    public override void MakeMove()
    {
        Debug.Log(String.Format("AI player with ID: {0} makes move", numberOfPlayer));
        Debug.Log(String.Format("My shopping list is:"));
        foreach(var listItem in shoppinglist.items)
        {
            Debug.Log(listItem.Key.ToString() + ": " + listItem.Value);
        }
        
        // TODO przenieść decyzję o ruchu do osobnej metody dla każdej fazy rozgrywki
        if(gameManager.phase == GameManager.Phase.PawnsPlacing)
        {
            GameManager.Shop bestResultShop = 0;
            int bestResult = 0;
            foreach(var queue in gameManager.queues)
            {
                int thisQueueResult = 0;
                if (!shoppinglist.items.ContainsKey(queue.Key))
                    continue;

                if (!pawnsInQueue.ContainsKey(queue.Key))
                    thisQueueResult += 10 + 5 * shoppinglist.items[queue.Key];
                else
                    thisQueueResult += 5 * (shoppinglist.items[queue.Key] - (int)pawnsInQueue[queue.Key]);

                //TODO dodać punkty za numer miejsca w kolejce

                //TODO dodać punkty za możliwość braku miejsca w kolejce w następnej rundzie

                if(thisQueueResult > bestResult)
                {
                    bestResult = thisQueueResult;
                    bestResultShop = queue.Key;
                    if (!pawnsInQueue.ContainsKey(queue.Key)) pawnsInQueue[queue.Key] = 1;
                    else pawnsInQueue[queue.Key] = (int)pawnsInQueue[queue.Key] + 1;
                }
            }

            //TODO zapisywać wyniki wszystkich kolejek i wybierać najlepszą. Jeśli nie ma miejsca to następna kolejka.
            //Debug.Log("Best: " + bestResultShop);
            if (!gameManager.queues[bestResultShop].PutPawn()) throw new Exception("No place for pawn");
        }
    }

    private void PlacingPawnsPhase()
    {

    }
}
