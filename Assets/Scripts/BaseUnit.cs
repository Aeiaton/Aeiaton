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
    public ChessBoardCell previousCell;
    protected ChessBoardCell destCell;

    protected ChessBoardCell opponentCell;          // cell where the opponent unit is
    protected ChessBoardCell targetCell;            // cell where we want to go to attack opponent

    protected ChessBoard board;

    public bool isActive;

    // True when unit is moving between source and destination cells
    public bool isMoving;
    public bool isFighting;
    public bool arrivedAtNextCell;

    public float health;
    protected float damage = 1;
    public float attackWait;
    public float timeSinceLastAttack = 0;
    public float speed;

    private float movementProgress;

    public bool isPlayer;

    public virtual void Setup(UnitManager unitManager, ChessBoard board, bool isPlayer, Color32 color, int health) {
        this.unitManager = unitManager;
        this.color = color;
        GetComponent<Image>().color = color;
        this.board = board;
        this.isPlayer = isPlayer;
        this.health = health;
        this.isActive = false;
        isMoving = false;
        isFighting = false;
        arrivedAtNextCell = false;
        rectTransform = GetComponent<RectTransform>();
    }

    // Assign the cell to move to and begin movement
    public void Place(ChessBoardCell cell, bool initial) {
        if (currentCell) {
            currentCell.currentUnit = null;
            currentCell.occupied = false;
        }
        previousCell = currentCell ==  null ? cell : currentCell;
        currentCell = cell;
        currentCell.currentUnit = this;
        gameObject.SetActive(true);
        movementProgress = 0;
        isMoving = true;
        arrivedAtNextCell = false;
        isFighting = false;
        if (initial) {
            rectTransform.position = currentCell.transform.position;   
        }
    }

    public void Activate() {
        this.isActive = true;
    }

    public void Update() {
        if (!isActive) return;

        // The unit is moving to a previously decided cell, so we don't make any decisions until it reaches the next cell.
        if (isMoving) {
            movementProgress += speed * Time.deltaTime;
            rectTransform.position = Vector3.Lerp(previousCell.transform.position, currentCell.transform.position, movementProgress);

            // Check if we reached the dest cell
            if (Mathf.Abs(rectTransform.position.x - currentCell.transform.position.x) <= 0.1 && Mathf.Abs(rectTransform.position.y - currentCell.transform.position.y) <= 0.1) {
                isMoving = false;
                rectTransform.position = currentCell.transform.position;
                arrivedAtNextCell = true;
            }

            // Check if we reached the target cell
            if (targetCell != null && Mathf.Abs(rectTransform.position.x - targetCell.transform.position.x) <= 0.1 && Mathf.Abs(rectTransform.position.y - targetCell.transform.position.y) <= 0.1) {
                isMoving = false;
                isFighting = true;
                rectTransform.position = currentCell.transform.position;
                arrivedAtNextCell = true;
            }

            return;
        }

        // Once the unit reaches a cell, it can decide who the opponent is and how to get there, or fight if it is already there
        if (arrivedAtNextCell && !isFighting) {
            if (isPlayer) Debug.Log("Arrived at cell, choosing next cell");
            // Sets opponentCell
            FindOpponent();
            // Sets targetCell
            FindTargetCell();
            // Sets destCell
            ComputeMove();

            // Ignore the move if we are already at the target position
            if (destCell.position == currentCell.position) {
                isFighting = true;
            } else {
                if (isPlayer) Debug.Log("curr="+currentCell.position+"opp="+opponentCell.position+"target="+targetCell.position+" dest="+destCell.position);
                Place(destCell, false);
            }
            
        }

        if (isFighting) {
            if (opponentCell.currentUnit == null) {
                if (isPlayer) Debug.Log("Opponent is gone");
                isFighting = false;
            } else {
                if (isPlayer) Debug.Log("Fighting");
                //DealDamage();
            }
        }
        
    }

    public void Tick() {
        if (opponentCell == null) { // not in combat
            FindOpponent();
            if (opponentCell == null) { //attempts to find opponent. If nothing around, move
                ComputeMove();
                if (movementProgress >= 1) {
                    Place(destCell, false);
                }
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

    public void TakeDamage(float damage) {
        health -= damage;
        GetComponentInChildren<HealthBar>().health = health;
    }

    public void DealDamage() {
        if (opponentCell != null) {
            BaseUnit opponent = opponentCell.currentUnit;
            opponent.TakeDamage(damage);
        }
    }
    
    public override void OnBeginDrag(PointerEventData eventData) {}

    public override void OnDrag(PointerEventData eventData) {
        transform.position += (Vector3) eventData.delta;
    }

    public override void OnEndDrag(PointerEventData eventData) {
        ChessBoardCell targetCell = null;
        foreach (ChessBoardCell cell in currentCell.board.boardCells) {
            if (RectTransformUtility.RectangleContainsScreenPoint(cell.rectTransform, Input.mousePosition)) {
                targetCell = cell;
                break;
            }
        }
        if (!targetCell) {
            transform.position = currentCell.gameObject.transform.position;
        } else {
            Place(targetCell, false);
            //currentCell = targetCell;
            //currentCell.currentUnit = this;
            transform.position = currentCell.transform.position;
        }
    }

    public abstract ChessBoardCell FindOpponent();
    public abstract void FindTargetCell();
    public abstract void ComputeMove();
    

}
