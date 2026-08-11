using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image staminaBar;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        float healthPercentage = currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;
    }
    public void UpdateStaminaBar(float maxStamina, float currentStamina) 
    {
        float StaminaPercentage = currentStamina / maxStamina;
        staminaBar.fillAmount = StaminaPercentage;
    } 
}
