using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{

    //public PawnManager pawnManager;
    public GameManager gameManager;
    public bool isTaken;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        isTaken = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PutPawn()
    {
        if (!isTaken && gameManager.phase == GameManager.Phase.PawnsPlacing)
        {
            isTaken = true;
            gameManager.PutPawn(gameObject);
        }
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }

    /// <summary>
    /// Jeśli pole nie jest zajęte to ustawia to pole jako zajęte.
    /// </summary>
    /// <returns>isTaken</returns>
    public bool PutBlackPawn()
    {
        if (!isTaken)
        {
            isTaken = true;
            return true;
        }
        return false;
    }
}
