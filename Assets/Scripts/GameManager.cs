using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject vendor;
    public enum Day { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday };
    public enum Phase { PawnsPlacing, Supply, Manipulations, Opening, Trading, TePeZet };

    public enum Shop { Newsstand, Grocery, Electronic, Furniture, Clothing, Bazaar}

    public int week;
    public Day day;
    public Phase phase;
    public int turn;
    public int currentPlayer;

    // gracz zaczynajacy dzien
    public int markedPlayer;

    public int numberOfTurns;
    public int numberOfPlayers;

    List<Player> players = new List<Player>();
    List<ShoppingList> shoppingLists = new List<ShoppingList>();
    Dictionary<Shop, QueueManager> queues = new Dictionary<Shop, QueueManager>();

    // Use this for initialization
    void Start()
    {
        week = 1;
        day = Day.Monday;
        phase = Phase.PawnsPlacing;
        turn = 0;
        currentPlayer = 0;

        markedPlayer = 0;
        numberOfPlayers = 4;
        numberOfTurns = 4;

        queues.Add(Shop.Newsstand, GameObject.Find("Newsstand Queue").GetComponent<QueueManager>());
        queues.Add(Shop.Grocery, GameObject.Find("Grocery Queue").GetComponent<QueueManager>());
        queues.Add(Shop.Electronic, GameObject.Find("Electronic Queue").GetComponent<QueueManager>());
        queues.Add(Shop.Furniture, GameObject.Find("Furniture Queue").GetComponent<QueueManager>());
        queues.Add(Shop.Clothing, GameObject.Find("Clothing Queue").GetComponent<QueueManager>());
        queues.Add(Shop.Bazaar, GameObject.Find("Bazaar Queue").GetComponent<QueueManager>());

        shoppingLists.Add(new ShoppingList("wyposazyc kuchnie", 4, 0, 1, 2, 3));
        shoppingLists.Add(new ShoppingList("wyprawic pierwsza komunie", 3, 4, 0, 1, 2));
        shoppingLists.Add(new ShoppingList("spedzic urlop na dzialce", 2, 3, 4, 0, 1));
        shoppingLists.Add(new ShoppingList("wyslac dzieci na kolonie", 1, 2, 3, 4, 0));
        shoppingLists.Add(new ShoppingList("urzadzic mieszkanie z przydzialu", 0, 1, 2, 3, 4));

        System.Random rnd = new System.Random();
        int firstList = rnd.Next(1, 5);

        Color[] colors = { Color.magenta, Color.yellow, Color.green, Color.blue, Color.red };
        for(int i = 0; i < numberOfPlayers - 1; i++)
        {
            players.Add(new Player(colors[i], i, shoppingLists[(firstList+i)%5]));
        }
        int aiPlayerId = numberOfPlayers - 1; // Id gracza SI
        players.Add(new PlayerAI(this, colors[aiPlayerId], aiPlayerId, shoppingLists[(firstList + aiPlayerId) % 5])); // jeden gracz SI
    }

    // Update is called once per frame
    void Update() {

    }

    public void PutPawn(GameObject field)
    {
        if (players[currentPlayer].pawnsInHand > 0 && phase == Phase.PawnsPlacing)
        {
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Pawn.prefab", typeof(GameObject));
            GameObject pawn = Instantiate(prefab, field.transform.position, field.transform.rotation) as GameObject;
            pawn.GetComponent<Renderer>().material.color = players[currentPlayer].pawnColor;
            pawn.transform.SetParent(field.transform);
            players[currentPlayer].PutDownPawn();
        }

        this.EndOfTurn();
    }

    public void GetNextPlayer()
    {
        currentPlayer = (currentPlayer + 1) % numberOfPlayers;
    }
  
    public void EndOfTurn()
    {
        this.GetNextPlayer();
 
        if (currentPlayer == markedPlayer)
        {
            if (turn < numberOfTurns)
                turn++;
            else
            {
                turn = 0;

                if (phase != Phase.TePeZet)
                    phase++;
                else
                {
                    phase = Phase.PawnsPlacing;

                    if (markedPlayer < numberOfPlayers)
                        markedPlayer++;
                    else
                        markedPlayer = 0;

                    if (day != Day.Saturday)
                        day++;
                    else
                    {
                        day = Day.Monday;
                        week++;
                    }
                }
            }
        }
        players[currentPlayer].MakeMove();
    }
}
