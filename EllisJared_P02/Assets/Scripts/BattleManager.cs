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

    [SerializeField] BattleDialogBox battleDialogBox;

    private void Start()
    {
        StartCoroutine(SetupBattle());
    }

    private IEnumerator SetupBattle()
    {
        playerUnit.SetupCreature();
        playerHUD.SetCreatureData(playerUnit.Creature);
        
        battleDialogBox.SetCreatureAttacks(playerUnit.Creature.Attacks);

        enemyUnit.SetupCreature();
        enemyHUD.SetCreatureData(enemyUnit.Creature);
        
        yield return (battleDialogBox.SetDialog($"An {enemyUnit.Creature.Base.name} has appeared."));
        PlayerAction();
    }

    public void PlayerAction()
    {
        StartCoroutine(PlayerActionCoroutine());
    }

    IEnumerator PlayerActionCoroutine()
    {
        StartCoroutine(battleDialogBox.SetDialog("Select an action."));
        yield return new WaitForSeconds(0.5f);
        battleDialogBox.ToggleActions(true);
    }

    public void PlayerAttack()
    {
        StartCoroutine(PlayerAttackCoroutine());
    }

    IEnumerator PlayerAttackCoroutine()
    {
        battleDialogBox.ToggleDialogText(false);
        battleDialogBox.ToggleActions(false);
        yield return new WaitForSeconds(0.2f);
        battleDialogBox.ToggleAttacks(true);
    }
}
