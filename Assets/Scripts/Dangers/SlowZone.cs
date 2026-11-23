using UnityEngine;

public class SlowZone : MonoBehaviour
{
    [SerializeField] private float slowDebuffMultiplier;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        {
            other.GetComponent<PlayerController>().DebuffSpeed = slowDebuffMultiplier;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().DebuffSpeed = 1;
        }
    }
}
