using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public ChessBoard board;
    public UnitManager unitManager;
    public ShopManager shopManager;

    public GameObject hand;

    public GameObject endSetupButton;

    public enum Stage {SETUP, PLAY};
    public Stage gameStage;

    void Start()
    {
        gameStage = Stage.SETUP;
        board.CreateBoard();
        board.CreateBench();
        hand = GameObject.Find("Hand");
        endSetupButton = GameObject.Find("EndSetupButton");
    }

    public void SwitchGameStage(Stage newStage) {
        switch (newStage) {
            case Stage.SETUP:
                // Show the hand selection UI
                hand.SetActive(true);
                Debug.Log("switching game stage");

                // Show the play button UI
                endSetupButton.SetActive(true);

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
