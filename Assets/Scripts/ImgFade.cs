using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImgFade : MonoBehaviour
{
    private Image img;
    private GameObject txt;
    public bool fade;
    public bool isWrong;

    void Awake()
    {
        img = GetComponent<Image>();
        txt = transform.GetChild(0).gameObject;
        //transform.GetChild(0).localPosition = new Vector3(0, 0, 0);
        txt.SetActive(false);
    }

    public void Fade(bool flag)
    {
        fade = true;
        isWrong = flag;
    }

    public void SetOpaque()
    {
        fade = false;
        img.color = new Color(1, 1, 1, 1);
        img.raycastTarget = true;
        txt.SetActive(false);
    }

    void Update()
    {
        if (fade)
        {

            if (img.color.a <= 0.6f) fade = false;
            else
            {
                if (!isWrong) img.color = new Color(1, 1, 1, Mathf.Lerp(img.color.a, 0.6f, Time.deltaTime * 10));
                else img.color = new Color(1, Mathf.Lerp(img.color.g, 0.6f, Time.deltaTime * 10), Mathf.Lerp(img.color.b, 0.6f, Time.deltaTime * 10), Mathf.Lerp(img.color.a, 0.5f, Time.deltaTime * 10));
            }
            txt.SetActive(true);
            if (img.raycastTarget) img.raycastTarget = false;
        }        
    }
}
