using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessBoard : MonoBehaviour
{

    public GameObject cellPrefab;
    public ChessBoardCell[,] cells = new ChessBoardCell[8, 8];

    public void CreateBoard()
    {
        for (int y = 0; y < 8; ++y)
        {
            for (int x = 0; x < 8; ++x)
            {
                GameObject cell = Instantiate(cellPrefab, transform);
                RectTransform rectTransform = cell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(x * 100 + 50, y * 100 + 50);
                cells[x, y] = cell.GetComponent<ChessBoardCell>();
                cells[x, y].Setup(new Vector2Int(x, y), this);
                if (y > 3) {
                    cells[x, y].GetComponent<Image>().color = (x + y) % 2 == 0 ?
                    new Color32(102, 52, 52, 255) :
                    new Color32(130, 66, 66, 255);
                } else {
                    cells[x, y].GetComponent<Image>().color = (x + y) % 2 == 0 ?
                    new Color32(52, 71, 102, 255) :
                    new Color32(66, 91, 130, 255);
                }
                
            }
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
