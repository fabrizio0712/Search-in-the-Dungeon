using UnityEngine;

public class SlowZone : MonoBehaviour
{
    [SerializeField] private float slowDebuffMultiplier;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger Enter");
        if(other.gameObject.tag == "Player") 
        {
            other.GetComponent<PlayerController>().DebuffSpeed = slowDebuffMultiplier;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("trigger Exit");
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().DebuffSpeed = 1;
        }
    }
}
