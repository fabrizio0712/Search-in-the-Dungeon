using UnityEngine;

public class AddEmbersDrops : CardEffect
{
    [SerializeField] private int amount;
    public override void ActivateEffect()
    {
        GameManager gm = FindAnyObjectByType<GameManager>();
        gm.SpawnEmbers(amount);
    }
}
