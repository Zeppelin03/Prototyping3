using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : Pieces
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[6, 10];

        Pieces c;
        int i, j;


        //Top Side
        i = CurrentX - 1;
        j = CurrentY + 1;

        if (CurrentY != 9)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 || i < 10)
                {
                    c = BoardManager.Instance.PlayerPieces[i, j];
                    if (c == null)
                        r[i, j] = true;
                    else if (isWhite != c.isWhite)
                        r[i, j] = true;
                }
                i++;
            }
        }

        //Down Side
        i = CurrentX - 1;
        j = CurrentY - 1;

        if (CurrentY != 0)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 || i < 10)
                {
                    c = BoardManager.Instance.PlayerPieces[i, j];
                    if (c == null)
                        r[i, j] = true;
                    else if (isWhite != c.isWhite)
                        r[i, j] = true;
                }
                i++;
            }
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
