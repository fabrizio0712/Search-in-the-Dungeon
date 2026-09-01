using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardShopOption : MonoBehaviour
{
    [SerializeField] private RewardManager rewardManager;
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI cardValue;
    
    private GameObject cardGameObject;
    private CardLogic card;

    public void SetUpCardOption(GameObject GivedCard) 
    {
        cardGameObject = GivedCard;
        card = cardGameObject.GetComponent<CardLogic>();
        cardValue.SetText(card.CardInfo.cardPrice.ToString());
        CheckEnoughEmbers();
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
    }
}
