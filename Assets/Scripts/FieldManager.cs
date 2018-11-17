using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{

    //public PawnManager pawnManager;
    public GameManager gameManager;
    public bool isTaken { get; private set; }

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
        if (!isTaken)
        {
            gameManager.PutPawn(gameObject);
            isTaken = true;
        }
    }
}
