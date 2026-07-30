using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        float healthPorcentage = currentHealth / maxHealth;
        healthBar.fillAmount = healthPorcentage;
    }
}
