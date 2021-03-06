﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : Pieces

{

    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[6, 10];

        //Forward
        LeaderMove(CurrentX, CurrentY + 1, ref r);

        //Backward
        LeaderMove(CurrentX, CurrentY - 1, ref r);

        //Left
        LeaderMove(CurrentX - 1, CurrentY, ref r);

        //Right
        LeaderMove(CurrentX + 1, CurrentY, ref r);

        //Top Right
        LeaderMove(CurrentX + 1, CurrentY + 1, ref r);

        //Top Left
        LeaderMove(CurrentX - 1, CurrentY + 1, ref r);

        //Bown Right
        LeaderMove(CurrentX + 1, CurrentY - 1, ref r);

        //Down Left
        LeaderMove(CurrentX - 1, CurrentY - 1, ref r);


        if (BoardManager.Instance.LeaderKilledBase == true && GameObject.Find("Board").GetComponent<BoardManager>().isWhiteTurn != true)
        {
            LeaderMove(CurrentX + 2, CurrentY + 2, ref r);
            LeaderMove(CurrentX - 2, CurrentY + 2, ref r);
            LeaderMove(CurrentX + 2, CurrentY - 2, ref r);
            LeaderMove(CurrentX - 2, CurrentY - 2, ref r);
        }
        if(BoardManager.Instance.LeaderKilledSpecial == true && GameObject.Find("Board").GetComponent<BoardManager>().isWhiteTurn != true)
        {
            LeaderMove(CurrentX, CurrentY + 2, ref r);
            LeaderMove(CurrentX, CurrentY - 2, ref r);
            LeaderMove(CurrentX - 2, CurrentY, ref r);
            LeaderMove(CurrentX + 2, CurrentY, ref r);
        }
        if (BoardManager.Instance.LeaderKilledBaseWhite == true && GameObject.Find("Board").GetComponent<BoardManager>().isWhiteTurn == true)
        {
            LeaderMove(CurrentX + 2, CurrentY + 2, ref r);
            LeaderMove(CurrentX - 2, CurrentY + 2, ref r);
            LeaderMove(CurrentX + 2, CurrentY - 2, ref r);
            LeaderMove(CurrentX - 2, CurrentY - 2, ref r);
        }
        if (BoardManager.Instance.LeaderKilledSpecialWhite == true && GameObject.Find("Board").GetComponent<BoardManager>().isWhiteTurn == true)
        {
            LeaderMove(CurrentX, CurrentY + 2, ref r);
            LeaderMove(CurrentX, CurrentY - 2, ref r);
            LeaderMove(CurrentX - 2, CurrentY, ref r);
            LeaderMove(CurrentX + 2, CurrentY, ref r);
        }

        return r;
    }

    public void LeaderMove(int x, int y, ref bool[,] r)
    {
        Pieces c;
        if (x >= 0 && x < 6 && y >= 0 && y < 10)
        {
            c = BoardManager.Instance.PlayerPieces[x, y];
            if (c == null)
                r[x, y] = true;
            else if (isWhite != c.isWhite)
                r[x, y] = true;
        }
    }
}
