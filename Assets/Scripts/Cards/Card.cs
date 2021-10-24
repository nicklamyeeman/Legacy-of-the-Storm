using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    //public struct Effect
    //{
    //    public Type Type;
    //    public int Stat;
    //}

    public enum Type {
        ATTACK,
        ARMOR,
        POISON,
        EVADE
    }

    public enum Tier
    {
        NORMAL,
        RARE,
        EPIC
    }

    public enum NbTarget
    {
        SOLO,
        MULTIPLE,
        ZONE
    }

    public enum Target
    {
        ENEMY,
        ALLY
    }

    public new string name;
    public string description;

    public int cost;
    //public Effect[] effects; 

    public Tier tier;
    public Type type;
    public NbTarget nbTarget;
    public Target target;
    public int stat;
    public bool zone;

    public Sprite artwork;
}

