﻿using System.Collections;
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
    public ScoreTab scoreTab;
    public uint numberOfProductsInDeliveryTruck = 20;
    public uint numberOfDeliveryCards = 10;
    Object pawnPrefab;
  
    GameObject pawn;
    public GameObject[] blackPawns = new GameObject[5];

    // gracz zaczynajacy dzien
    public int markedPlayer;

    public int numberOfTurns;
    public int numberOfPlayers;

    public List<Sprite> shoppingListImages = new List<Sprite>();
    List<Player> players = new List<Player>();
    List<ShoppingList> shoppingLists = new List<ShoppingList>();
    public Dictionary<Shop, QueueManager> queues = new Dictionary<Shop, QueueManager>();

    public List<Sprite> manipulationGreenCardsImages = new List<Sprite>();
    public List<Sprite> manipulationRedCardsImages = new List<Sprite>();
    public List<Sprite> manipulationBlueCardsImages = new List<Sprite>();
    public List<Sprite> manipulationBrownCardsImages = new List<Sprite>();
    public List<Sprite> manipulationYellowCardsImages = new List<Sprite>();
    public Dictionary<int, List<Sprite>> manipulationCardsImages = new Dictionary<int, List<Sprite>>();

    //public Dictionary<int, List<GameObject>> pawnsInQueue = new Dictionary<int, List<GameObject>>();
    public Dictionary<int, List<int[]>> pawnOwners = new Dictionary<int, List<int[]>>();
    public Dictionary<int, List<GameObject>> fieldsDictionary = new Dictionary<int, List<GameObject>>();
    //public Dictionary<int, List<int>> pawnNumbers = new Dictionary<int, List<int>>();

    public ManipulationCard selectedManipulationCard;
    public ManipulationCard playedManipulationCard;
    public int[] selectedPawn = new int[2];
    public bool isPawnSelected = false;

    public Dictionary<Shop, StackManager> products = new Dictionary<Shop, StackManager>();
    public Dictionary<Shop, string> shopLiteral = new Dictionary<Shop, string>();

    

    // Use this for initialization
    void Start()
    {
        week = 1;
        day = Day.Monday;
        phase = Phase.PawnsPlacing;
        turn = 0;
        currentPlayer = 0;
        scoreTab.GetPlayersList(players);

        uiManager.UpdateDay(day);
        uiManager.UpdateWeek(week);
        uiManager.UpdatePlayer(currentPlayer);
        uiManager.UpdatePhase(phase);

        markedPlayer = 0;
        numberOfPlayers = 4;
        numberOfTurns = 4;

        queues.Add(Shop.Newsstand, GameObject.Find("Newsstand Queue").GetComponent<QueueManager>());
        GameObject.Find("Newsstand Queue").GetComponent<QueueManager>().shop = Shop.Newsstand;
        queues.Add(Shop.Grocery, GameObject.Find("Grocery Queue").GetComponent<QueueManager>());
        GameObject.Find("Grocery Queue").GetComponent<QueueManager>().shop = Shop.Grocery;
        queues.Add(Shop.Electronic, GameObject.Find("Electronic Queue").GetComponent<QueueManager>());
        GameObject.Find("Electronic Queue").GetComponent<QueueManager>().shop = Shop.Electronic;
        queues.Add(Shop.Furniture, GameObject.Find("Furniture Queue").GetComponent<QueueManager>());
        GameObject.Find("Furniture Queue").GetComponent<QueueManager>().shop = Shop.Furniture;
        queues.Add(Shop.Clothing, GameObject.Find("Clothing Queue").GetComponent<QueueManager>());
        GameObject.Find("Clothing Queue").GetComponent<QueueManager>().shop = Shop.Clothing;
        queues.Add(Shop.Bazaar, GameObject.Find("Bazaar Queue").GetComponent<QueueManager>());
        GameObject.Find("Bazaar Queue").GetComponent<QueueManager>().shop = Shop.Bazaar;

        shopLiteral.Add(Shop.Newsstand, "Newsstand");
        shopLiteral.Add(Shop.Grocery, "Grocery");
        shopLiteral.Add(Shop.Electronic, "Electronic");
        shopLiteral.Add(Shop.Furniture, "Furniture");
        shopLiteral.Add(Shop.Clothing, "Clothing");
        shopLiteral.Add(Shop.Bazaar, "Bazaar");

        products.Add(Shop.Newsstand, GameObject.Find("Newsstand Store Card Field").GetComponent<StackManager>());
        products.Add(Shop.Grocery, GameObject.Find("Grocery Store Card Field").GetComponent<StackManager>());
        products.Add(Shop.Electronic, GameObject.Find("Electronic Store Card Field").GetComponent<StackManager>());
        products.Add(Shop.Furniture, GameObject.Find("Furniture Store Card Field").GetComponent<StackManager>());
        products.Add(Shop.Clothing, GameObject.Find("Clothing Store Card Field").GetComponent<StackManager>());
        
        shoppingLists.Add(new ShoppingList(shoppingListImages[0], "wyposazyc kuchnie", Electronic: 4, Grocery: 0, Newsstand: 1, Clothing: 2, Furniture: 3));
        shoppingLists.Add(new ShoppingList(shoppingListImages[1], "wyprawic pierwsza komunie", Electronic: 3, Grocery: 4, Newsstand: 0, Clothing: 1, Furniture: 2));
        shoppingLists.Add(new ShoppingList(shoppingListImages[2], "spedzic urlop na dzialce", Electronic: 2, Grocery: 3, Newsstand: 4, Clothing: 0, Furniture: 1));
        shoppingLists.Add(new ShoppingList(shoppingListImages[3], "wyslac dzieci na kolonie", Electronic: 1, Grocery: 2, Newsstand: 3, Clothing: 4, Furniture: 0));
        shoppingLists.Add(new ShoppingList(shoppingListImages[4], "urzadzic mieszkanie z przydzialu", Electronic: 0, Grocery: 1, Newsstand: 2, Clothing: 3, Furniture: 4));

        manipulationCardsImages.Add(0, manipulationBlueCardsImages);
        manipulationCardsImages.Add(1, manipulationRedCardsImages);
        manipulationCardsImages.Add(2, manipulationGreenCardsImages);
        manipulationCardsImages.Add(3, manipulationBrownCardsImages);
        manipulationCardsImages.Add(4, manipulationYellowCardsImages);


        for (int i = 0; i < 6; i++)
        {
            pawnOwners[i] = new List<int[]>();
            fieldsDictionary[i] = new List<GameObject>();
        }
            //pawnsInQueue[i] = new List<GameObject>();



            // Ustawienie kart towarów na samochodach
        int bazaarFieldCounter = 1;
        foreach(string shopName in System.Enum.GetNames(typeof(Shop))){
            if (shopName == Shop.Bazaar.ToString()) continue;

            var shopDeliveryTruck = GameObject.Find(shopName + " Store Truck").GetComponent<StackManager>();
            shopDeliveryTruck.gapBetweenObjectsInStack = 0.05f;
            var productCardPrefab = Resources.Load<GameObject>("Prefabs/Cards/"+shopName+" Store Delivery Card");
            for(int i = 0; i < numberOfProductsInDeliveryTruck; i++)
            {
                var newDeliveryCard = Instantiate(productCardPrefab, Vector3.up, Quaternion.identity);
                shopDeliveryTruck.Push(newDeliveryCard);
            }
            var bazaarField = GameObject.Find(string.Format("Bazaar Card Field {0}", bazaarFieldCounter++)).GetComponent<StackManager>();
            var baazarDeliveryCard = Instantiate(productCardPrefab, Vector3.up, Quaternion.identity);
            bazaarField.Push(baazarDeliveryCard);
        }

        // ustawienie kart dostawy na planszy
        var deliveryCardPrefab = Resources.Load<GameObject>("Prefabs/Cards/Delivery Card");
        var trashGameObject = GameObject.Find("Trash").GetComponent<StackManager>();
        var targetPosition = trashGameObject.transform.position;
        for(int i = 0; i < numberOfDeliveryCards; i++)
        {
            var newCard = Instantiate(deliveryCardPrefab, Vector3.up, Quaternion.identity);
            trashGameObject.Push(newCard);
        }

        MoveCardsFromTrashShufleAndCreateDeliveryStack();

        // Pionek przekupki na bazarze
        vendor = GameObject.Find("Vendor");

        System.Random rnd = new System.Random();
        int firstList = rnd.Next(0, 5);

        Color[] colors = { Color.magenta, Color.yellow, Color.green, Color.blue, Color.red };
        for(int i = 0; i < numberOfPlayers - 1; i++)
        {
            players.Add(new Player(colors[i], i, shoppingLists[(firstList+i)%5], manipulationCardsImages[i]));
        }
        int aiPlayerId = numberOfPlayers - 1; // Id gracza SI
        players.Add(new PlayerAI(this, colors[aiPlayerId], aiPlayerId, shoppingLists[(firstList + aiPlayerId) % 5], manipulationCardsImages[aiPlayerId])); // jeden gracz SI
        uiManager.UpdateShoppingList(players[currentPlayer].shoppinglist.image);
        
        scoreTab.GetPlayersList(players);
        scoreTab.UpdateScoreTab();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsTabKeyPressed();
    }

    private void MoveCardsFromTrashShufleAndCreateDeliveryStack()
    {
        var trash = GameObject.Find("Trash").GetComponent<StackManager>();
        var allChildren = new List<Transform>();
        while (!trash.IsEmpty())
        {
            allChildren.Add(trash.Pop().transform);
        }
        allChildren.Shuffle();

        var stackOfDeliveryCards = GameObject.Find("Stack of Delivery Cards").GetComponent<StackManager>();
        foreach(Transform deliveryCard in allChildren)
        {
            stackOfDeliveryCards.Push(deliveryCard.gameObject);
        }
    }

    private void CheckIsTabKeyPressed()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            uiManager.gameObject.SetActive(false);
            scoreTab.gameObject.SetActive(true);
        }
        else
        {
            scoreTab.gameObject.SetActive(false);
            uiManager.gameObject.SetActive(true);
        }
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
            GameObject pawn = players[currentPlayer].pawns[pawnNumber].gameObject;
            players[currentPlayer].PutDownPawn();
            MovePawn(pawn, field);

            int tmpQue;
            QueueManager myQueue = field.transform.parent.GetComponent<QueueManager>();
            
            if(myQueue.name == "Newsstand Queue")
                tmpQue = 0;
            else if (myQueue.name == "Grocery Queue")
                tmpQue = 1;
            else if (myQueue.name == "Electronic Queue")
                tmpQue = 2;
            else if (myQueue.name == "Furniture Queue")
                tmpQue = 3;
            else if (myQueue.name == "Clothing Queue")
                tmpQue = 4;
            else //if(myQueue.name == "Bazaar Queue")
                tmpQue = 5;

            int[] tmpOwner = new int[2];
            tmpOwner[0] = currentPlayer;
            tmpOwner[1] = pawnNumber;
            pawnOwners[tmpQue].Add(tmpOwner);
            fieldsDictionary[tmpQue].Add(field);
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
        pawn.transform.SetParent(field.transform);
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
                var pawn = GameObject.Find(string.Format("{0} Vendor", shopLiteral[queue.Key]));
                foreach(FieldManager field in queue.Value.gameObject.GetComponentsInChildren<FieldManager>())
                {
                    if (field.PutBlackPawn())
                    {
                        int tmpQue;
                        QueueManager myQueue = field.transform.parent.GetComponent<QueueManager>();

                        if (myQueue.name == "Newsstand Queue")
                            tmpQue = 0;
                        else if (myQueue.name == "Grocery Queue")
                            tmpQue = 1;
                        else if (myQueue.name == "Electronic Queue")
                            tmpQue = 2;
                        else if (myQueue.name == "Furniture Queue")
                            tmpQue = 3;
                        else if (myQueue.name == "Clothing Queue")
                            tmpQue = 4;
                        else
                            tmpQue = 5;

                        pawn = Instantiate(prefab, field.transform.position, field.transform.rotation) as GameObject;
                        queue.Value.hasBlackPawn = true;

                        blackPawns[tmpQue] = pawn;
                        int[] tmpOwner = new int[2];
                        tmpOwner[0] = 9;
                        tmpOwner[1] = tmpQue;
                        pawnOwners[tmpQue].Add(tmpOwner);
                        fieldsDictionary[tmpQue].Add(field.gameObject);

                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Dostawia karte towaru na pole podanego sklepu.
    /// </summary>
    private void PutStoreCardOnStoreField(Shop store, int numberOfProducts)
    {
        string truck = string.Format("{0} Store Truck", shopLiteral[store]);
        var storeTruckStack = GameObject.Find(truck).GetComponent<StackManager>();
        var storeCardFieldStack = products[store];
        for (int i = 0; i < numberOfProducts; i++)
        {
            var card = storeTruckStack.Pop();
            storeCardFieldStack.Push(card);
        }
    }

    private void SupplyPhase()
    {
        var deliveryStack = GameObject.Find("Stack of Delivery Cards").GetComponent<StackManager>();
        for (int i = 1; i < 4; i++)
        {
            if (deliveryStack.IsEmpty()) break;
            var deliveryField = GameObject.Find(string.Format("Delivery Cards Field {0}", i));
            var card = deliveryStack.Pop();
            card.GetComponent<DeliveryCard>().ShowDelivery();
            card.transform.parent = deliveryField.transform;
            card.transform.position = deliveryField.transform.position;
            PutStoreCardOnStoreField(card.GetComponent<DeliveryCard>().shop, card.GetComponent<DeliveryCard>().productsToBeDelivered);
        }
    }

    private void TakeProducts()
    {
        foreach(KeyValuePair<Shop, StackManager> product_pile in products)
        {
            // Zbieraj przedmioty jesli kolejka do sklepu nie jest pusta
            // i sklep nie jest pusty
            while (!product_pile.Value.IsEmpty() && !queues[product_pile.Key].IsEmpty())
            {
                queues[product_pile.Key].MovePawnsByOneFieldToTheFrontOfTheQueue();
                product_pile.Value.Pop();
            }
        }
    }

    public void SelectManipulationCard(int number)
    {
        selectedManipulationCard = players[currentPlayer].avlManipulationCards[number];

        if(players[currentPlayer].avlManipulationCards[number].getCardName() == ManipulationCard.ManipulationCardName.KolegaWKomitecie)
        {
            players[currentPlayer].avlManipulationCards[number].PlayCard();
        }
    }

    public void SelectQueue(string queue)
    {
        int tmpQue;
        if (queue == "Newsstand Queue")
            tmpQue = 0;
        else if (queue == "Grocery Queue")
            tmpQue = 1;
        else if (queue == "Electronic Queue")
            tmpQue = 2;
        else if (queue == "Furniture Queue")
            tmpQue = 3;
        else if (queue == "Clothing Queue")
            tmpQue = 4;
        else //if(myQueue.name == "Bazaar Queue")
            tmpQue = 5;

        if (playedManipulationCard.getCardName() == ManipulationCard.ManipulationCardName.ListaSpoleczna)
        {
            List<int[]> tmpQueueList = new List<int[]>();
            foreach (int[] pawn in pawnOwners[tmpQue])
                tmpQueueList.Add(pawn);
            tmpQueueList.Reverse();
            pawnOwners[tmpQue].Clear();

            int i = 0;
            foreach (GameObject field in fieldsDictionary[tmpQue])
            {
                int[] tmpPawn = tmpQueueList[i];

                if(tmpPawn[0] != 9)
                    MovePawn(players[tmpPawn[0]].pawns[tmpPawn[1]], field);
                else
                    MovePawn(blackPawns[tmpPawn[1]], field);

                pawnOwners[tmpQue].Add(tmpPawn);
                i++;
            }

            players[currentPlayer].PlayManipulationCard(playedManipulationCard);
            selectedManipulationCard = null;
            playedManipulationCard = null;

            EndOfTurn();
        }
        else if (playedManipulationCard.getCardName() == ManipulationCard.ManipulationCardName.SzczesliwyTraf && isPawnSelected == true)
        {

        }

        

    }

    public void PlayManipulationCard()
    {
        playedManipulationCard = selectedManipulationCard;

        if( playedManipulationCard.getCardName() == ManipulationCard.ManipulationCardName.KolegaWKomitecie)
        {
            //pokaż na 3 sekundy 2 pierwsze karty dostaw
        }
    }

    public void FoldManipulationCard()
    {

        selectedManipulationCard = null;
        playedManipulationCard = null;
        players[currentPlayer].ToFold();
        EndOfTurn();
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
        if(phase == Phase.Supply)
        {
            EndOfTurn();
        }

        if(phase == Phase.Manipulations)
        {
            if(day != Day.Saturday && turn == 0)
            {
                if (day == Day.Monday)
                    players[currentPlayer].SetManipulationCards();

                players[currentPlayer].SetAvlManipulationCards();
                players[currentPlayer].UnFold();
            }

            if (players[currentPlayer].fold == true)
                EndOfTurn();

            isPawnSelected = false;
            uiManager.UpdateManipulationCards(players[currentPlayer].avlManipulationCards[0].getImage(), players[currentPlayer].avlManipulationCards[1].getImage(), players[currentPlayer].avlManipulationCards[2].getImage());
            
            //players[currentPlayer].avlManipulationCards.;
        }


        players[currentPlayer].MakeMove();


        if (phase == Phase.Supply)
        {
            this.SupplyPhase();
            phase++;
            // TODO usunąć poniższą linijkę gdy faza manipulacji będzie robiona
            phase++;
            uiManager.UpdatePhase(phase);
        }

        if (phase == Phase.Opening)
        {
            TakeProducts();
            phase++;
            uiManager.UpdatePhase(phase);
        }
    }
}


