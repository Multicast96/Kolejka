using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject vendor;
    public enum Day { Monday=1, Tuesday, Wednesday, Thursday, Friday, Saturday };
    public enum Phase { PawnsPlacing, Supply, Manipulations, Opening, Trading, TePeZet };

    public enum Shop { Newsstand, Grocery, Electronic, Furniture, Clothing, Bazaar}

    public int week;
    public Day day;
    public Phase phase;
    public int turn;
    public int currentPlayer;
    public UIManager uiManager;
    Object pawnPrefab;
    GameObject pawn;

    // gracz zaczynajacy dzien
    public int markedPlayer;

    public int numberOfTurns;
    public int numberOfPlayers;

    public List<Sprite> shoppingListImages = new List<Sprite>();
    List<Player> players = new List<Player>();
    List<ShoppingList> shoppingLists = new List<ShoppingList>();
    public Dictionary<Shop, QueueManager> queues = new Dictionary<Shop, QueueManager>();
    

    // Use this for initialization
    void Start()
    {
        week = 1;
        day = Day.Monday;
        phase = Phase.PawnsPlacing;
        turn = 0;
        currentPlayer = 0;

        uiManager.UpdateDay(day);
        uiManager.UpdateWeek(week);
        uiManager.UpdatePlayer(currentPlayer);
        uiManager.UpdatePhase(phase);

        markedPlayer = 0;
        numberOfPlayers = 4;
        numberOfTurns = 4;

        queues.Add(Shop.Newsstand, GameObject.Find("Newsstand Queue").GetComponent<QueueManager>());
        queues.Add(Shop.Grocery, GameObject.Find("Grocery Queue").GetComponent<QueueManager>());
        queues.Add(Shop.Electronic, GameObject.Find("Electronic Queue").GetComponent<QueueManager>());
        queues.Add(Shop.Furniture, GameObject.Find("Furniture Queue").GetComponent<QueueManager>());
        queues.Add(Shop.Clothing, GameObject.Find("Clothing Queue").GetComponent<QueueManager>());
        queues.Add(Shop.Bazaar, GameObject.Find("Bazaar Queue").GetComponent<QueueManager>());

        shoppingLists.Add(new ShoppingList(shoppingListImages[0], "wyposazyc kuchnie", Electronic: 4, Grocery: 0, Newsstand: 1, Clothing: 2, Furniture: 3));
        shoppingLists.Add(new ShoppingList(shoppingListImages[1], "wyprawic pierwsza komunie", Electronic: 3, Grocery: 4, Newsstand: 0, Clothing: 1, Furniture: 2));
        shoppingLists.Add(new ShoppingList(shoppingListImages[2], "spedzic urlop na dzialce", Electronic: 2, Grocery: 3, Newsstand: 4, Clothing: 0, Furniture: 1));
        shoppingLists.Add(new ShoppingList(shoppingListImages[3], "wyslac dzieci na kolonie", Electronic: 1, Grocery: 2, Newsstand: 3, Clothing: 4, Furniture: 0));
        shoppingLists.Add(new ShoppingList(shoppingListImages[4], "urzadzic mieszkanie z przydzialu", Electronic: 0, Grocery: 1, Newsstand: 2, Clothing: 3, Furniture: 4));

        // Pionek przekupki na bazarze
        vendor = GameObject.Find("Vendor");

        System.Random rnd = new System.Random();
        int firstList = rnd.Next(0, 5);

        Color[] colors = { Color.magenta, Color.yellow, Color.green, Color.blue, Color.red };
        for(int i = 0; i < numberOfPlayers - 1; i++)
        {
            players.Add(new Player(colors[i], i, shoppingLists[(firstList+i)%5]));
        }
        int aiPlayerId = numberOfPlayers - 1; // Id gracza SI
        players.Add(new PlayerAI(this, colors[aiPlayerId], aiPlayerId, shoppingLists[(firstList + aiPlayerId) % 5])); // jeden gracz SI
        uiManager.UpdateShoppingList(players[currentPlayer].shoppinglist.image);
    }

    // Update is called once per frame
    void Update() {

    }

    /// <summary>
    /// Postaw pionek z ręki gracza na planszę.
    /// </summary>
    /// <param name="field">Pole na które ma być postawiony pionek</param>
    public void PutPawn(GameObject field)
    {
        // Postaw nowy pionek - metoda dla gracza
        if (players[currentPlayer].pawnsInHand > 0 && phase == Phase.PawnsPlacing)
        {
            int pawnNumber = Player.maxPawns - players[currentPlayer].pawnsInHand;
            GameObject pawn = players[currentPlayer].pawns[pawnNumber];
            players[currentPlayer].PutDownPawn();
            MovePawn(pawn, field);
        }

        this.EndOfTurn();
    }

    /// <summary>
    /// Metoda do przesuwania pojedynczego pionka.
    /// </summary>
    /// <param name="pawn">Obiekt pionka</param>
    /// <param name="field">Obiekt pola na który ma być przesunięty pionek</param>
    public void MovePawn(GameObject pawn, GameObject field)
    {
        pawn.transform.SetPositionAndRotation(field.transform.position, field.transform.rotation);
    }


    public void GetNextPlayer()
    {
        currentPlayer = (currentPlayer + 1) % numberOfPlayers;
        uiManager.UpdatePlayer(currentPlayer);
        uiManager.UpdateShoppingList(players[currentPlayer].shoppinglist.image);
    }

    /// <summary>
    /// Metoda do uaktualniania dnia. Ustawia przekupkę na nowym polu na bazarze
    /// </summary>
    /// <param name="day">Aktualny dzień</param>
    void UpdateDay(Day day)
    {
        string fieldName = string.Format("Vendor Field {0}", (int)day);
        GameObject field = GameObject.Find(fieldName);
        MovePawn(vendor, field);
    }

    /// <summary>
    /// Sprawdza czy wszyscy gracze rozstawili swoje wszystkie pionki oraz czy w danej
    /// kolejce jest już czarny pionek. Nie dostawia pionka na bazar.
    /// Jeśli gracze rozstawili pionki: stawia czarne pionki na koncu kolejek w których 
    /// jest jeszcze miejsce i nie ma czarnego pionka.
    /// Jeśli nie: nic nie robi.
    /// </summary>
    void PutDownBlackPawns()
    {
        foreach(KeyValuePair<Shop, QueueManager> queue in queues)
        {
            if (queue.Key == Shop.Bazaar)
                break;
            if((!queue.Value.isFull) && (!queue.Value.hasBlackPawn))
            {
                Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Black Pawn.prefab", typeof(GameObject));
                foreach(FieldManager field in queue.Value.gameObject.GetComponentsInChildren<FieldManager>())
                {
                    if (field.PutBlackPawn())
                    {
                        GameObject pawn = Instantiate(prefab, field.transform.position, field.transform.rotation) as GameObject;
                        queue.Value.hasBlackPawn = true;
                        break;
                    }
                }
            }
        }
    }
  
    public void EndOfTurn()
    {
        this.GetNextPlayer();
        if (currentPlayer == markedPlayer)
        {
            if (phase == Phase.PawnsPlacing)
            {
                bool placeBlackPawns = true;
                // Sprawdź czy każdy gracz rozłożył wszystkie pionki
                // Jeśli tak to rozłóż czarne pionki
                foreach (Player player in players)
                {
                    if (player.pawnsInHand > 0)
                    {
                        placeBlackPawns = false;
                        break;
                    }
                }
                if (placeBlackPawns)
                    this.PutDownBlackPawns();
            }
            if (turn < numberOfTurns) { 
                turn++;
            }
            else
            {
                turn = 0;

                if (phase != Phase.TePeZet)
                {
                    phase++;
                    uiManager.UpdatePhase(phase);
                }
                else
                {
                    phase = Phase.PawnsPlacing;

                    if (markedPlayer < numberOfPlayers)
                        markedPlayer++;
                    else
                        markedPlayer = 0;

                    if (day != Day.Saturday)
                    {
                        day++;
                        uiManager.UpdateDay(day);
                        this.UpdateDay(day);
                    }
                    else
                    {
                        day = Day.Monday;
                        week++;
                        uiManager.UpdateWeek(week);
                    }
                }
            }
        }

        players[currentPlayer].MakeMove();
    }
}
