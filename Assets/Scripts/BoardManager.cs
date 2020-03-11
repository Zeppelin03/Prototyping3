using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { set; get; }
    private bool[,] allowedMoves{ set; get; }

    public Pieces[,] PlayerPieces   { set; get; }
    private Pieces selectedPieces;
    public Pieces b;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> piecesPrefabs;
    private List<GameObject> activePieces;

    public bool isWhiteTurn = true;

    public float specialMove;

    public bool LeaderKilledBase;
    public bool LeaderKilledBaseWhite;
    public bool LeaderKilledSpecial;
    public bool LeaderKilledSpecialWhite;

    private void Start()
    {
        Instance = this;
        SpawnAllPieces();
        LeaderKilledBase = false;
        LeaderKilledSpecial = false;
    }

    private void Update() 
    {
        UpdateSelection();
        Drawboard();

        if(Input.GetMouseButtonDown(0))
        {
            b = selectedPieces;

            if(selectionX >= 0 && selectionY >= 0)
            {
                if(selectedPieces == null)
                {
                    //Select a piece
                    SelectPiece(selectionX , selectionY);
                }
                else
                {
                    //Move the piece
                    MovePiece(selectionX, selectionY);

                }
            }
        }
    }

    private void SelectPiece(int x, int y)
    {
        if (PlayerPieces [x , y] == null)
            return;

        if (PlayerPieces [x , y].isWhite != isWhiteTurn)
            return;

        bool hasAtlestOneMove = false;
        allowedMoves = PlayerPieces[x,y].PossibleMove();
        for (int i = 0; i < 6; i++)
            for (int j = 0; j < 10; j++)
                if (allowedMoves[i, j])
                    hasAtlestOneMove = true;

        if (!hasAtlestOneMove)
            return;

        selectedPieces = PlayerPieces[x, y];
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
    }

    private void MovePiece(int x, int y)
    {
        if (allowedMoves[x, y])
        {
            Pieces c = PlayerPieces[x, y];

            if (c != null && c.isWhite != isWhiteTurn)
            {
                //Capture a piece

                //If it is Leader
                if (c.GetType() == typeof(Leader) && selectedPieces.GetType() == typeof(Leader))
                {
                    EndGame();
                    return;
                }

                if (selectedPieces.GetType() == typeof(Leader))
                {
                    if (c.GetType() == typeof(BasePiece))
                    {
                        LeaderKilledBase = true;
                    }
                    if (c.GetType() == typeof(SpecialPiece))
                    {
                        LeaderKilledSpecial = true;
                    }
                }
                if (selectedPieces.GetType() == typeof(Leader) && isWhiteTurn)
                {
                    if (c.GetType() == typeof(BasePiece))
                    {
                        LeaderKilledBaseWhite = true;
                        LeaderKilledBase = false;
                    }
                    if (c.GetType() == typeof(SpecialPiece))
                    {
                        LeaderKilledSpecialWhite = true;
                        LeaderKilledSpecial = false;
                    }
                    else if (c.isWhite == true)
                    {
                        SpawnPieces(3, 2, 1);
                    }
                    else
                    {
                        SpawnPieces(0, 3, 8);
                    }
                    Destroy(c.gameObject);
                }
                else if (c.isWhite == true)
                {
                    SpawnPieces(3, 2, 1);
                }
                else
                {
                    SpawnPieces(0, 3, 8);
                }
                Destroy(c.gameObject);
            }

            PlayerPieces [selectedPieces.CurrentX, selectedPieces.CurrentY] = null;
            selectedPieces.transform.position = GetTileCenter(x, y);
            selectedPieces.SetPosition(x, y);
            PlayerPieces[x, y] = selectedPieces;
            isWhiteTurn = !isWhiteTurn;
        }

        BoardHighlights.Instance.Hidehighlights();
        selectedPieces = null;
    }

    private void UpdateSelection()
    {
        if (!Camera.main)
            return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f, LayerMask.GetMask("BoardPlane")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }

    private void SpawnPieces(int index, int x, int y )
    {
        GameObject go = Instantiate(piecesPrefabs[index], GetTileCenter(x,y) , Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);
        PlayerPieces[x,y] = go.GetComponent<Pieces>();
        PlayerPieces[x, y].SetPosition(x, y);
        activePieces.Add(go);
    }

    private void SpawnAllPieces()
    {
        activePieces = new List<GameObject>();
        PlayerPieces = new Pieces[6, 10];

        //Spawn the white team.

        //Leader
        SpawnPieces(3, 2, 1);

        //Special
        SpawnPieces(4, 3, 1);

        //Base 1
        SpawnPieces(5, 1, 0);

        //Base 2
        SpawnPieces(5, 4, 0);


        //Spawn the black team

        //Leader
        SpawnPieces(0, 3, 8);

        //Special
        SpawnPieces(1, 2, 8);

        //Base 1
        SpawnPieces(2, 1, 9);

        //Base 2
        SpawnPieces(2, 4, 9);
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }

    private void Drawboard ()
    {
        Vector3 widthLine = Vector3.right * 6;
        Vector3 heightLine = Vector3.forward * 10;

        for(int i = 0; i <= 10; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for(int j = 0; j <= 6; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heightLine);
            }
        }

        //Draw the selection
        if(selectionX >= 0 && selectionY >=0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

            Debug.DrawLine(
                Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
                Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
        }
    }

    private void EndGame()
    {
        if (isWhiteTurn)
            Debug.Log("White team wins");
        else
            Debug.Log("Black team wins");

        foreach (GameObject go in activePieces)
            Destroy(go);

        LeaderKilledBase = false; ;
        LeaderKilledBaseWhite = false;
        LeaderKilledSpecial = false;
        LeaderKilledSpecialWhite = false;

    isWhiteTurn = true;
        BoardHighlights.Instance.Hidehighlights();
        SpawnAllPieces();
    }
}