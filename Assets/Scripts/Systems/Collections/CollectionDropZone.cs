using UnityEngine;
using UnityEngine.EventSystems;

public class CollectionDropZone : MonoBehaviour, IDropHandler
{
    [SerializeField] private CardCollectionManager collectionManager;
    public void OnDrop(PointerEventData eventData)
    {
        CollectionCard temp = eventData.pointerDrag.GetComponent<CollectionCard>();
        if (temp != null) 
        {
            collectionManager.SendCardToCollection(temp);
        }
    }
}
