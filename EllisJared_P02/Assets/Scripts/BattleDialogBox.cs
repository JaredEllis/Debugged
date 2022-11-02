using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        attackSelect.SetActive(activated);
        attackDescription.SetActive(activated);
    }
}
