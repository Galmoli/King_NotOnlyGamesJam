using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private bool player0PosCorrect;
    private bool player1PosCorrect;
    private bool player0Checked;
    private bool player1Checked;

    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    public void SetPlayer0State(bool state)
    {
        player0PosCorrect = state;
        player0Checked = true;
        if (player1Checked)
        {
            if(player0PosCorrect && player1PosCorrect) CorrectPos();
            else IncorrectPos();
        }
    }
    public void SetPlayer1State(bool state)
    {
        player1PosCorrect = state;
        player1Checked = true;
        if (player0Checked)
        {
            if(player0PosCorrect && player1PosCorrect) CorrectPos();
            else IncorrectPos();
        }
    }

    private void CorrectPos()
    {
        Debug.Log("<color=green> Correct Pos </color>");
        CleanPos();
    }

    private void IncorrectPos()
    {
        Debug.Log("<color=red> Incorrect Pos </color>");
        CleanPos();
    }

    private void CleanPos()
    {
        player0Checked = false;
        player1Checked = false;
    }
}
