﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour {

    public GameManager gameManager;
    public bool isFull { get; private set; }
    public bool hasBlackPawn { get; set; }
    public GameManager.Shop shop;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        isFull = false;
        hasBlackPawn = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        gameObject.GetComponent<Renderer>().enabled = true;
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    private void OnMouseDown()
    {
        PutPawn();
    }

    public bool PutPawn()
    {
        /* zwraca true jeśli udało sie postawić pionek
         * zwraca false jeśli nie można było postawić pionka,
         * bo kolejka była już pełna */

        FieldManager[] children = gameObject.GetComponentsInChildren<FieldManager>();
        foreach (FieldManager child in children)
        {
            if (!child.isTaken)
            {
                child.PutPawn();
                return true;
            }
        }
        isFull = true;
        return false;
    }

    public bool IsEmpty()
    {
        FieldManager[] children = gameObject.GetComponentsInChildren<FieldManager>();
        foreach (FieldManager child in children)
        {
            if (child.isTaken)
            {
                return false;
            }
        }
        return true;
    }


    /// <summary>
    /// Przesuwa pionki o jedno pole do przodu w kolejce.
    /// Pierwszy pionek zostaje usunięty z kolejki.
    /// </summary>
    /// <returns>False jesli nie bylo pionków do przesunięcia.</returns>
    public void MovePawnsByOneFieldToTheFrontOfTheQueue()
    {
        if (!IsEmpty())
        {
            // usun pierwszy pionek z kolejki
            FieldManager[] children = gameObject.GetComponentsInChildren<FieldManager>();
            PawnManager pawn = children[0].GetComponentInChildren<PawnManager>();
            if (pawn != null)
            {
                pawn.gameObject.transform.SetPositionAndRotation(pawn.startingLocationPosition, pawn.startingLocationRotation);
                pawn.gameObject.transform.parent = null;
                if (pawn.player != null)
                {
                    pawn.player.AddItemToMyProductsEquipment(shop);
                    gameManager.scoreTab.UpdateScoreTab();
                }
            }
            // ustaw pierwsze pole jako niezajete jesli za nim nie ma juz pionkow
            if (!children[1].isTaken)
            {
                children[0].isTaken = false;
            }
            // pozostałe pionki przesuń do przodu
            for (int i = 1; i < children.Length; i++)
            {
                pawn = children[i].GetComponentInChildren<PawnManager>();
                if (pawn != null)
                    gameManager.MovePawn(pawn.gameObject, children[i - 1].gameObject);
                else
                // ustaw ostatnie pole z ktorego przesunieto pionek jako wolne
                {
                    children[i].isTaken = false;
                    break;
                }
            }
        }
    }
}
