using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulationCard : MonoBehaviour {

    private Sprite image;
    public enum ManipulationCardName { PanTuNieStal, MatkaZDzieckiem, PomylkaWDostawie, SzczesliwyTraf, ListaSpoleczna, KrytykaWladzy, TowarSpodLady, KolegaWKomitecie, Remanent, ZwiekszonaDostawa};
    private ManipulationCardName cardName;

    public ManipulationCard(Sprite image, int numberOfCard)//ManipulationCardName cardName) 
    {
        this.image = image;
        this.cardName = (ManipulationCardName)numberOfCard;
    }
   
    // Nazwa Karty (0-9)
    public ManipulationCardName getCardName() 
    {
        return cardName;
    }

    // Image
    public Sprite getImage() 
    {
        return image;
    }

    // Zagranie danej karty
    public void PlayCard() 
    {
        if(cardName == ManipulationCardName.PanTuNieStal)
        {

        }
        else if (cardName == ManipulationCardName.PanTuNieStal)
        {

        }
        else if (cardName == ManipulationCardName.PomylkaWDostawie)
        {

        }
        else if (cardName == ManipulationCardName.SzczesliwyTraf)
        {

        }
        else if (cardName == ManipulationCardName.ListaSpoleczna)
        {

        }
        else if (cardName == ManipulationCardName.KrytykaWladzy)
        {

        }
        else if (cardName == ManipulationCardName.TowarSpodLady)
        {

        }
        else if (cardName == ManipulationCardName.KolegaWKomitecie)
        {

        }
        else if (cardName == ManipulationCardName.Remanent)
        {

        }
        else if (cardName == ManipulationCardName.ZwiekszonaDostawa)
        {

        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
