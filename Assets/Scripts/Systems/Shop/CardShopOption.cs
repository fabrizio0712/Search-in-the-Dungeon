using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardShopOption : MonoBehaviour
{
    [SerializeField] private RewardManager rewardManager;
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI cardValue;
    [SerializeField] private GameObject greyOut;
    [SerializeField] private TextMeshProUGUI greyOutText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip spawnCardSound;
    [SerializeField] private List<AudioClip> buyCardSounds;
    
    private GameObject cardGameObject;
    private CardLogic card;

    public void SetUpCardOption(GameObject GivedCard) 
    {
        audioSource.PlayOneShot(spawnCardSound);
        cardGameObject = GivedCard;
        card = cardGameObject.GetComponent<CardLogic>();
        card.SetUpCard(false);
        cardValue.SetText(card.CardInfo.cardPrice.ToString());
        CheckEnoughEmbers();
        if (GameInstance.instance.CardsObtained[card.CardInfo.cardID] >= card.CardInfo.cardMaxCopies) 
        {
            greyOut.SetActive(true);
            greyOutText.text = "Maxed Copies";
            buyButton.interactable = false;
        }
    }
    public void CheckEnoughEmbers() 
    {
        if(rewardManager.CurrentEmbers < card.CardInfo.cardPrice) 
        {
            buyButton.interactable = false;
        }
    }
    public void BuyCard() 
    {
        rewardManager.UpdateCurrentEmbers(card.CardInfo.cardPrice);
        if (GameInstance.instance != null)
        {
            GameInstance.instance.ObtainCard(card.CardInfo.cardID);
        }
        buyButton.interactable = false;
        greyOut.SetActive(true);
        greyOutText.text = "Sold";
        audioSource.PlayOneShot(buyCardSounds[Random.Range(0, buyCardSounds.Count)]);
    }
}
