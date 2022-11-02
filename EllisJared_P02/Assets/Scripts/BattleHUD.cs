using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text creatureName;
    public Text creatureLevel;

    public HealthBar healthBar;

    public void SetCreatureData(Creature creature)
    {
        creatureName.text = creature.Base.CreatureName;
        creatureLevel.text = $"LVL: {creature.Level}";
        healthBar.SetHP(creature.HP/creature.MaxHP);
    }
}
