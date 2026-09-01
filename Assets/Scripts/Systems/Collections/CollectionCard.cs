using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CollectionCard : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private CardLogic card;
    [SerializeField] private int cardCount;
    [SerializeField] private TextMeshProUGUI count;
    [SerializeField] private GameObject cardSocket;
    [SerializeField] private GameObject greyOut;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private bool canBeDrag;

    private CardCollectionManager cardCollectionManager;
    public int CardCount { get => cardCount;}
    public CardLogic Card { get => card; }

    public void Initializer(GameObject cardReference, CardCollectionManager manager, bool useCardCount) 
    {
        GameObject temp = Instantiate(cardReference,cardSocket.transform);
        card = temp.GetComponent<CardLogic>();
        card.SetUpCard(false);
        cardCollectionManager = manager;
        count.enabled = useCardCount;
    }
    public void SetCardCount(int amount)
    {
        cardCount = amount;
        count.SetText(cardCount.ToString());
        if (cardCount == 0)
        {
            greyOut.SetActive(true);
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            greyOut.SetActive(false);
            canvasGroup.blocksRaycasts = true;
        }
    }
    public void IncreaseCardCount(int amount) 
    {
        SetCardCount(cardCount + amount);
    }
    public void DecreaseCardCount(int amount) 
    {
        SetCardCount(cardCount - amount);
    }
    public string GetCardName() 
    {
        return card.CardInfo.cardName;
    }
    public void AutoDestroy() 
    {
        card.gameObject.transform.SetParent(cardSocket.transform);
        Destroy(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        card.gameObject.transform.SetParent(cardCollectionManager.gameObject.transform);
        card.gameObject.transform.position = eventData.position;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        card.gameObject.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        card.gameObject.transform.SetParent(cardSocket.transform);
        card.gameObject.transform.position = cardSocket.transform.position;
        SetCardCount(cardCount);
    }
}
