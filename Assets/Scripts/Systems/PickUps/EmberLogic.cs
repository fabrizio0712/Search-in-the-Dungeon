using UnityEngine;

public class EmberLogic : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject visuals;
    [SerializeField] private float rotationSpeed = 5;

    private void Update()
    {
        visuals.transform.RotateAroundLocal(Vector3.up, rotationSpeed * Time.deltaTime);
    }
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
