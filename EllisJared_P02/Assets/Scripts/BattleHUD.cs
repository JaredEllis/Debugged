using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text creatureName;
    public Text creatureLevel;

    public HealthBar healthBar;
    public Text creatureHealth;

    private Creature _creature;

    public void SetCreatureData(Creature creature)
    {
        _creature = creature;
        creatureName.text = creature.Base.CreatureName;
        creatureLevel.text = $"LVL: {creature.Level}";
        UpdateCreatureData(creature.HP);    
    }

    public void UpdateCreatureData(int oldHPValue)
    {
        StartCoroutine(healthBar.SetSmoothHP((float)_creature.HP/_creature.MaxHP));
        StartCoroutine(DecreaseHealthPoints(oldHPValue));
    }

    IEnumerator DecreaseHealthPoints(int oldHpValue)
    {
        while (oldHpValue > _creature.HP)
        {
            oldHpValue--;
            creatureHealth.text = $"{oldHpValue}/{_creature.MaxHP}";
            yield return new WaitForSeconds(0.04f);
        }

        creatureHealth.text = $"{_creature.HP}/{_creature.MaxHP}";
    }
}
