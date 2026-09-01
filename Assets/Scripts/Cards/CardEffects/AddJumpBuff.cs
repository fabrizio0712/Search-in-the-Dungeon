using UnityEngine;

public class AddJumpBuff : CardEffect
{
    [SerializeField] private float amount;
    [SerializeField] private float duration;
    
    public override void ActivateEffect()
    {
        GameManager gm = FindAnyObjectByType<GameManager>();
        gm.Player.ApplyJumpBuff(amount, duration);
    }
}
