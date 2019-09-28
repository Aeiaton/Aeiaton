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
    protected ChessBoardCell currentCell;
    protected ChessBoardCell destCell;
    protected ChessBoard board;

    protected bool isPlayer;

    public virtual void Setup(UnitManager unitManager, ChessBoard board, bool isPlayer, Color32 color)
    {
        this.unitManager = unitManager;
        this.color = color;
        GetComponent<Image>().color = color;
        this.board = board;
        this.isPlayer = isPlayer;
        rectTransform = GetComponent<RectTransform>();
    }

    public void Place(ChessBoardCell cell) {
        currentCell = cell;
        currentCell.currentUnit = this;
        transform.position = cell.transform.position;
        Debug.Log(cell.transform.position);
        gameObject.SetActive(true);
    }

    public void Move() {
        Place(destCell);
    }

    public virtual void ComputeMove();
    

}
