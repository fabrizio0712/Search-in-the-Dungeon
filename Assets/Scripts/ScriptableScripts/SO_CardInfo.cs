using UnityEngine;

[CreateAssetMenu(fileName = "SO_Card", menuName = "Scriptable Objects/SO_Card")]
public class SO_Card : ScriptableObject
{
    public int cardID;
    public string cardName;
    public string cardDescription;
    public int cardMaxCopies;
    public CardRarity cardRarity;
    public Sprite cardImage;
    public int cardPrice;
    public int cardDuration;

    public enum CardRarity
    {
        comon,
        rare,
        super,
    }


}
