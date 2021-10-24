using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    private int enrageCD;
    private bool isEnrageAvailable;
    private bool isEnraged;
    private int enrageDuration;
    [SerializeField] private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        enrageCD = 0;
        isEnrageAvailable = true;
        isEnraged = false;
        enrageDuration = 0;
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
        if (nextAction == MOVE.ATTACK) {
            SetAnimatorTrigger("Attack");
            player.GetComponent<HealthManager>().TakeDamage(damage);
            print("Enemy Attack");
        }
    }

    public override int WhichAction()
    {
        print("Skeleton");
        print("Enrage: " + isEnrageAvailable);
        if (enrageDuration >= 1) {
            isEnraged = false;
            damage -= 5;
            enrageDuration = 0;
        }
        if (enrageCD == 0)
            isEnrageAvailable = true;
        if (isEnrageAvailable == true) {
            isEnraged = true;
            damage += 5;
            isEnrageAvailable = false;
            enrageCD = 2;
            return 2;
        } else {
            if (isEnraged)
                enrageDuration += 1;
            enrageCD -= 1;
            return 0;
        }
    }

    public override void SetAnimatorTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
        Debug.Log("Skelet animation");
    }

}
