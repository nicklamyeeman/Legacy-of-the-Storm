using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int maxHealth;
    protected int currentHealth;
    protected int currentArmor;

    // public void TakeDamage(int damage)
    // {
    //     if (currentArmor > 0) {
    //         if (currentArmor - damage < 0) {
    //             currentHealth = (currentHealth + currentArmor) - damage;
    //             currentArmor = 0;
    //         }
    //         else
    //             currentArmor = currentArmor - damage;
    //     } else
    //         currentHealth = currentHealth - damage;
    // }

    public void ArmorUp(int armor)
    {
        currentArmor = currentArmor + armor;
    }

    public int CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }

    public int MaxHealth { get { return maxHealth; } }

    public int CurrentArmor { get { return currentArmor; } set { currentArmor = value; } }

}