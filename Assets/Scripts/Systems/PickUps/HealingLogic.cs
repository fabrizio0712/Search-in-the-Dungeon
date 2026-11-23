using UnityEngine;

public class HealingLogic : MonoBehaviour
{
    [SerializeField] private GameObject visuals;
    [SerializeField] private BushLogic parentBush;
    [SerializeField] private float healAmount;
    [SerializeField] private float rotationSpeed = 1;
    private void Update()
    {
        visuals.transform.RotateAroundLocal(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    public void SetParentBush(BushLogic parent) 
    {
        parentBush = parent;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().GetHeal(healAmount);
            parentBush.AddHealObjectToPool(gameObject);
            gameObject.SetActive(false);
        }
    }
}
