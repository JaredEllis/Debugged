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

        if (playerUnit.Creature.Speed < enemyUnit.Creature.Speed)
        {
            StartCoroutine(EnemyAction());
        }
        else
        {
            PlayerAction();
        }
    }

    IEnumerator EnemyAction()
    {
        Attack attack = enemyUnit.Creature.RandomAttack();
        yield return battleDialogBox.SetDialog($"{enemyUnit.Creature.Base.name} has used {attack.Base.Name}");

        var oldHPValue = playerUnit.Creature.HP;

        enemyUnit.PlayAttackAnimation();
        playerUnit.PlayReceiveAttackAnimation();

        var damageDesc = playerUnit.Creature.ReceiveDamage(enemyUnit.Creature, attack);
        playerHUD.UpdateCreatureData(oldHPValue);
        yield return ShowDamageDescription(damageDesc);

        if (damageDesc.Dead)
        {
            yield return battleDialogBox.SetDialog($"You have been killed by a {enemyUnit.Creature.Base.name}");
            playerUnit.PlayDeathAnimation();
            yield return new WaitForSeconds(1.5f);
            // TODO: Implement end of battle.
        }
        else
        {
            StartCoroutine(PlayerActionCoroutine());
        }

        yield return new WaitForSeconds(1.0f);
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

        var oldHPValue = enemyUnit.Creature.HP;

        if (selectedAttack == 0)
        {
            yield return battleDialogBox.SetDialog($"You used {attacks[0].Base.Name}");

            var damageDesc = enemyUnit.Creature.ReceiveDamage(playerUnit.Creature, attacks[0]);
            attacks[0].Pp--;
            enemyHUD.UpdateCreatureData(oldHPValue);
            yield return ShowDamageDescription(damageDesc);

            if (damageDesc.Dead)
            {
                yield return battleDialogBox.SetDialog($"You have killed a {enemyUnit.Creature.Base.name}");
                enemyUnit.PlayDeathAnimation();
                yield return new WaitForSeconds(1.0f);
                // TODO: Implement end of battle.
            }
            else
            {
                yield return new WaitForSeconds(1.0f);
                StartCoroutine(EnemyAction());
            }
        }
        else if (selectedAttack == 1)
        {
            yield return battleDialogBox.SetDialog($"You used {attacks[1].Base.Name}");

            var damageDesc = enemyUnit.Creature.ReceiveDamage(playerUnit.Creature, attacks[1]);
            attacks[0].Pp--;
            enemyHUD.UpdateCreatureData(oldHPValue);
            yield return ShowDamageDescription(damageDesc);

            if (damageDesc.Dead)
            {
                yield return battleDialogBox.SetDialog($"You have killed a {enemyUnit.Creature.Base.name}");
                enemyUnit.PlayDeathAnimation();
                yield return new WaitForSeconds(1.0f);
                // TODO: Implement end of battle.
            }
            else
            {
                yield return new WaitForSeconds(1.0f);
                StartCoroutine(EnemyAction());
            }
        }
        else if (selectedAttack == 2)
        {
            yield return battleDialogBox.SetDialog($"You used {attacks[2].Base.Name}");

            var damageDesc = enemyUnit.Creature.ReceiveDamage(playerUnit.Creature, attacks[2]);
            attacks[0].Pp--;
            enemyHUD.UpdateCreatureData(oldHPValue);
            yield return ShowDamageDescription(damageDesc);

            if (damageDesc.Dead)
            {
                yield return battleDialogBox.SetDialog($"You have killed a {enemyUnit.Creature.Base.name}");
                enemyUnit.PlayDeathAnimation();
                yield return new WaitForSeconds(1.0f);
                // TODO: Implement end of battle.
            }
            else
            {
                yield return new WaitForSeconds(1.0f);
                StartCoroutine(EnemyAction());
            }
        }
        else if (selectedAttack == 3)
        {
            yield return battleDialogBox.SetDialog($"You used {attacks[3].Base.Name}");

            var damageDesc = enemyUnit.Creature.ReceiveDamage(playerUnit.Creature, attacks[3]);
            attacks[0].Pp--;
            enemyHUD.UpdateCreatureData(oldHPValue);
            yield return ShowDamageDescription(damageDesc);

            if (damageDesc.Dead)
            {
                yield return battleDialogBox.SetDialog($"You have killed a {enemyUnit.Creature.Base.name}");
                enemyUnit.PlayDeathAnimation();
                yield return new WaitForSeconds(1.0f);
                // TODO: Implement end of battle.
            }
            else
            {
                yield return new WaitForSeconds(1.0f);
                StartCoroutine(EnemyAction());
            }
        }

        playerUnit.PlayAttackAnimation();
        enemyUnit.PlayReceiveAttackAnimation();
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator ShowDamageDescription(DamageDescription description)
    {
        if (description.Critical > 1)
        {
            yield return battleDialogBox.SetDialog("A critical hit!");
        }

        if (description.Type > 1)
        {
            yield return battleDialogBox.SetDialog("It is extremely effective!");
        }

        if (description.Type < 1)
        {
            yield return battleDialogBox.SetDialog("It is not effective.");
        }
    }
}
