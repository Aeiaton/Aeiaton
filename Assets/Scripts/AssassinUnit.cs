using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssassinUnit : BaseUnit
{
    protected bool firstMove = true;
    // 'base' is keyword, references the parent
    override public void Setup(UnitManager unitManager, ChessBoard board, bool isPlayer, Color32 color) {
        base.Setup(unitManager, board, isPlayer, color);
        
        this.speed = AutoChessData.ASSASSIN_SPEED;
        this.health = AutoChessData.ASSASSIN_HEALTH;
        this.attackWait = AutoChessData.ASSASSIN_ATTACK_SPEED;
        this.max_mana = AutoChessData.ASSASSIN_MANA;

        Image image = gameObject.GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>("assassin");
    }

    // TODO: if there isn't any adjacent free cell for the closest opponent, then we should consider the next closest.
    override public ChessBoardCell FindOpponent () {
        if (!firstMove) {
            float smallestDistance = -1;
            ChessBoardCell closest = null;
            foreach(ChessBoardCell cell in board.boardCells) {
                if (cell.currentUnit != null && cell.currentUnit.isPlayer != isPlayer) {
                    if (closest == null || (cell.position - currentCell.position).magnitude < smallestDistance) {
                        closest = cell;
                        smallestDistance = (cell.position - currentCell.position).magnitude;
                    }
                }
            }
            opponentCell = closest;
            return closest;
        } else { // first move
            float furthestDistance = -1;
            ChessBoardCell furthest = null;
            foreach(ChessBoardCell cell in board.boardCells) {
                if (cell.currentUnit != null && cell.currentUnit.isPlayer != isPlayer) {
                    if (furthest == null || (cell.position - currentCell.position).magnitude > furthestDistance) {
                        furthest = cell;
                        furthestDistance = (cell.position - currentCell.position).magnitude;
                    }
                }
            }
            opponentCell = furthest;
            return furthest;
        }
    }

    // TODO: combine with FindOpponent() to only use single pass
    override public void FindTargetCell() {
        float smallestDistance = -1;
        ChessBoardCell target = null;
        foreach(ChessBoardCell cell in board.boardCells) {
            if (!cell.isOccupied()) {
                float toUnit = 0*(cell.position - currentCell.position).magnitude;
                float toOpponent = (cell.position - opponentCell.position).magnitude;
                if (target == null || toUnit + toOpponent < smallestDistance) {
                    target = cell;
                    smallestDistance = toUnit + toOpponent;
                }

            }
        }
        targetCell = target;
    }

    override public void ComputeMove() {
        if (!firstMove) {
            float smallestDistance = -1;
            ChessBoardCell dest = null;

            List<Vector2Int> candidateCells = new List<Vector2Int>();
            candidateCells.Add(new Vector2Int(currentCell.position.x + 1, currentCell.position.y));
            candidateCells.Add(new Vector2Int(currentCell.position.x - 1, currentCell.position.y));
            candidateCells.Add(new Vector2Int(currentCell.position.x, currentCell.position.y + 1));
            candidateCells.Add(new Vector2Int(currentCell.position.x, currentCell.position.y - 1));

            foreach(Vector2Int position in candidateCells) {
                if (position.x < 0 || position.x > 7 || position.y < 0 || position.y > 7) continue;
                if (targetCell.position == position) {
                    dest = targetCell;
                    break;
                }
                ChessBoardCell cell = board.boardCells[position.x, position.y];
                float toUnit = (cell.position - currentCell.position).magnitude;
                float toOpponent = (cell.position - targetCell.position).magnitude;
                if ((dest == null || toUnit + toOpponent < smallestDistance) && !cell.isOccupied()) {
                    dest = cell;
                    smallestDistance = toUnit + toOpponent;
                }
            }

            destCell = dest;
        } else { // first move
            firstMove = false;
            destCell = targetCell;
        }
    }

    override public bool CanAttack() {
        return (currentCell.position - opponentCell.position).magnitude == 1;
    }
}
