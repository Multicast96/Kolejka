using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    GameObject[] pawns = new GameObject[5];
    List<GameObject> pawnsInHand = new List<GameObject>();
    public Color pawnColor;

    public Player(Color color)
    {
        this.pawnColor = color;
    }

}
