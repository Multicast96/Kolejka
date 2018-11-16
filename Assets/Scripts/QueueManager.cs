using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour {

    public GameManager gameManager;
    bool isFull;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        isFull = true;
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
        FieldManager[] children = gameObject.GetComponentsInChildren<FieldManager>();
        foreach (FieldManager child in children)
        {
            if (!child.isTaken)
            {
                child.PutPawn();
                break;
            }
        }
    }
}
