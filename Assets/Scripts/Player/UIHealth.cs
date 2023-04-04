using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Health = default;
    [SerializeField] private TextMeshProUGUI Stamina = default;
    [SerializeField]private Image HealthBar;
    [SerializeField] private Image StaminaBar;

    private void OnEnable()
    {
        FPSController.OnDamage += UpdateHealth;
        FPSController.OnHeal += UpdateHealth;
        FPSController.OnStaminaChange += UpdateStamina;

    }
    private void OnDisable()
    {
        FPSController.OnDamage -= UpdateHealth;
        FPSController.OnHeal -= UpdateHealth;
        FPSController.OnStaminaChange -= UpdateStamina;

    }

    private void Start()
    {
        UpdateHealth(100);
        UpdateStamina(100);
    }
    private void UpdateHealth(float currentHealth)
    {
        Health.text = currentHealth.ToString("00");
        HealthBar.fillAmount = currentHealth / 100;
    }
    private void UpdateStamina(float currentStamina)
    {
        Stamina.text = currentStamina.ToString("00");
        StaminaBar.fillAmount = currentStamina / 100;

    }
}
