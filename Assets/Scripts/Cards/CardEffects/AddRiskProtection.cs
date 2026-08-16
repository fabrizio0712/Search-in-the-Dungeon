using UnityEngine;

public class AddRiskProtection : CardEffect
{
    [SerializeField] private int amount;
    
    public override void ActivateEffect()
    {
        GameManager.AddRiskBlock(amount);
    }
}
