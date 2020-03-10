using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlights : MonoBehaviour
{
    public static BoardHighlights Instance { set; get; }

    public GameObject highlightPrefab;
    private List<GameObject> highlights;

    private void Start()
    {
        Instance = this;
        highlights = new List<GameObject>();
    }

    private GameObject GetHighlighObject()
    {
        GameObject go = highlights.Find(g => !gameObject.activeSelf);

        if(go == null)
        {
            go = Instantiate(highlightPrefab);
            highlights.Add (go);
        }

        return go;
    }

    public void HighlightAllowedMoves(bool[,]moves)
    {
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                if(moves[i,j])
                {
                    GameObject go = GetHighlighObject();
                    go.SetActive(true);
                    go.transform.position = new Vector3(i + 0.5f, 0, j + 0.5f);
                }
            }
        }
    }

    public void Hidehighlights()
    {
        foreach (GameObject go in highlights)
            go.SetActive(false);
    }
}
