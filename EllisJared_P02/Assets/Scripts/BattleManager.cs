using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHUD playerHUD;

    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHUD enemyHUD;

    private void Start()
    {
        SetupBattle();
    }

    private void SetupBattle()
    {
        playerUnit.SetupCreature();
        playerHUD.SetCreatureData(playerUnit.Creature);
        
        //enemyUnit.SetupCreature();
        //enemyHUD.SetCreatureData(enemyUnit.Creature);
    }
}
