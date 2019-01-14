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

    public Image manipulationCard_1;
    public Image manipulationCard_2;
    public Image manipulationCard_3;

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

    public void UpdateManipulationCards()
    {
        Color colorVar = new Color(1f, 1f, 1f, 0f);
        manipulationCard_1.color = colorVar;
        manipulationCard_2.color = colorVar;
        manipulationCard_3.color = colorVar;
    }

    public void UpdateManipulationCards(Sprite image1)
    {
        Color colorVar = new Color(1f, 1f, 1f, 1f);
        manipulationCard_1.sprite = image1;
        manipulationCard_1.color = colorVar;

        colorVar = new Color(1f, 1f, 1f, 0f);
        manipulationCard_2.color = colorVar;
        manipulationCard_3.color = colorVar;
    }

    public void UpdateManipulationCards(Sprite image1, Sprite image2)
    {
        Color colorVar = new Color(1f, 1f, 1f, 1f);
        manipulationCard_1.sprite = image1;
        manipulationCard_2.sprite = image2;
        manipulationCard_1.color = colorVar;
        manipulationCard_2.color = colorVar;

        colorVar = new Color(1f, 1f, 1f, 0f);
        manipulationCard_3.color = colorVar;
    }

    public void UpdateManipulationCards(Sprite image1, Sprite image2, Sprite image3)
    {
        Color colorVar = new Color(1f, 1f, 1f, 1f);
        manipulationCard_1.sprite = image1;
        manipulationCard_2.sprite = image2;
        manipulationCard_3.sprite = image3;
        manipulationCard_1.color = colorVar;
        manipulationCard_2.color = colorVar;
        manipulationCard_3.color = colorVar;
    }

    public void SelectCard(GameObject ob)
    {   
        //var colors = ob.GetComponent<Button>().colors;
        //colors.highlightedColor = new Color(255, 100, 100);
        //colors.highlightedColor = new ColorBlock();
        //ob.GetComponent<Button>().colors = colors;
        ColorBlock colorVar = ob.GetComponent<Button>().colors;
        colorVar.highlightedColor = new Color(0.5f, 0.5f, 0.5f, 1f);
        ob.GetComponent<Button>().colors = colorVar;

    }
}
