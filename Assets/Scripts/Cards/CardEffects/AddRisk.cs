using UnityEngine;

public class AddRisk : CardEffect
{
    [SerializeField] private int amount;
    
    public override void ActivateEffect()
    {
        GameManager gm = FindAnyObjectByType<GameManager>();
        gm.RiskUpdate(amount);
    }
}
