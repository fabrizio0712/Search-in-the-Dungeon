using UnityEngine;

public class BaseCard : MonoBehaviour
{
    [SerializeField] string cardName;
    [SerializeField] int cardPrice;
    [SerializeField] CardRarity cardRarity;

    public enum CardRarity 
    {
        comon,
        rare,
        super,
    }

    public void CardEffect() 
    {

    }
    
}
