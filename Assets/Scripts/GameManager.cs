using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public ChessBoard board;
    public UnitManager unitManager;
    public HandManager handManager;
    public DeckManager DeckManager;

    public enum Stage {SETUP, PLAY};
    public Stage gameStage;

    public GameObject hand;

    public GameObject endSetupButton;

    public enum Stage {SETUP, PLAY};
    public Stage gameStage;

    void Start()
    {
        gameStage = Stage.SETUP;
        board.CreateBoard();
        board.CreateBench();
        unitManager.Setup(board);
        handManager.Setup();
        DeckManager.Setup();
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
                hand.SetActive(false);

                // Hide the play button UI
                endSetupButton.SetActive(false);
            
                // Start updating units
                GameObject unitManagerObject = GameObject.Find("UnitManager");
                unitManager.Setup(board);
                unitManagerObject.GetComponent<UnitManager>().Begin();

                break;
        }
    }

}
