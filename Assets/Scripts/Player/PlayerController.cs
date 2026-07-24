using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController charController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
