using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class GameInstance : MonoBehaviour, IDataPersistence
{
    public static GameInstance instance { get; private set; }
    public List<GameObject> CurrentDeck { get => currentDeck; set => currentDeck = value; }
    public List<GameObject> CardList { get => cardList; }
    public Dictionary<int, GameObject> CardsReferences { get => cardsReferences; }
    public Dictionary<int, int> CardsObtained { get => cardsObtained; }
    public int MaxCardsInDeck { get => maxCardsInDeck; }

    [SerializeField] private List<GameObject> currentDeck = new List<GameObject>();
    [SerializeField] private List<GameObject> cardList = new List<GameObject>();
    [SerializeField] private Dictionary<int, GameObject> cardsReferences = new Dictionary<int, GameObject>();
    [SerializeField] private Dictionary<int, int> cardsObtained = new Dictionary<int, int>();
    [SerializeField] private int maxCardsInDeck;
    [SerializeField] private AudioMixer audioMixer;

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
            // -----------------------------------------------------------------------------------
            // logica de prueba para actualizar numero de copias obtenidas sin sistema de guardado
            foreach(GameObject card in currentDeck) 
            {
                int temp = card.GetComponent<CardLogic>().CardInfo.cardID;
                cardsObtained[temp] += 1;
            }
            // -----------------------------------------------------------------------------------
        }
        else if(instance != this) 
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        CheckPlayerPrefs();
    }
    public void ObtainCard(int obtained) 
    {
        if (cardsObtained.ContainsKey(obtained)) 
        {
            cardsObtained[obtained] += 1;
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
    private void CheckPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("MasterVolume")) 
        {
            audioMixer.SetFloat("Master",PlayerPrefs.GetFloat("MasterVolume"));
        }
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            audioMixer.SetFloat("Music", PlayerPrefs.GetFloat("MusicVolume"));
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            audioMixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFXVolume"));
        }
    }

    public void LoadData(GameData gameData)
    {
        if (gameData.currentDeck.Count > 0)
        {
            currentDeck.Clear();
            foreach (int id in gameData.currentDeck)
            {
                currentDeck.Add(cardsReferences[id]);
            }
            cardsObtained.Clear();
            foreach (var dic in gameData.cardsCount)
            {
                cardsObtained.Add(dic.Key, dic.Value);
            }
        }
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.currentDeck.Clear();
        foreach(GameObject go in currentDeck) 
        {
            gameData.currentDeck.Add(go.GetComponent<CardLogic>().CardInfo.cardID);
        }
        gameData.cardsCount.Clear();
        foreach(var dic in cardsObtained) 
        {
            gameData.cardsCount.Add(dic.Key, dic.Value);
        }
    }
}
