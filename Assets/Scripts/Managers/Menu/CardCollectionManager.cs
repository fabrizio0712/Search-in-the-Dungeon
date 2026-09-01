using System.Collections.Generic;
using UnityEngine;

public class CardCollectionManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject cardCollectionReference;

    [Header("DeckReferences")]
    [SerializeField] private GameObject deckZone;
    [SerializeField] private List<GameObject> deckCards = new List<GameObject>();

    [Header("CollectionReferences")]
    [SerializeField] private GameObject cardCollectionZone;
    [SerializeField] private List<CollectionCard> cardCollection = new List<CollectionCard >();

    private void Start()
    {
        InitializeDeck();
        InitializeCollections();
    }
    private void InitializeCollections()
    {
        foreach (GameObject go in GameInstance.instance.CardList)
        {
            int cardCount = GameInstance.instance.CardsObtained[go.GetComponent<CardLogic>().CardInfo.cardID];
            if (cardCount >= 0)
            {
                GameObject temp = Instantiate(cardCollectionReference, cardCollectionZone.transform);
                cardCollection.Add(temp.GetComponent<CollectionCard>());
                temp.GetComponent<CollectionCard>().Initializer(go,this,true);
                int copiesInDeck = 0;
                foreach(GameObject cardInDeck in deckCards) 
                {
                    if(temp.GetComponent<CollectionCard>().Card.CardInfo.cardID == cardInDeck.GetComponent<CollectionCard>().Card.CardInfo.cardID) 
                    {
                        copiesInDeck++;
                    }
                }
                temp.GetComponent<CollectionCard>().SetCardCount(cardCount - copiesInDeck);
            }
        }
    }
    private void InitializeDeck() 
    {
        for(int i = 0; i < GameInstance.instance.CurrentDeck.Count; i++) 
        {
            GameObject temp = Instantiate(cardCollectionReference, deckZone.transform);
            temp.GetComponent<CollectionCard>().Initializer(GameInstance.instance.CurrentDeck[i], this, false);
            temp.GetComponent<CollectionCard>().SetCardCount(1);
            deckCards.Add(temp);
        }
        SortDeck();
    }
    public void SortDeck()
    {
        for(int i = 0; i < deckCards.Count - 1; i++) 
        {
            for(int j = 0; j < deckCards.Count - 1; j++) 
            {
                if (deckCards[j].GetComponent<CollectionCard>().Card.CardInfo.cardID > deckCards[j + 1].GetComponent<CollectionCard>().Card.CardInfo.cardID) 
                {
                    GameObject auxObject = deckCards[j];
                    deckCards[j] = deckCards[j + 1];
                    deckCards[j + 1] = auxObject;
                    deckCards[j].transform.SetSiblingIndex(j);
                }
            }
        }
    }
    public void SendCardToCollection(CollectionCard sendedCard) 
    {
        if (!cardCollection.Contains(sendedCard))
        {
            foreach (CollectionCard cardInCollection in cardCollection)
            {
                if (cardInCollection.Card.CardInfo.cardID == sendedCard.Card.CardInfo.cardID)
                {
                    cardInCollection.IncreaseCardCount(1);
                }
            }
            deckCards.Remove(sendedCard.gameObject);
            GameInstance.instance.RemoveCardFromDeck(sendedCard.Card.CardInfo.cardID);
            sendedCard.AutoDestroy();
            SortDeck();
        }
    }
    public void SendCardToDeck(CollectionCard sendedCard) 
    {
        if (!deckCards.Contains(sendedCard.gameObject))
        {
            if (CheckCopiesInDeck(sendedCard.Card.CardInfo))
            {
                foreach (CollectionCard cardInCollection in cardCollection)
                {
                    if (cardInCollection.Card.CardInfo.cardID == sendedCard.Card.CardInfo.cardID)
                    {
                        cardInCollection.DecreaseCardCount(1);
                    }
                }
                GameObject temp = Instantiate(cardCollectionReference, deckZone.transform);
                temp.GetComponent<CollectionCard>().Initializer(GameInstance.instance.CardsReferences[sendedCard.Card.CardInfo.cardID], this, false);
                temp.GetComponent<CollectionCard>().SetCardCount(1);
                deckCards.Add(temp);
                GameInstance.instance.AddCardToDeck(sendedCard.Card.CardInfo.cardID);
                SortDeck();
            }
        }
    }
    private bool CheckCopiesInDeck(SO_Card cardInfo) 
    {
        int copies = 0;
        foreach (GameObject cardInDeck in deckCards ) 
        {
            if (cardInDeck.GetComponent<CollectionCard>().Card.CardInfo.cardID == cardInfo.cardID) copies++;
        }
        if (copies < cardInfo.cardMaxCopies) return true;
        else return false;
    }
}
