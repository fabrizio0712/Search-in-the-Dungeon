using UnityEngine;

public class EmberLogic : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public void SetGameManager(GameManager gm) 
    {
        gameManager = gm;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            gameManager.AddEmberToPool(gameObject);
            gameObject.SetActive(false);
        }
    }
}
