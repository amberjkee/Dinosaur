using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TMPSize : MonoBehaviour
{
    ContentSizeFitter fitter;
    private void OnEnable()
    {
        fitter = GetComponent<ContentSizeFitter>();
        StartCoroutine(XYU());

    }

    private void OnDisable()
    {
        fitter.enabled = true;

    }

    IEnumerator XYU()
    {
        yield return new WaitForEndOfFrame();
        fitter.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
