using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text deckIndicator;

    [Header("Variables")]
    [SerializeField] private float drawTime;
    [SerializeField] private float currentDrawTime;

    [Header("Cards")]
    [SerializeField] private Transform cardPosition;
    [SerializeField] private GameObject currentCard;
    [SerializeField] private List<GameObject> deck = new List<GameObject>();

    [Header("StumbleCard")]
    [SerializeField] private GameObject stumblePrefab;

    
    private void Start()
    {
        // Retirar el if una vez implementado correctamente el flujo de juego al iniciar desde el menu
        if (GameInstance.instance != null)
        {
            deck.Clear();
            foreach(GameObject card in GameInstance.instance.CurrentDeck) 
            {
                deck.Add(card);
            }
        }
        DeckCountUpdate();
    }

    private void Update()
    {
        if(currentDrawTime < drawTime) 
        {
            currentDrawTime += Time.deltaTime;
        }
        else 
        {
            currentDrawTime = 0;
            DrawCard();
        }
    }
    private void DrawCard() 
    {
        if (deck.Count > 0)
        {
            int temp = Random.Range(0, deck.Count);
            currentCard = deck[temp];
            deck.Remove(currentCard.gameObject);
            DeckCountUpdate();
            currentCard = Instantiate(currentCard, cardPosition);
            currentCard.GetComponent<CardLogic>().GameManager = gameManager;
            currentCard.GetComponent<CardLogic>().SetUpCard(true);
            currentCard.GetComponent<CardLogic>().ActivateCard();
        }
        else Debug.Log("No cards in Deck");
    }
    private void DeckCountUpdate() 
    {
        deckIndicator.text = deck.Count.ToString();
    }
    public void AddStumbleToDeck() 
    {
        deck.Add(stumblePrefab);
        DeckCountUpdate();
    }

}
