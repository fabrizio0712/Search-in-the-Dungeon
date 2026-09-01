using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardLogic : MonoBehaviour
{
    [SerializeField] private SO_Card cardInfo;
    [SerializeField] private GameObject cardVisuals;
    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text cardDescription;
    [SerializeField] private TMP_Text cardMaxCopies;
    [SerializeField] private Image cardImage;
    [SerializeField] private Image cardBackground;
    [SerializeField] private List<CardEffect> cardEffects = new List<CardEffect>();
    private GameManager gameManager;
    private float currentDuration = 0;

    public bool LogicActive;

    public GameManager GameManager { get => gameManager; set => gameManager = value; }
    public SO_Card CardInfo { get => cardInfo; }


    private void Update()
    {
        if (LogicActive)
        {
            if (currentDuration < cardInfo.cardDuration)
            {
                if (currentDuration > 5 && cardVisuals.active) cardVisuals.SetActive(false);
                currentDuration += Time.deltaTime;
            }
            else gameObject.SetActive(false);
        }
    }
    public void SetUpCard(bool activateLogic) 
    {
        LogicActive = activateLogic; 
        cardName.text = cardInfo.cardName;
        cardDescription.text = cardInfo.cardDescription;
        cardMaxCopies.text = cardInfo.cardMaxCopies.ToString();
        cardImage.sprite = cardInfo.cardImage;
        switch (cardInfo.cardRarity) 
        {
            case SO_Card.CardRarity.comon:
                cardBackground.color = Color.grey;
                break;
            case SO_Card.CardRarity.rare:
                cardBackground.color = Color.green;
                break;
            case SO_Card.CardRarity.super:
                cardBackground.color = Color.blue;
                break;
        }
    }
    public void ActivateCard() 
    {
        foreach(CardEffect card in cardEffects) 
        {
            card.GameManager = gameManager;
            card.ActivateEffect();
        }
    }
}
