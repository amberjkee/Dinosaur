using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[System.Serializable]
public class JigsawPiece
{
    public GameObject piece;
    public GameObject cell;
    public bool placed;
}

public class GameJigsaw : MonoBehaviour
{
    public GameObject game;
    public Transform board;
    public JigsawPiece[] pieces;
    private Vector3 firstTouch;
    private int selectedObject;
    public Transform grid;
    public GameObject result;
    private Vector2[] pieceOrigin;
    private int placed;

    private Card beastCard;
    public Vector2 beastParameters;
    public string titleText;
    public Text title;
    public Image image;
    private string origText;

    void Start()
    {
        
        pieceOrigin = new Vector2[pieces.Length];
        for (int i = 0; i < pieces.Length; i++)
        {
            pieceOrigin[i] = pieces[i].piece.GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public void ShowCard(Card card)
    {
        origText = title.text;

        SetBeast(card);
        DrawPieces(card);
        game.SetActive(true);
        title.gameObject.SetActive(true);
    }


    private void OnDisable()
    {
        HideCard();
    }
    public void HideCard()
    {

            for (int m = 0; m < pieces.Length; m++)
            {
                pieces[m].piece.transform.SetParent(board, false);
                pieces[m].piece.GetComponent<RectTransform>().anchoredPosition = pieceOrigin[m];
                pieces[m].placed = false;
                //matches[m].match.SetActive(true);
            }
            result.SetActive(false);
            title.gameObject.SetActive(true);
            placed = 0;
            selectedObject = 0;
            title.text = origText;
            game.SetActive(false);

    }

    public void Select(int item)
    {
        if (pieces[item].placed) return;
        if (selectedObject - 1 != item)
        {
            selectedObject = item + 1;
            //foreach (JigsawPiece p in pieces) p.piece.GetComponent<Image>().raycastTarget = false;
            pieces[item].piece.GetComponent<Image>().raycastTarget = false;
            pieces[item].piece.transform.SetParent(game.transform, false);
            pieces[item].piece.transform.SetParent(board, false);
            pieces[item].piece.GetComponent<Shadow>().effectDistance = new Vector2(4, -4);
        }
    }

    public void Drag()
    {
        if (selectedObject == 0 || pieces[selectedObject-1].placed) return;
        float cs = 1080f / (float)Screen.height;
        Vector2 pos = pieces[selectedObject-1].piece.transform.GetComponent<RectTransform>().anchoredPosition;
        Vector2 _drag = (firstTouch - Input.mousePosition) * cs;
        pos -= _drag;
        pieces[selectedObject-1].piece.transform.GetComponent<RectTransform>().anchoredPosition = pos;
        firstTouch = Input.mousePosition;
    }

    public void Place(int cell)
    {
        if (cell != selectedObject - 1) return;
        pieces[selectedObject-1].piece.transform.position = pieces[selectedObject-1].cell.transform.position;
        pieces[selectedObject-1].placed = true;
        pieces[selectedObject-1].piece.transform.SetParent(grid, false);
        pieces[selectedObject-1].piece.GetComponent<Shadow>().effectDistance = new Vector2(0, 0);
        placed++;
        if (placed == pieces.Length)
        {
            result.SetActive(true);
            title.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) firstTouch = Input.mousePosition;
        if (Input.GetKeyUp(KeyCode.Mouse0) && selectedObject != 0)
        {
            foreach (JigsawPiece p in pieces) p.piece.GetComponent<Image>().raycastTarget = true;
            pieces[selectedObject-1].piece.GetComponent<Shadow>().effectDistance = new Vector2(2, -2);
            selectedObject = 0;
        }
    }

    public void DrawPieces(Card card)
    {
        try
        {
            string[] files = ContentLoader.LoadPieces(GetShorterName(card.name));
            for (int i = 0; i < 24; i++)
            {
                pieces[i].piece.GetComponent<Image>().sprite = ContentLoader.LoadPhoto(files[i]);

            }
;
            image.sprite = ContentLoader.LoadPhoto(files[24]);

        }
        catch (Exception e)
        {
            return;
        }
    }


    string GetShorterName(string input)
    {
        string[] names = input.Split('\n');
        string name = names[0];
        name = name.Replace("\r", "");
        names = name.Split(' ');
        name = names[0];
        return name;
    }

    public void SetBeast(Card card)
    {
        beastCard = card;
        //beastParameters = ContentLoader.GetBeastParameters(GetShorterName(card.name));
        string titlef = titleText.Replace("@", GetShorterName(card.name));
        title.text = titlef;
    }
}
