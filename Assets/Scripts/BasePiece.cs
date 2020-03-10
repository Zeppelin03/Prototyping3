using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePiece : Pieces
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[6, 10];
        Pieces c, c2;

        //WHite team move
        if (isWhite)
        {

            //Forward
            if (CurrentY != 9)
            {
                c = BoardManager.Instance.PlayerPieces[CurrentX, CurrentY + 1];
                if (c == null)
                    r[CurrentX, CurrentY + 1] = true;
            }

            //Backward
            if (CurrentY != 0)
            {
                c = BoardManager.Instance.PlayerPieces[CurrentX, CurrentY + 1];
                if (c == null)
                    r[CurrentX, CurrentY + 1] = true;
            }

            //Middle Left
            if (CurrentX != 0)
            {
                c = BoardManager.Instance.PlayerPieces[CurrentX - 1, CurrentY];
                if (c == null)
                    r[CurrentX - 1, CurrentY] = true;
                else if (isWhite != c.isWhite)
                    r[CurrentX - 1, CurrentY] = true;
            }

            //Middle Right
            if (CurrentX != 5)
            {
                c = BoardManager.Instance.PlayerPieces[CurrentX - 1, CurrentY];
                if (c == null)
                    r[CurrentX + 1, CurrentY] = true;
                else if (isWhite != c.isWhite)
                    r[CurrentX + 1, CurrentY] = true;
            }
        }

        else
        {
            //Forward
            if (CurrentY != 0)
            {
                c = BoardManager.Instance.PlayerPieces[CurrentX, CurrentY - 1];
                if (c == null)
                    r[CurrentX, CurrentY - 1] = true;
            }
        }

        //Backward
        if (CurrentY != 9)
        {
            c = BoardManager.Instance.PlayerPieces[CurrentX, CurrentY + 1];
            if (c == null)
                r[CurrentX, CurrentY + 1] = true;
        }

        //Middle Left
        if (CurrentX != 0)
        {
            c = BoardManager.Instance.PlayerPieces[CurrentX - 1, CurrentY];
            if (c == null)
                r[CurrentX - 1, CurrentY] = true;
            else if (isWhite != c.isWhite)
                r[CurrentX - 1, CurrentY] = true;
        }

        //Middle Right
        if (CurrentX != 5)
        {
            c = BoardManager.Instance.PlayerPieces[CurrentX - 1, CurrentY];
            if (c == null)
                r[CurrentX + 1, CurrentY] = true;
            else if (isWhite != c.isWhite)
                r[CurrentX + 1, CurrentY] = true;
        }

        return r;
    }
}
