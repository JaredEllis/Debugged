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

    public Button primaryAttack;

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

    IEnumerator EnemyAction()
    {
        Attack attack = enemyUnit.Creature.RandomAttack();
        yield return battleDialogBox.SetDialog($"{enemyUnit.Creature.Base.name} has used {attack.Base.Name}");
        
        enemyUnit.PlayAttackAnimation();
        playerUnit.PlayReceiveAttackAnimation();

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(PlayerActionCoroutine());
    }

    private void PlayerAction()
    {
        StartCoroutine(PlayerActionCoroutine());
    }

    IEnumerator PlayerActionCoroutine()
    {
        StartCoroutine(battleDialogBox.SetDialog("Select an action."));
        yield return new WaitForSeconds(0.5f);
        battleDialogBox.ToggleActions(true);
        primaryAttack.Select();
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

    public void AttackButtonPressed(int selectedAttack)
    {
        StartCoroutine(SelectAttack(selectedAttack, playerUnit.Creature.Attacks));
    }

    IEnumerator SelectAttack(int selectedAttack, List<Attack> attacks)
    {
        battleDialogBox.ToggleAttacks(false);
        battleDialogBox.ToggleDialogText(true);
        
        switch (selectedAttack)
        {
            case 0:
                yield return battleDialogBox.SetDialog($"You used {attacks[0].Base.Name}");
                break;
            case 1:
                yield return battleDialogBox.SetDialog($"You used {attacks[1].Base.Name}");
                break;
            case 2:
                yield return battleDialogBox.SetDialog($"You used {attacks[2].Base.Name}");
                break;
            case 3:
                yield return battleDialogBox.SetDialog($"You used {attacks[3].Base.Name}");
                break;
        }
        
        playerUnit.PlayAttackAnimation();
        enemyUnit.PlayReceiveAttackAnimation();
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(EnemyAction());
    }
}
