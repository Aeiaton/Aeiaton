using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdleUnit : BaseUnit
{

    override public void Setup(UnitManager unitManager, ChessBoard board, bool isPlayer, Color32 color) {
        base.Setup(unitManager, board, isPlayer, color);
        health = AutoChessData.IDLE_HEALTH;
        max_mana = AutoChessData.IDLE_MANA;
        Image image = gameObject.GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>("mech");
    }

    override public ChessBoardCell FindOpponent() { return null; }
    override public void FindTargetCell() {}
    override public void ComputeMove() {}
    override public bool CanAttack() { return false; }

}
