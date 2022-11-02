using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] Text dialogText;
    
    [SerializeField] private GameObject actionSelect;
    [SerializeField] private GameObject attackSelect;
    [SerializeField] private GameObject attackDescription;
    
    [SerializeField] private List<Button> actionTexts;
    [SerializeField] private List<Text> attackTexts;
    [SerializeField] private List<GameObject> attackButtons;
    
    [SerializeField] private Text ppText;
    [SerializeField] private Text typeText;
    [SerializeField] private BattleUnit playerUnit;
    
    public bool isWriting = false;

    public float charactersPerSecond;

    public Button primaryAttackButton;

    private void Update()
    {
        SetAttackDescription(playerUnit.Creature.Attacks);
    }

    public IEnumerator SetDialog(string message)
    {
        isWriting = true;
        dialogText.text = "";
        foreach (var character in message)
        {
            dialogText.text += character;
            yield return new WaitForSeconds(1 / charactersPerSecond);
        }

        yield return new WaitForSeconds(0.5f);
    }

    public void ToggleDialogText(bool activated)
    {
        dialogText.enabled = activated;
    }

    public void ToggleActions(bool activated)
    {
        actionSelect.SetActive(activated);
    }

    public void ToggleAttacks(bool activated)
    {
        primaryAttackButton.Select();
        attackSelect.SetActive(activated);
        attackDescription.SetActive(activated);
    }

    public void SetCreatureAttacks(List<Attack> attacks)
    {
        for (int i = 0; i < attackTexts.Count; i++)
        {
            if (i < attacks.Count)
            {
                attackTexts[i].text = attacks[i].Base.Name;
                attackButtons[i].SetActive(true);
            }
            else
            {
                attackTexts[i].text = "-----";
                attackButtons[i].SetActive(false);
            }
        }
    }

    public void SetAttackDescription(List<Attack> attacks)
    {
        GameObject selectedButton = EventSystem.current.currentSelectedGameObject;

        if (selectedButton == attackButtons[0])
        {
            ppText.text = $"{attacks[0].Pp}/{attacks[0].Base.PP}";
            typeText.text = $"{attacks[0].Base.Type}";
        }
        else if (selectedButton == attackButtons[1])
        {
            ppText.text = $"{attacks[1].Pp}/{attacks[1].Base.PP}";
            typeText.text = $"{attacks[1].Base.Type}";
        }
        else if (selectedButton == attackButtons[2])
        {
            ppText.text = $"{attacks[2].Pp}/{attacks[2].Base.PP}";
            typeText.text = $"{attacks[2].Base.Type}";
        }
        else if (selectedButton == attackButtons[3])
        {
            ppText.text = $"{attacks[3].Pp}/{attacks[3].Base.PP}";
            typeText.text = $"{attacks[3].Base.Type}";
        }
    }
}
