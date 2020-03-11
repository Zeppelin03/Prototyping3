using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPiece : Pieces
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[6, 10];

        //Forward
        SpecialPieceMove(CurrentX, CurrentY + 1, ref r);
        SpecialPieceMove(CurrentX, CurrentY + 2, ref r);

        //Backward
        SpecialPieceMove(CurrentX, CurrentY - 1, ref r);
        SpecialPieceMove(CurrentX, CurrentY - 2, ref r);

        //Left
        SpecialPieceMove(CurrentX - 1, CurrentY, ref r);
        SpecialPieceMove(CurrentX - 2, CurrentY, ref r);

        //Right
        SpecialPieceMove(CurrentX + 1, CurrentY, ref r);
        SpecialPieceMove(CurrentX + 2, CurrentY, ref r);

        return r;
    }

    public void SpecialPieceMove(int x, int y, ref bool[,]r)
    {
        Pieces c;
        if(x >= 0 && x < 6 && y >= 0 && y < 10)
        {
            c = BoardManager.Instance.PlayerPieces[x, y];
            if (c == null)
                r[x, y] = true;
            else if (isWhite != c.isWhite && c.isLeader != true)
                r[x, y] = true;
        }
    }
}
