using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePiece : Pieces
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[6, 10];

        //Forward
        BasePieceMove(CurrentX, CurrentY + 1, ref r);

        //Backward
        BasePieceMove(CurrentX, CurrentY - 1, ref r);

        //Left
        BasePieceMove(CurrentX - 1, CurrentY, ref r);

        //Right
        BasePieceMove(CurrentX + 1, CurrentY, ref r);

        return r;
    }

    public void BasePieceMove(int x, int y, ref bool[,] r)
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
