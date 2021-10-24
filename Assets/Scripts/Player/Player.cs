using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public int action;

    void Start()
    {
        currentHealth = maxHealth;
        action = 3;
    }
}
