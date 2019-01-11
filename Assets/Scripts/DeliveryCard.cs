using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCard : MonoBehaviour {
    public GameManager.Shop shop { get; private set; }
    public int productsToBeDelivered { get; private set; }

    public void ShowDelivery()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
    }

    // Use this for initialization
    void Start () {
        productsToBeDelivered = Mathf.RoundToInt(Random.Range(1, 4));
        shop = (GameManager.Shop)Mathf.RoundToInt(Random.Range(0, System.Enum.GetNames(typeof(GameManager.Shop)).Length - 1));
        var textMesh = transform.GetChild(0).GetComponent<TextMesh>();
        textMesh.text = productsToBeDelivered.ToString();

        switch (shop)
        {
            case GameManager.Shop.Furniture:
                textMesh.color = new Color(0.6320754f, 0.1878337f, 0.4932569f);
                break;
            case GameManager.Shop.Clothing:
                textMesh.color = new Color(1, 0.08018869f, 0.08018869f);
                break;
            case GameManager.Shop.Newsstand:
                textMesh.color = new Color(0.3011303f, 0.9368078f, 0.9528302f);
                break;
            case GameManager.Shop.Electronic:
                textMesh.color = new Color(0.0648427f, 255, 0);
                break;
            case GameManager.Shop.Grocery:
                textMesh.color = new Color(255, 0.4678748f, 0);
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
