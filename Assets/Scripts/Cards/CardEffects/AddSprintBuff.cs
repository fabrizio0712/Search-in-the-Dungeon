using UnityEngine;

public class AddSprintBuff : CardEffect
{
    [SerializeField] private float amount;
    [SerializeField] private float duration;
    
    public override void ActivateEffect()
    {
        GameManager gm = FindAnyObjectByType<GameManager>();
        gm.Player.ApplySpeedBuff(amount, duration);
    }
}
