using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitManager : MonoBehaviour
{

    public Boolean running;
    public GameObject unitPrefab;
    public Image meleeUnitImage;

    private List<BaseUnit> playerUnits = null;
    private float elapsedTime;

    public void Setup(ChessBoard board)
    {
        running = false;
        playerUnits = new List<BaseUnit>();

        Type[,] opponentUnitTypes = new Type[8,4];
        opponentUnitTypes[0, 3] = typeof(MeleeUnit);
        opponentUnitTypes[1, 3] = typeof(RangedUnit);
        opponentUnitTypes[2, 3] = typeof(AssassinUnit);
        opponentUnitTypes[3, 3] = typeof(MeleeUnit);
        opponentUnitTypes[4, 3] = typeof(MeleeUnit);

        AddUnit(board, typeof(MeleeUnit), 0, 0, true, false);
        AddUnit(board, typeof(MeleeUnit), 0, 1, true, false);
        AddUnit(board, typeof(RangedUnit), 0, 2, true, false);
        AddUnit(board, typeof(RangedUnit), 0, 3, true, false);
        AddUnit(board, typeof(AssassinUnit), 0, 4, true, false);
        AddUnit(board, typeof(AssassinUnit), 0, 5, true, false);
        AddUnit(board, typeof(IdleUnit), 0, 6, true, false);
        AddUnit(board, typeof(IdleUnit), 0, 7, true, false);

        for (int x = 0; x < 8; x++) {
            for (int y = 0; y < 4; ++y) {
                // if (playerUnitTypes[x, y] != null) {
                //     AddUnit(board, playerUnitTypes[x, y], x, y, true, true);
                // }
                if (opponentUnitTypes[x, y] != null) {
                    AddUnit(board, opponentUnitTypes[x, y], x, y + 4, false, true);
                }
            }
        }
    }

    // onBoard = true to place on board, false for bench
    private void AddUnit(ChessBoard board, Type type, int x, int y, bool isPlayer, bool onBoard) {
        // Instantiate game object from prefab
        GameObject unit = Instantiate(unitPrefab);
        unit.transform.SetParent(transform);
        unit.transform.localScale = new Vector3(1, 1, 1);
        unit.transform.localRotation = Quaternion.identity;

        // Add the unit script based on whatever type of unit this is
        BaseUnit baseUnit = (BaseUnit) unit.AddComponent(type);
        playerUnits.Add(baseUnit);

        // Temp color to differentiate player from enemy
        Color32 color = isPlayer ? new Color32(255, 255, 255, 255) : new Color32(255, 1, 1, 255);

        // Set variables and stuff
        baseUnit.Setup(this, board, isPlayer, color);

        // Give the health bar the health so it knows how much to decrease by
        HealthBar hb = unit.GetComponentInChildren<HealthBar>();
        hb.Setup(baseUnit.health);
        
        // Place on the board/bench
        if (onBoard) baseUnit.Place(board.boardCells[x, y], true, true);
        else baseUnit.Place(board.benchCells[x, y], true, false);
    }

    public void Begin() {
        this.running = true;
        foreach(BaseUnit unit in playerUnits) {
            if (!unit.onBench) unit.Activate();
        }
    }

    // TODO: consider a reasonable time frame to remove dead units
    public void Update() {
        if (!running) return;
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 0.5f) {
            elapsedTime = 0;

            List<BaseUnit> toRemove = new List<BaseUnit>();

            foreach(BaseUnit unit in playerUnits) {
                if (unit.health <= 0) toRemove.Add(unit);
            }
            
            foreach(BaseUnit unit in toRemove) {
                playerUnits.Remove(unit);
                unit.currentCell.currentUnit = null;
                Destroy(unit.gameObject);
            }
        }
    }

}
