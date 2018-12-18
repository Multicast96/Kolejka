using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTab : MonoBehaviour {


    public Text playersScoreLabel;
    
    
    private List<Player> _players;
   

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public ScoreTab()
    {
        
    }

    public void GetPlayersList(List<Player> players)
    {
        _players = players;
    }

    public void UpdateScoreTab()
    {
        playersScoreLabel.text = "\t\t\t\tClothes:\tElectronics:\tFurnitures:\tGroceries:\tNewstands\n";
        for (int i = 0; i < _players.Count; i++)
        {
            playersScoreLabel.text += "Player " + (i+1) + "\t\t";
            playersScoreLabel.text += _players[i].productsEquipment.items[GameManager.Shop.Clothing] + "/" + _players[i].shoppinglist.items[GameManager.Shop.Clothing] + "\t\t\t\t";
            playersScoreLabel.text += _players[i].productsEquipment.items[GameManager.Shop.Electronic] + "/" + _players[i].shoppinglist.items[GameManager.Shop.Electronic] + "\t\t\t  ";
            playersScoreLabel.text += _players[i].productsEquipment.items[GameManager.Shop.Furniture] + "/" + _players[i].shoppinglist.items[GameManager.Shop.Furniture] + "\t\t\t\t ";
            playersScoreLabel.text += _players[i].productsEquipment.items[GameManager.Shop.Grocery] + "/" + _players[i].shoppinglist.items[GameManager.Shop.Grocery] + "\t\t\t\t  ";
            playersScoreLabel.text += _players[i].productsEquipment.items[GameManager.Shop.Newsstand] + "/" + _players[i].shoppinglist.items[GameManager.Shop.Newsstand] + "\t";
            playersScoreLabel.text += "\n";
        }
    }
}
