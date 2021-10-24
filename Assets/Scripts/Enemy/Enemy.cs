using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : Entity
{
    public int damage;
    public NextMove nextMove;

    protected MOVE nextAction;

    public abstract void MakeAction(Entity player);
    public abstract int WhichAction();
    public abstract void SetAnimatorTrigger(string trigger);

    public enum MOVE
    {
        ATTACK,
        ARMOR,
        ENRAGE
    }

    public void PrepareTurn()
    {
        int enemyAction = WhichAction();
        SaveAction(enemyAction);
        nextMove.UpdateUI(enemyAction, damage);
    }

    private void SaveAction(int enemyAction)
    {
        if (enemyAction == 0)
        {
            nextAction = MOVE.ATTACK;
        }
        else if (enemyAction == 1)
        {
            nextAction = MOVE.ARMOR;
        }
        else if (enemyAction == 2)
        {
            nextAction = MOVE.ENRAGE;
        }
    }
}