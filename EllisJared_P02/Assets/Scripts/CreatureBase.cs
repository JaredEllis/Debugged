using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Creature", menuName = "Creature/New Creature")]
public class CreatureBase : ScriptableObject
{
    [SerializeField] private int ID;
    [SerializeField] private string creatureName; public string CreatureName => creatureName;
    [TextArea][SerializeField] private string description; public string Description => description;
    [SerializeField] private Sprite frontSprite; public Sprite FrontSprite => frontSprite;
    [SerializeField] private Sprite backSprite; public Sprite BackSprite => backSprite;
    [SerializeField] private CreatureType type; public CreatureType Type => type;

    // Stats

    [SerializeField] private int maxHP; public int MaxHP => maxHP;
    [SerializeField] private int attack; public int Attack => attack;
    [SerializeField] private int defense; public int Defense => defense;
    [SerializeField] private int spAttack; public int SpAttack => spAttack;
    [SerializeField] private int spDefense; public int SpDefense => spDefense;
    [SerializeField] private int speed; public int Speed => speed;
    
    [SerializeField] private List<LearnableAttack> learnableAttacks;
    public List<LearnableAttack> LearnableAttacks => learnableAttacks;
}

    public enum CreatureType
    {
        Normal,
        Fire,
        Shock,
        Explosive,
        Corrosive,
    }

    public class TypeMatrix
    {
        private static float[][] matrix =
        {
            //                     NOR   FRE   SHK   EXP   COR
            /*NOR*/ new float[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
            /*FRE*/ new float[] { 1.5f, 0.5f, 1.0f, 1.5f, 1.0f },
            /*SHK*/ new float[] { 1.5f, 1.5f, 0.5f, 1.0f, 1.0f },
            /*EXP*/ new float[] { 2.0f, 1.5f, 0.5f, 0.5f, 1.0f },
            /*COR*/ new float[] { 1.5f, 1.0f, 1.0f, 1.0f, 0.5f },
        };

        public static float GetMultEffectiveness(CreatureType attackType, CreatureType defenderType)
        {
            int row = (int)attackType;
            int col = (int)defenderType;

            return matrix[row][col];
        }
    }

    [Serializable]
    public class LearnableAttack
    {
        [SerializeField] private AttackBase attack;
        [SerializeField] private int level;

        public AttackBase Attack => attack;
        public int Level => level;
    }