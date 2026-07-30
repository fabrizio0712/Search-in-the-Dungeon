using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlay() 
    {
        SceneManager.LoadScene(1);
    }
    public void OnOptions() 
    {

    }
    public void OnExit() 
    {
        Application.Quit();
    }
}
