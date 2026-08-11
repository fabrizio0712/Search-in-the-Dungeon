using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private Collider blockCollider;
    [SerializeField] private GameObject hazardBody;
    [SerializeField] private List<GameObject> otherObjects = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blockCollider = GetComponent<Collider>();
    }
    public void TryToBlock() 
    {
        if (otherObjects.Count > 0) StartCoroutine(TryAgain());
        else
        {
            blockCollider.enabled = false;
            hazardBody.SetActive(true);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!otherObjects.Contains(other.gameObject)) otherObjects.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if(otherObjects.Contains(other.gameObject)) otherObjects.Remove(other.gameObject);
    }
    IEnumerator TryAgain() 
    {
        yield return new WaitForSeconds(0.1f);
        TryToBlock();
    }
}
