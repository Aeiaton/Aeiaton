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

    protected ChessBoardCell opponentCell;
    protected ChessBoardCell targetCell;

    protected ChessBoard board;

    public bool isActive;
    public bool onBench;

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

    public virtual void Setup(UnitManager unitManager, ChessBoard board, bool isPlayer, Color32 color) {
        this.unitManager = unitManager;
        this.color = color;
        onBench = true;
        GetComponent<Image>().color = color;
        this.board = board;
        this.isPlayer = isPlayer;
        this.isActive = false;
        isMoving = false;
        isFighting = false;
        arrivedAtNextCell = false;
        rectTransform = GetComponent<RectTransform>();
    }

    // Assign the cell to move to and begin movement, onBoard = true if placing onto the board as opposed to the bench
    public void Place(ChessBoardCell cell, bool initial, bool onBoard) {
        if (currentCell) {
            currentCell.currentUnit = null;
            currentCell.occupied = false;
        }
        onBench = !onBoard;
        previousCell = (currentCell ==  null || initial) ? cell : currentCell;
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

            movementProgress += Time.deltaTime;
            rectTransform.position = Vector3.Lerp(previousCell.transform.position, currentCell.transform.position, movementProgress / speed);

            // Check if we reached the dest cell
            if (Mathf.Abs(rectTransform.position.x - currentCell.transform.position.x) <= 0.1 && Mathf.Abs(rectTransform.position.y - currentCell.transform.position.y) <= 0.1) {
                isMoving = false;
                rectTransform.position = currentCell.transform.position;
                arrivedAtNextCell = true;
            }

            // Check if we reached the target cell
            if (targetCell != null && Mathf.Abs(rectTransform.position.x - targetCell.transform.position.x) <= 0.1 && Mathf.Abs(rectTransform.position.y - targetCell.transform.position.y) <= 0.1) {
                isMoving = false;
                rectTransform.position = currentCell.transform.position;
                arrivedAtNextCell = true;
                if (CanAttack()) isFighting = true;
            }

            return;
        }

        // Once the unit reaches a cell, it can decide who the opponent is and how to get there, or fight if it is already there
        if (arrivedAtNextCell && !isFighting) {

            FindOpponent();
            if (opponentCell == null) return;

            if (CanAttack()) {
                // If we are already in position to hit an opponent, then don't bother calculating the position.
                isFighting = true;
            } else {
                FindTargetCell();
                ComputeMove();
                if (destCell != null) {
                    Place(destCell, false, true);
                }
                
            }
            
        }

        if (isFighting) {
            if (opponentCell.currentUnit == null) {
                // Opponent is no longer there; maybe moved, maybe was killed
                isFighting = false;
            } else {
                // Only attack after you wait for the correct amount of time, not every frame
                if (timeSinceLastAttack >= attackWait) {
                    DealDamage();
                    timeSinceLastAttack = 0;
                } else {
                    timeSinceLastAttack += Time.deltaTime;
                }
                
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
            Debug.Log("BaseUnit ended drag, failed to find cell");
            transform.position = currentCell.gameObject.transform.position;
        } else {
            Place(targetCell, true, true);
            //transform.position = currentCell.transform.position;
        }
    }

    // Searches board to find opponent. Generally, the closest.
    public abstract ChessBoardCell FindOpponent();
    // Sets targetCell. Based on the opponent it found, search for a 
    // cell to go to so that it can fight that opponent. For melee, this 
    // is right next to the opponent, for ranged maybe father away
    public abstract void FindTargetCell();
    // Based on the target cell, set destCell. Choose the next cell to go
    // to that is in the direction of the target cell.
    public abstract void ComputeMove();
    // Check if unit can attack the opponent from its current position.
    public abstract bool CanAttack();    

}
