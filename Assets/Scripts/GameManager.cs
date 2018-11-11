using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject vendor;
    enum Day { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday };
    List<Player> players = new List<Player>();
    int numberOfPlayers;
    int currentPlayer;

    // Use this for initialization
    void Start()
    {
        numberOfPlayers = 4;
        currentPlayer = 0;
        Color[] colors = { Color.magenta, Color.yellow, Color.green, Color.blue, Color.red };
        for(int i = 0; i < numberOfPlayers; i++)
        {
            players.Add(new Player(colors[i]));
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public void PutPawn(GameObject field)
    { 
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Pawn.prefab", typeof(GameObject));
        GameObject pawn = Instantiate(prefab, field.transform.position, field.transform.rotation) as GameObject;
        pawn.GetComponent<Renderer>().material.color = players[currentPlayer].pawnColor;
        pawn.transform.SetParent(field.transform);
        this.GetNextPlayer();
    }

    public void GetNextPlayer()
    {
        currentPlayer++;
        if (currentPlayer >= numberOfPlayers)
        {
            currentPlayer = 0;
        }
    }
}
