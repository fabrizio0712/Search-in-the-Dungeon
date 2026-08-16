using UnityEngine;

public class AddHazard : CardEffect
{
    [SerializeField] private int amount;
    
    public override void ActivateEffect()
    {
        GameManager gm = FindAnyObjectByType<GameManager>();
        gm.HazardUpdate(amount);
    }
}
