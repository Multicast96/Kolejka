using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnManager : MonoBehaviour {

    public GameManager gameManager;
    public Vector3 startingLocationPosition { get; private set; }
    public Quaternion startingLocationRotation { get; private set; }
    public Player player;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        startingLocationPosition = this.gameObject.transform.position;
        startingLocationRotation = this.gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update () {
		
	}
    
}