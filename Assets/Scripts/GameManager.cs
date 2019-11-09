using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public ChessBoard board;
    public UnitManager unitManager;
    public ShopManager shopManager;

    public enum Stage {SETUP, PLAY};
    public Stage gameStage;

    void Start()
    {
        gameStage = Stage.SETUP;
        board.CreateBoard();
        board.CreateBench();
        unitManager.Setup(board);
    }

    public void SwitchGameStage(Stage newStage) {
        GameObject hand;
        switch (newStage) {
            case Stage.SETUP:

                // Show the hand selection UI
                hand = GameObject.Find("Hand");
                hand.SetActive(true);

                // Show the play button UI
                GameObject.Find("EndSetupButton").SetActive(true);

                break;
            case Stage.PLAY:

                // Hide the hand selection UI
                hand = GameObject.Find("Hand");
                hand.SetActive(false);

                // Hide the play button UI
                GameObject.Find("EndSetupButton").SetActive(false);


                // Start updating units
                GameObject unitManagerObject = GameObject.Find("UnitManager");
                unitManagerObject.GetComponent<UnitManager>().Begin();

                break;
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            //GameObject hand = GameObject.Find("Hand");
            //hand.SetActive(!hand.activeSelf);
        }
    }

}
