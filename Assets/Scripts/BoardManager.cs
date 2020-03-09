using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Pieces[,] PlayerPieces   { set; get; }
    private Pieces selectedPieces;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> piecesPrefabs;
    private List<GameObject> activePieces;

    public bool isWhiteTurn = true;

    private void Start()
    {
        SpawnAllPieces();
    }

    private void Update() 
    {
        UpdateSelection();
        Drawboard();

        if(Input.GetMouseButtonDown(0))
        {
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

        selectedPieces = PlayerPieces[x, y];
    }

    private void MovePiece(int x, int y)
    {
        if(selectedPieces.PossibleMove(x, y))
        {
            PlayerPieces [selectedPieces.CurrentX, selectedPieces.Currenty] = null;
            selectedPieces.transform.position = GetTileCenter(x, y);
            PlayerPieces[x, y] = selectedPieces;
            isWhiteTurn = !isWhiteTurn;
        }

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
        PlayerPieces = new Pieces[6, 11];

        //Spawn the black team.

        //Leader
        SpawnPieces(0, 2, 1);

        //Special
        SpawnPieces(1, 3, 1);

        //Base 1
        SpawnPieces(2, 1, 0);

        //Base 2
        SpawnPieces(2, 4, 0);


        //Spawn the white team

        //Leader
        SpawnPieces(3, 3, 8);

        //Special
        SpawnPieces(4, 2, 8);

        //Base 1
        SpawnPieces(5, 1, 9);

        //Base 2
        SpawnPieces(5, 4, 9);
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
}