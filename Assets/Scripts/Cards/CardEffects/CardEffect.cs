using UnityEngine;

public abstract class CardEffect : MonoBehaviour
{
    private GameManager gameManager;

    public GameManager GameManager { get => gameManager; set => gameManager = value; }

    public abstract void ActivateEffect();
}
