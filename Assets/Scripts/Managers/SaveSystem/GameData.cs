using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class GameData
{
    public List<int> currentDeck;
    public SerializedDictionary<int, int> cardsCount;
    public int prueba;

    public GameData() 
    {
        currentDeck = new List<int>();
        cardsCount = new SerializedDictionary<int, int>();
    }
}
