using UnityEngine;

public class AddHazardProtection : CardEffect
{
    [SerializeField] private int amount;
    
    public override void ActivateEffect()
    {
        GameManager gm = FindAnyObjectByType<GameManager>();
        gm.AddHazardBlock(amount);
    }
}
