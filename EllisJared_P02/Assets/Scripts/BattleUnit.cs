using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Audio;

public class BattleUnit : MonoBehaviour
{
    public CreatureBase _base;
    public int _level;
    public bool isPlayer;
    public Creature Creature { get; set; }
    
    private Vector3 initialPosition;
    private Image creatureImage;
    private Color initialColor;
    [SerializeField] private float startTimeAnimation, attackTimeAnimation, dieTimeAnimation, hitTimeAnimation;

    private void Awake()
    {
        creatureImage = GetComponent<Image>();
        initialPosition = creatureImage.transform.localPosition;
        initialColor = creatureImage.color;
    }

    public void SetupCreature()
    {
        Creature = new Creature(_base, _level);

        creatureImage.sprite = isPlayer ? Creature.Base.BackSprite : Creature.Base.FrontSprite;
        
        PlayStartAnimation();
    }

    private void PlayStartAnimation()
    {
        creatureImage.transform.localPosition = isPlayer ? new Vector3(initialPosition.x - 400, initialPosition.y) : new Vector3(initialPosition.x + 400, initialPosition.y);
        creatureImage.transform.DOLocalMoveX(initialPosition.x, startTimeAnimation);
    }

    public void PlayAttackAnimation()
    {
        AudioManager.Instance.PlaySFX("AttackEffect");
        var seq = DOTween.Sequence();
        seq.Append(creatureImage.transform.DOLocalMoveX(initialPosition.x + (isPlayer ? 1 : -1)*60, 0.2f));
        seq.Append(creatureImage.transform.DOLocalMoveX(initialPosition.x, 0.3f));
    }

    public void PlayReceiveAttackAnimation()
    {
        AudioManager.Instance.PlaySFX("DamageEffect");
        creatureImage.transform.DOShakePosition(1, 20, 10, 90, false, true);
        var seq = DOTween.Sequence();

        for (int i = 0; i < 3; i++)
        {
            seq.Append(creatureImage.DOColor(Color.gray, hitTimeAnimation));
            seq.Append(creatureImage.DOColor(initialColor, hitTimeAnimation));
        }
    }

    public void PlayDeathAnimation()
    {
        var seq = DOTween.Sequence();
        seq.Join(creatureImage.DOFade(0, dieTimeAnimation));
        seq.Join(creatureImage.transform.DOShakePosition(1, 20, 10, 90, false, true));
        AudioManager.Instance.PlaySFX("DeathEffect");
        AudioManager.Instance.ToggleMusic();
    }
}
