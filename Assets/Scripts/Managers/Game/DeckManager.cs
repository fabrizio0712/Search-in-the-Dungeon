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
    [SerializeField] private CardLogic currentCard;
    [SerializeField] private List<GameObject> deck = new List<GameObject>();

    private void Start()
    {
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
            currentCard = deck[temp].GetComponent<CardLogic>();
            deck.Remove(currentCard.gameObject);
            DeckCountUpdate();
            Instantiate(currentCard.gameObject,cardPosition);
            currentCard.GameManager = gameManager;
            currentCard.SetUpCard();
            currentCard.ActivateCard();
        }
        else Debug.Log("No cards in Deck");
    }
    private void DeckCountUpdate() 
    {
        deckIndicator.text = deck.Count.ToString();
    }

}
