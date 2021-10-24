using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private Entity entity;
    private UIHealthBarManager UIBarManager;

    void Start()
    {
        entity = GetComponent<Entity>();
        GameObject canvas = transform.GetChild(0).gameObject;
        UIBarManager = canvas.GetComponent<UIHealthBarManager>();
        // print("Entity Health bar");
        // print("CurrentHealth: " + entity.CurrentHealth);
        // print("MaxHealth: " + entity.MaxHealth);
        UIBarManager.SetUpHealthValue(entity.CurrentHealth, entity.MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (entity.CurrentArmor > 0)
        {
            if (entity.CurrentArmor - damage < 0)
            {
                entity.CurrentHealth = (entity.CurrentHealth + entity.CurrentArmor) - damage;
                entity.CurrentArmor = 0;
            }
            else
                entity.CurrentArmor = entity.CurrentArmor - damage;
        }
        else
            entity.CurrentHealth = entity.CurrentHealth - damage;
        if (entity.CurrentHealth <= 0)
        {
            entity.CurrentHealth = 0;
        }
        UIBarManager.UpdateUIHealthBar(entity.CurrentHealth, entity.MaxHealth, entity.CurrentArmor);
        print("Current Health: " + entity.CurrentHealth);
    }

    public void ArmorUp(int armor)
    {
        entity.CurrentArmor = entity.CurrentArmor + armor;
        UIBarManager.UpdateUIShieldBar(entity.CurrentHealth, entity.MaxHealth, entity.CurrentArmor);
    }

}