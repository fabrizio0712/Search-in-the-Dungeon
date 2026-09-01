using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance instance { get; private set; }
    public List<GameObject> CurrentDeck { get => currentDeck; set => currentDeck = value; }
    public List<GameObject> CardList { get => cardList; }
    public Dictionary<int, GameObject> CardsReferences { get => cardsReferences; }
    public Dictionary<int, int> CardsObtained { get => cardsObtained; }

    [SerializeField] private List<GameObject> currentDeck = new List<GameObject>();
    [SerializeField] private List<GameObject> cardList = new List<GameObject>();
    [SerializeField] private Dictionary<int, GameObject> cardsReferences = new Dictionary<int, GameObject>();
    [SerializeField] private Dictionary<int, int> cardsObtained = new Dictionary<int, int>();

    private void Awake()
    {
        if(instance == null ) 
        {
            instance = this;
            foreach(GameObject card in cardList) 
            {
                int temp = card.GetComponent<CardLogic>().CardInfo.cardID;
                cardsReferences.Add(temp, card);
                cardsObtained.Add(temp, 0);
            }
            // logica de prueba para actualizar numero de copias obtenidas sin sistema de guardado
            foreach(GameObject card in currentDeck) 
            {
                int temp = card.GetComponent<CardLogic>().CardInfo.cardID;
                cardsObtained[temp] += 1;
            }
        }
        else if(instance != this) 
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }
    public void ObtainCard(int obtained) 
    {
        if (cardsObtained.ContainsKey(obtained)) 
        {
            cardsObtained[obtained] += 1;
            // Añadido Temporal Para Prueba de Compra
            //currentDeck.Add(cardsReferences[obtained]);
        }
    }
    public void AddCardToDeck(int cardID) 
    {
        currentDeck.Add(cardsReferences[cardID]);
    }
    public void RemoveCardFromDeck(int cardID) 
    {
        currentDeck.Remove(cardsReferences[cardID]);
    }


    public void NewGame() 
    {
        // Armar Default Deck
        // Actualizar Cartas obtenidas segun el Default Deck
    }
    public void SaveData() 
    {
        // Guardar Cartas Desbloqueadas
        // Guardar Deck Actual
    }
    public void LoadData()
    {
        // Cargar Cartas Desbloqueadas
        // Cargar Deck Actual
    }
}
