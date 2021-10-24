using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bandit : Enemy
{
    public int armorValue;
    [SerializeField] private Animator animator;

    void Start()
    {
        armorValue = 5;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth == 0)
        {
            SetAnimatorTrigger("Death");
            nextMove.spriteRenderer.enabled = false;
            nextMove.Text.enabled = false;
            nextMove.enabled = false;
        }
    }

    public override void MakeAction(Entity player)
    {
        if (CurrentHealth <= 0)
            return;
        if (nextAction == MOVE.ATTACK)
        {
            SetAnimatorTrigger("Attack");
            player.GetComponent<HealthManager>().TakeDamage(damage);
            print("Enemy Attack");
        }
        else if (nextAction == MOVE.ARMOR)
        {
            //healthManager.ArmorUp(armorValue);
            print("Enemy armor up");
        }
    }

    public override int WhichAction()
    {
        print("bandit");
        if ((currentHealth + currentArmor) + armorValue > maxHealth)
        {
            return 0;
        }
        else
        {
            int rand = Random.Range(0, 2);
            print("ACTION: " + rand);
            // Either Attack or Armor up
            return (rand);
        }
    }

    public override void SetAnimatorTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }

}