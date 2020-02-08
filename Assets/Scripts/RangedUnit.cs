using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangedUnit : BaseUnit {

    override public void Setup(UnitManager unitManager, ChessBoard board, bool isPlayer, Color32 color) {
        base.Setup(unitManager, board, isPlayer, color);
        speed = AutoChessData.RANGED_SPEED;
        health = AutoChessData.RANGED_HEALTH;
        attackWait = AutoChessData.RANGED_ATTACK_SPEED;
        max_mana = AutoChessData.RANGED_MANA;
        Image image = gameObject.GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>("ranged");
    }

    // TODO: ranged unit might want to move away from opponent, and then start attacking, so that it keeps its distance
    // TODO: don't just find cell within range, find _closest_ cell within range
    // TODO: don't repeat this code, make something more efficient and put it in a static function somewhere
    override public ChessBoardCell FindOpponent() {
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
    }
    override public void FindTargetCell() {
        // Looking for closest cell that is barely in range to hit opponent
        float distance = -1;
        ChessBoardCell target = null;
        foreach(ChessBoardCell cell in board.boardCells) {
            if (!cell.isOccupied()) {
                float toOpponent = (cell.position - opponentCell.position).magnitude;
                if (target == null || toOpponent - AutoChessData.RANGED_ATTACK_RANGE < distance) {
                    target = cell;
                    distance = toOpponent;
                }

            }
        }
        targetCell = target;
    }


    override public void ComputeMove() {
        
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
    }

    override public bool CanAttack() {
        return (currentCell.position - opponentCell.position).magnitude <= AutoChessData.RANGED_ATTACK_RANGE;
    }

}
