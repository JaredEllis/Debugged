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

    public Attack RandomAttack()
    {
        int randId = Random.Range(0, Attacks.Count);
        return Attacks[randId];
    }
}
