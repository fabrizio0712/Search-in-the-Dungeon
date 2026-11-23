using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        float healthPercentage = currentHealth / maxHealth;
        healthBar.value = healthPercentage;
    }
}
