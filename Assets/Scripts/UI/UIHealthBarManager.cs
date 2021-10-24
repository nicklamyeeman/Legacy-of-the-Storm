using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHealthBarManager : MonoBehaviour
{
    public SpriteRenderer armor;
    public Outline backgroundOutline;
    public Image healthBar;
    public Image shieldBar;
    public TextMeshProUGUI armorValue;
    public TextMeshProUGUI currentHealthValue;
    public TextMeshProUGUI maxHealthValue;

    void Start()
    {
        SetArmorDown();
    }

    public void SetArmorUp(int currentArmor)
    {
        backgroundOutline.effectColor = new Color(0, 255, 248, 128);
        SetArmorIconVisible(true);
        UpdateUIShieldValue(currentArmor);
    }

    public void SetArmorDown()
    {
        backgroundOutline.effectColor = new Color(0, 0, 0);
        SetArmorIconVisible(false);
    }

    public void SetArmorIconVisible(bool value)
    {
        armorValue.enabled = value;
        armor.enabled = value;
    }

    public void UpdateUIHealthBar(int currentHealth, int maxHealth, int currentArmor)
    {
        if (currentArmor == 0)
        {
            shieldBar.fillAmount = (float)currentHealth / (float)maxHealth;
        }
        else
        {
            shieldBar.fillAmount = ((float)currentHealth / (float)maxHealth) + (float)currentArmor / (float)maxHealth;
        }
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
        currentHealthValue.text = currentHealth.ToString();
        maxHealthValue.text = maxHealth.ToString();
        if (currentArmor > 0)
            SetArmorUp(currentArmor);
        else
            SetArmorDown();
    }

    public void UpdateUIShieldBar(int currentHealth, int maxHealth, int currentArmor)
    {
        print("ARMORING: " + (((float)currentHealth + (float)currentArmor) / (float)maxHealth));
        shieldBar.fillAmount = (((float)currentHealth + (float)currentArmor) / (float)maxHealth);
        if (currentArmor > 0)
            SetArmorUp(currentArmor);
        else
            SetArmorDown();
    }

    public void UpdateUIShieldValue(int value)
    {
        armorValue.text = value.ToString();
    }

    public void SetUpHealthValue(int currentHealth, int maxHealth)
    {
        currentHealthValue.text = currentHealth.ToString();
        maxHealthValue.text = maxHealth.ToString();
    }
}