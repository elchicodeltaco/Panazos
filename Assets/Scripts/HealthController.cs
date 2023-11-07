using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthController : MonoBehaviour
{
    [Header("Health Parameters")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    [SerializeField] private float smoothDecreaseDuration = 0.5f;

    [Header ("UI Parameters")]
    [SerializeField] private TMP_Text healthText;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamge(float damage)
    {
        StartCoroutine(SmoothDecreaseHealth(damage));
    }

    private IEnumerator SmoothDecreaseHealth(float damage)
    {
        float damagePertick = damage / smoothDecreaseDuration;
        float elapsedTime = 0f;

        while(elapsedTime < smoothDecreaseDuration)
        {
            float currentDamage = damagePertick * Time.deltaTime;
            currentHealth -= currentDamage;
            elapsedTime += Time.deltaTime;

            updateHealthText();

            if (currentHealth <= 0)
            {
                currentHealth= 0;

                break;
            }
            yield return null;
        }
    }
    void updateHealthText()
    {
        healthText.text = currentHealth.ToString("0");
    }
}
