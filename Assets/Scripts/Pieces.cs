using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pieces : MonoBehaviour
{
    public int CurrentX { set; get; }
    public int Currenty { set; get; }
    public bool isWhite;

    public void SetPosition (int x, int y)
    {
        CurrentX = x;
        Currenty = y;
    }

    public virtual bool PossibleMove(int x, int y)
    {
        return true;
    }
}
