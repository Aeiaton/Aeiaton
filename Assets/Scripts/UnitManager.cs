using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{

    public GameObject unitPrefab;

    private List<BaseUnit> playerUnits = null;

    public void Setup(ChessBoard board)
    {
        playerUnits = new List<BaseUnit>();
        createUnit(board.benchCells[0, 0]);
    }

    public BaseUnit createUnit(ChessBoardCell cell) {
        GameObject unit = Instantiate(unitPrefab);

        unit.transform.SetParent(transform);
        unit.transform.localScale = new Vector3(1, 1, 1);
        unit.transform.localRotation = Quaternion.identity;

        Type unitType = typeof(TestUnit);
        BaseUnit baseUnit = (BaseUnit) unit.AddComponent(unitType);
        playerUnits.Add(baseUnit);


        baseUnit.Setup(this, new Color32(66, 245, 230, 255));
        baseUnit.Place(cell);

        return baseUnit;
    }

}
