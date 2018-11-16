using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : Player
{
    private GameManager gameManager;
    public PlayerAI(GameManager _gameManager, Color color, int numberOfPlayer, ShoppingList shoppinglist) : 
        base(color, numberOfPlayer, shoppinglist)
    {
        gameManager = _gameManager;
    }

    public override void MakeMove()
    {
        Debug.Log(String.Format("AI player with ID: {0} makes move", numberOfPlayer));
        
        // TODO przenieść decyzję o ruchu do osobnej metody dla każdej fazy rozgrywki
        if(gameManager.phase == GameManager.Phase.PawnsPlacing)
        {
            gameManager.EndOfTurn();
        }
    }
}
