using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class BaseUnit : EventTrigger
{

    public Color color = Color.clear;
    protected RectTransform rectTransform = null;
    protected UnitManager unitManager;
    public ChessBoardCell currentCell;
    protected ChessBoardCell destCell;

    protected ChessBoardCell opponentCell;
    protected ChessBoard board;

    protected int health;

    protected int damage = 1;

    public bool isPlayer;

    public virtual void Setup(UnitManager unitManager, ChessBoard board, bool isPlayer, Color32 color, int health)
    {
        this.unitManager = unitManager;
        this.color = color;
        GetComponent<Image>().color = color;
        this.board = board;
        this.isPlayer = isPlayer;
        this.health = health;
        rectTransform = GetComponent<RectTransform>();
    }

    public void Place(ChessBoardCell cell)
    {
        if (currentCell) currentCell.currentUnit = null;
        currentCell = cell;
        currentCell.currentUnit = this;
        rectTransform.position = currentCell.transform.position;
        Debug.Log("unit position: "+cell.transform.position);
        gameObject.SetActive(true);
    }

    public override void OnBeginDrag(PointerEventData eventData) {}

    public override void OnDrag(PointerEventData eventData) {
        transform.position += (Vector3) eventData.delta;
    }

    public override void OnEndDrag(PointerEventData eventData) {
        ChessBoardCell targetCell = null;
        foreach (ChessBoardCell cell in currentCell.board.cells) {
            if (RectTransformUtility.RectangleContainsScreenPoint(cell.rectTransform, Input.mousePosition)) {
                targetCell = cell;
                break;
            }
        }
        if (!targetCell) {
            transform.position = currentCell.gameObject.transform.position;
        } else {
            Place(targetCell);
            //currentCell = targetCell;
            //currentCell.currentUnit = this;
            transform.position = currentCell.transform.position;
        }
    }
    public void Tick() {
        if (opponentCell == null) { // not in combat
            FindOpponent();
            if (opponentCell == null) { //attempts to find opponent. If nothing around, move
                ComputeMove();
                Place(destCell);
            } else {               // found new opponent
                DealDamage();
            }
        } else { // already engaged in combat
            if (opponentCell.currentUnit == null) { // opponentCell killed by someone else
                opponentCell = null;
            } else {
                DealDamage();
            }
        }
    }

    public void DealDamage() {
        if (opponentCell != null) {
            BaseUnit opponent = opponentCell.currentUnit;
            opponent.health -= damage;
            if (opponent.health <= 0) {
                unitManager.Remove(opponent);
                opponentCell = null;
            }
        }
    }
    
    public abstract ChessBoardCell FindOpponent();
    public abstract void ComputeMove();
    

}
