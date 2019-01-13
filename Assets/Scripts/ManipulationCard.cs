using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulationCard : MonoBehaviour {

    private Sprite image;
    public enum ManipulationCardName { KolegaWKomitecie, KrytykaWladzy, ListaSpoleczna, MatkaZDzieckiem, PanTuNieStal, PomylkaWDostawie, Remanent, SzczesliwyTraf, TowarSpodLady, ZwiekszonaDostawa};
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
        if (cardName == ManipulationCardName.KolegaWKomitecie)
        {
            
        }
        else
        {
            //wyświetlenie na 5 sekund 2 następnych kart dostaw
           // GameManager.EndOfTurn();
        }

    }
    
        // Use this for initialization
        void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
