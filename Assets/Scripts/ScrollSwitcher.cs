using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSwitcher : MonoBehaviour
{
    public GameObject scrollHint;
    private ScrollRect sr;
    private RectTransform rt;
    private RectTransform target;

    void Start()
    {
        sr = GetComponent<ScrollRect>();
        rt = GetComponent<RectTransform>();
        target = transform.Find("viewport").GetChild(0).GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        float c = target.rect.height;
        float h = rt.rect.height;
        //Debug.Log(c.ToString() + ", " + h.ToString());
        if (c <= h)
        {
            if (sr && sr.vertical)
            {
                sr.vertical = false;
                if (scrollHint && scrollHint.activeInHierarchy) scrollHint.SetActive(false);
            }
        }
        else
        {
            if (sr && !sr.vertical) sr.vertical = true;
            if (scrollHint && !scrollHint.activeInHierarchy) scrollHint.SetActive(true);
        }
    }
}
