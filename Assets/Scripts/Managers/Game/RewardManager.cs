using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<Transform> cardsTranformParents = new List<Transform>();
    
    [Header("Cards Lists")]
    [SerializeField] private List<GameObject> commonCards = new List<GameObject>();
    [SerializeField] private List<GameObject> rareCards = new List<GameObject>();
    [SerializeField] private List<GameObject> superCards = new List<GameObject>();

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI embersText;
    [SerializeField] private List<CardShopOption> cardShopOptions = new List<CardShopOption>();


    // Other Variables
    private int currentEmbers = 0;

    public int CurrentEmbers { get => currentEmbers; }

    private void OnEnable()
    {
        SetUpShop();   
    }
    private void SetUpShop() 
    {
        currentEmbers = gameManager.CurrentActiveArtifact.ArtifactInfo.Value;
        UIEmbersUpdate();
        cardShopOptions[0].SetUpCardOption(Instantiate(GetRandomCard(commonCards), cardsTranformParents[0]));
        cardShopOptions[1].SetUpCardOption(Instantiate(GetRandomCard(commonCards), cardsTranformParents[1]));
        cardShopOptions[2].SetUpCardOption(Instantiate(GetRandomCard(rareCards), cardsTranformParents[2]));
        cardShopOptions[3].SetUpCardOption(Instantiate(GetRandomCard(rareCards), cardsTranformParents[3]));
        cardShopOptions[4].SetUpCardOption(Instantiate(GetRandomCard(superCards), cardsTranformParents[4]));
    }
    private GameObject GetRandomCard( List<GameObject> cardList) 
    {
        int temp = Random.Range(0, cardList.Count);
        return cardList[temp];
    }
    public void UpdateCurrentEmbers(int amount) 
    {
        currentEmbers -= amount;
        UIEmbersUpdate();
        foreach(CardShopOption CSO in cardShopOptions) 
        {
            CSO.CheckEnoughEmbers();
        }
    }
    private void UIEmbersUpdate() 
    {
        embersText.SetText("Embers: " + currentEmbers.ToString());
    }
}
