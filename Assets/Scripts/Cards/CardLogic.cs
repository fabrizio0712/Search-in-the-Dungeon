using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardLogic : MonoBehaviour
{
    [SerializeField] private SO_Card cardInfo;
    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text cardDescription;
    [SerializeField] private TMP_Text cardMaxCopies;
    [SerializeField] private Image cardImage;
    [SerializeField] private List<CardEffect> cardEffects = new List<CardEffect>();
    private GameManager gameManager;
    private float currentDuration = 0;

    public GameManager GameManager { get => gameManager; set => gameManager = value; }

    private void Update()
    {
        if (currentDuration < cardInfo.cardDuration) currentDuration += Time.deltaTime;
        else gameObject.SetActive(false);
    }
    public void SetUpCard() 
    {
        cardName.text = cardInfo.cardName;
        cardDescription.text = cardInfo.cardDescription;
        cardMaxCopies.text = cardInfo.cardMaxCopies.ToString();
        cardImage.sprite = cardInfo.cardImage;
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
