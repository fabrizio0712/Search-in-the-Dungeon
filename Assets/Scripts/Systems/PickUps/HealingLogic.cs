using UnityEngine;

public class HealingLogic : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float healAmount;

    public void SetGameManager(GameManager gm)
    {
        gameManager = gm;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().GetHeal(healAmount);
            //gameManager.AddEmberToPool(gameObject);
            gameObject.SetActive(false);
        }
    }
}
