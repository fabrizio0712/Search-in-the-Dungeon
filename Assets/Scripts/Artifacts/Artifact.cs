using UnityEngine;

public class Artifact : MonoBehaviour
{
    [SerializeField] private SO_Artifact artifactInfo;
    [SerializeField] private GameManager gameManager;
    private bool canInteract = false;

    public GameManager GameManager { get => gameManager; set => gameManager = value; }
    public SO_Artifact ArtifactInfo { get => artifactInfo; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract) 
        {
            if(Input.GetButtonDown("Interact")) DeactivateArtifact();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            gameManager.UIShowInteract(true);
            canInteract = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.UIShowInteract(false);
            canInteract = false;
        }
    }
    private void DeactivateArtifact() 
    {
        canInteract = false;
        gameManager.UIShowInteract(false);
        gameManager.PickUpArtifact();
        gameObject.SetActive(false);
    }
}
