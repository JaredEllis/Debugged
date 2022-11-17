using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Creature
{
    private CreatureBase _base;
    public CreatureBase Base
    {
        get => _base;
    }

    private int _level;

    public int Level
    {
        get => _level;
        set => _level = value;
    }

    private List<Attack> _attacks;

    public List<Attack> Attacks
    {
        get => _attacks;
        set => _attacks = value;
    }

    private int _hp;

    public int HP
    {
        get => _hp;
        set => _hp = value;
    }

    public Creature(CreatureBase creatureBase, int creatureLevel)
    {
        _base = creatureBase;
        _level = creatureLevel;

        _hp = MaxHP;
        
        _attacks = new List<Attack>();

        foreach (var lAttack in _base.LearnableAttacks)
        {
            if (lAttack.Level <= _level)
            {
                _attacks.Add(new Attack(lAttack.Attack));
            }

            if (_attacks.Count >= 4)
            {
                break;
            }
        }
    }

    public int MaxHP => Mathf.FloorToInt((_base.MaxHP * _level)/20.0f)+10;
    public int Attack => Mathf.FloorToInt((_base.Attack * _level)/100.0f)+1;
    public int Defense => Mathf.FloorToInt((_base.Defense * _level)/100.0f)+1;
    public int SpAttack => Mathf.FloorToInt((_base.SpAttack * _level)/100.0f)+1;
    public int Speed => Mathf.FloorToInt((_base.Speed * _level)/100.0f)+1;

    public DamageDescription ReceiveDamage(Creature attacker, Attack attack)
    {
        /*
         * Damage formula found in the Generation I Pokemon game:
         * 
         * https://bulbapedia.bulbagarden.net/wiki/Damage
         * https://wikimedia.org/api/rest_v1/media/math/render/svg/4445736b8e1e8be597cf7901e4ad0be60b54d1ab
         */
        
        float critical = 1f;
        
        if (Random.Range(0, 100f) < 5f)
        {
            critical = 2f;
        }

        float type = TypeMatrix.GetMultEffectiveness(attack.Base.Type, this.Base.Type);

        var damageDesc = new DamageDescription()
        {
            Critical = critical,
            Type = type,
            Dead = false
        };
        
        float modifiers = Random.Range(0.84f, 1.0f) * type * critical;
        float baseDamage = ((2 * attacker.Level / 5f + 2) * attack.Base.Power * (attacker.Attack/(float)Defense))/50f+2;
        int totalDamage = Mathf.FloorToInt(baseDamage * modifiers);

        HP -= totalDamage;
        if (HP <= 0)
        {
            damageDesc.Dead = true;
        }
        
        return damageDesc;
    }

    public Attack RandomAttack()
    {
        int randId = Random.Range(0, Attacks.Count);
        return Attacks[randId];
    }
}

public class DamageDescription
{
    public float Critical { get; set; }
    public float Type { get; set; }
    public bool Dead { get; set; }
}
