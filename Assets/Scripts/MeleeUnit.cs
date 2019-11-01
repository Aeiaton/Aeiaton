using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeUnit : BaseUnit
{

    void Start() {
        speed = 1f;
        attackWait = 0.5f;
    }

    // override public void ComputeMove() {
    //     int dir;
    //     if (isPlayer) {
    //         dir = 1;
    //     } else {
    //         dir = -1;
    //     }
        
    //     if (0 <= currentCell.position.y + dir && currentCell.position.y + dir <= 7) { 
    //         ChessBoardCell to = board.boardCells[currentCell.position.x, currentCell.position.y + dir];
    //         if (to.currentUnit == null) {
    //             destCell = to;
    //             return;
    //         }
    //     }
    //     destCell = currentCell;
    // }
    
    // TODO: if there isn't any adjacent free cell for the closest opponent, then we should consider the next closest.
    override public ChessBoardCell FindOpponent () {

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

    //     List<ChessBoardCell> opponents = new List<ChessBoardCell>();
    //     int y1 = Math.Min(currentCell.position.y + 1, 7);
    //     int x1 = Math.Max(currentCell.position.x - 1, 0);

    //     int y2 = Math.Max(currentCell.position.y - 1, 0);
    //     int x2 = Math.Min(currentCell.position.x + 1, 7);

    //     for (int y = y1; y >= y2; y--) {
    //         for (int x = x1; x <= x2; x++) {
    //             BaseUnit b = board.boardCells[x,y].currentUnit;
    //             if (b == null) continue;
    //             else if (b.isPlayer != this.isPlayer) {
    //                 opponents.Add(board.boardCells[x, y]);
    //             }
    //         }
    //     }
    //     int len = opponents.Count;
    //     if (len == 0) {
    //         opponentCell = null;
    //         return opponentCell;
    //     } else {
    //         System.Random r = new System.Random();
    //         opponentCell = opponents[r.Next(0, opponents.Count)];
    //         return opponentCell;
    //     }
    }

    // TODO: combine with FindOpponent() to only use single pass
    override public void FindTargetCell() {
        float smallestDistance = -1;
        ChessBoardCell target = null;
        foreach(ChessBoardCell cell in board.boardCells) {
            if (!cell.isOccupied()) {
                float toUnit = (cell.position - currentCell.position).magnitude;
                float toOpponent = (cell.position - opponentCell.position).magnitude;
                if (target == null || toUnit + toOpponent < smallestDistance) {
                    target = cell;
                    smallestDistance = toUnit + toOpponent;
                }

            }
        }
        targetCell = target;
        targetCell.occupied = true;
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
                dest = currentCell;
                break;
            }
            ChessBoardCell cell = board.boardCells[position.x, position.y];
            float toUnit = (cell.position - currentCell.position).magnitude;
            float toOpponent = (cell.position - targetCell.position).magnitude;
            if (dest == null || toUnit + toOpponent < smallestDistance) {
                dest = cell;
                smallestDistance = toUnit + toOpponent;
            }
        }

        destCell = dest;
    }

}
