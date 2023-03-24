using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Strelki : MonoBehaviour
{
    public GameObject[] strelki;
    public RectTransform content;
    public Image raycast;
    public static bool flag =true;
    public static bool flag2 = true;
    public bool isHorizontal = false;

    public static void flague()
    {
        flag = true;
        flag2 = true;
    }
    private void FixedUpdate()
    {
        if (flag)
        {
            if (!isHorizontal)
            {
                if (gameObject.GetComponent<RectTransform>().rect.height > content.rect.height)
                {
                    strelki[0].SetActive(false);
                    strelki[1].SetActive(true);
                    raycast.raycastTarget = true;
                }
                else
                {
                    foreach (var s in strelki)
                    {
                        s.SetActive(false);

                    }
                    raycast.raycastTarget = false;
                }
                flag = false;
            }

            
        }
        if (flag2)
        {
            if (isHorizontal)
            {

                if (gameObject.GetComponent<RectTransform>().rect.width > content.rect.width)
                {
                    strelki[1].SetActive(false);
                    strelki[0].SetActive(true);
                    raycast.raycastTarget = true;
                }
                else
                {
                    foreach (var s in strelki)
                    {
                        s.SetActive(false);

                    }
                    raycast.raycastTarget = false;
                }
                flag2 = false;
            }
            //print(gameObject.GetComponent<RectTransform>().rect.width);
            //print(content.rect.width);
            //print(gameObject.name);
        }
    }
    public void HideStrelki(Vector2 vec)
    {
        if (!isHorizontal)
        {
            if (vec.y > 0.85f)
            {
                strelki[0].SetActive(false);

            }
            if (vec.y > 0.15f && vec.y < 0.85f)
            {
                strelki[0].SetActive(true);
                strelki[1].SetActive(true);
            }
            if (vec.y < 0.15f)
            {
                strelki[1].SetActive(false);
            }

        }
        else
        {
            if (vec.x > 0.85f)
            {
                strelki[0].SetActive(false);

            }
            if (vec.x > 0.15f && vec.x < 0.85f)
            {
                strelki[0].SetActive(true);
                strelki[1].SetActive(true);
            }
            if (vec.x < 0.15f)
            {
                strelki[1].SetActive(false);
            }
        }
    }
    //private void OnEnable()
    //{
    //    if (gameObject.GetComponent<RectTransform>().rect.height > content.rect.height)
    //    {
    //        foreach (var s in strelki)
    //        {
    //            s.SetActive(true);
    //        }
    //        raycast.raycastTarget = true;
    //    }
    //    else 
    //    {
    //        foreach (var s in strelki)
    //        {
    //            s.SetActive(false);
                
    //        }
    //        raycast.raycastTarget = false;
    //    }

    //}

    //private void OnDisable()
    //{
    //    foreach (var s in strelki)
    //    {
    //        s.SetActive(true);
           
    //    }
    //    raycast.raycastTarget = true;
    //}
}
