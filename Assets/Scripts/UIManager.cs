using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text dayText;
    public Text weekText;
    public Text playerText;
    public Text phaseText;
    public Image shoppingList;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateDay(GameManager.Day day)
    {
        dayText.text = "Day: " + day.ToString();
    }

    public void UpdateWeek(int week)
    {
        weekText.text = "Week: " + week;
    }

    public void UpdatePlayer(int player)
    {
        playerText.text = "Now playing: Player " + (player + 1);
    }

    public void UpdatePhase(GameManager.Phase phase)
    {
        phaseText.text = "Phase: " + phase.ToString();
    }

    public void UpdateShoppingList(Sprite image)
    {
        shoppingList.sprite = image;
    }
}
