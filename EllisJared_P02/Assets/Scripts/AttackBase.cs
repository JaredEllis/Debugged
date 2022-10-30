using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Creature/New Attack")]
public class AttackBase : ScriptableObject
{
    [SerializeField] private string name; public string Name => name;
    
    [TextArea] [SerializeField] private string description; public string Description => description;
    
    //[SerializeField] private CreatureType type; public CreatureType Type => type;
    [SerializeField] private int power; public int Power => power;
    [SerializeField] private int accuracy; public int Accuracy => accuracy;
    [SerializeField] private int pp; public int PP => pp;
}
