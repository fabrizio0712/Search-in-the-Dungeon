using System.Collections.Concurrent;
using System.Linq;
using UnityEngine;

public class RiskSensor : MonoBehaviour
{
    // References
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LayerMask playerSoundMask;
    
    // Variables
    [SerializeField] private float detectionRange = 1f;
    [SerializeField] private float detectionInterval = 1f;
    [SerializeField] private float cooldownTime = 0;
    [SerializeField] private bool onCooldown = false;
    
    [SerializeField] private float detectionIntervalCurrentTime = 0;
    [SerializeField] private float cooldownCurrentTime = 0;

    private void Update()
    {
        if (onCooldown) 
        {
            if (cooldownCurrentTime < cooldownTime) 
            {
                cooldownCurrentTime += Time.deltaTime; 
            }
            else
            {
                onCooldown = false;
                cooldownCurrentTime = 0;
            }
        }
        else 
        {
            if (detectionIntervalCurrentTime < detectionInterval)
            {
                detectionIntervalCurrentTime += Time.deltaTime;
            }
            else
            {
                detectionIntervalCurrentTime = 0;
                Collider[] temp = Physics.OverlapSphere(transform.position, detectionRange, playerSoundMask);
                if (temp.Length > 0)
                {
                    gameManager.RiskUpdate(1);
                    onCooldown = true;
                }
            }
        }
    }


}
