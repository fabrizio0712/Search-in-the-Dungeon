using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menu References")]
    [SerializeField] private GameObject firstMenu;
    [SerializeField] private GameObject instanceMenu;
    [SerializeField] private GameObject deckBuilderMenu;
    [SerializeField] private GameObject optionsMenu;

    [Header("Audios")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip newRunClip;
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip exitClip;

    public enum ECurrentMenu {first,instance }
    public static ECurrentMenu currentMenu = ECurrentMenu.first;
     
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        switch (currentMenu) 
        {
            case ECurrentMenu.first:
                firstMenu.SetActive(true);
                instanceMenu.SetActive(false);
                deckBuilderMenu.SetActive(false);
                optionsMenu.SetActive(false);
                break;
            case ECurrentMenu.instance:
                firstMenu.SetActive(false);
                instanceMenu.SetActive(true);
                deckBuilderMenu.SetActive(false);
                optionsMenu.SetActive(false);
                break;
        }
    }

    // ------------ First Menu ---------------
    public void Play() 
    {
        firstMenu.SetActive(false);
        instanceMenu.SetActive(true);
        currentMenu = ECurrentMenu.instance;
    }
    public void Exit() 
    {
        Application.Quit();
    }
    // ---------------------------------------

    // ----------- Instance Menu -------------
    public void NewRun() 
    {
        StartCoroutine(NewRunCoroutine());
    }
    public void InstanceMenuToFirstMenu()
    {
        instanceMenu.SetActive(false);
        firstMenu.SetActive(true);
        currentMenu = ECurrentMenu.first;
    }
    // ---------------------------------------

    IEnumerator NewRunCoroutine() 
    {
        audioSource.PlayOneShot(newRunClip);
        instanceMenu.SetActive(false);
        yield return new WaitForSeconds(newRunClip.length);
        SceneManager.LoadScene(1);
    }
}
