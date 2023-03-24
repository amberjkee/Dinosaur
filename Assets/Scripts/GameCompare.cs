using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCompare : MonoBehaviour
{
    [Multiline]
    public string titleText;
    public string equalHeight;
    public string equalWeight;
    public TextMeshProUGUI title;
    public RectTransform manikin;
    public RectTransform manikinBody;
    public Slider heightBar;
    public Slider weightBar;
    public TextMeshProUGUI height;
    public TextMeshProUGUI weight;
    public GameObject calculate;
    public GameObject results;
    private Card beastCard;
    private Vector3 firstTouch;
    public Vector2 beastParameters;
    private Vector2 playerParameters;

    void OnEnable()
    {
        ChangeHeight(0);
        ChangeWeight(0);
        heightBar.value = 0;
        weightBar.value = 0;
        calculate.SetActive(false);
        results.SetActive(false);
    }

    private void OnDisable()
    {
        calculate.SetActive(false);
    }

    string GetShorterName(string input)
    {
        string[] names = input.Split('\n');
        string name = names[0];
        name = name.Replace("\r", "");
        return name;
    }

    public void SetBeast(Card card)
    {
        beastCard = card;
        beastParameters = ContentLoader.GetBeastParameters(GetShorterName(card.name));
        string titlef = titleText.Replace("@", GetShorterName(card.name));
        title.text = titlef;
    }

    public void Resize()
    {
        float cs = 1080f / (float)Screen.height;
        Vector2 size = (Input.mousePosition - firstTouch) * 0.01f * cs;
        if (heightBar.value + size.y >= 0 && heightBar.value + size.y <= 1)
            heightBar.value += size.y;
        if (weightBar.value + size.x >= 0 && weightBar.value + size.x <= 1)
            weightBar.value += size.x;
        firstTouch = Input.mousePosition;
    }

    public void ChangeHeight(float value)
    {
        manikin.localScale = Vector2.one * (0.5f + value * 0.5f);
        int h = (int)(100 + value * 100);
        height.text = h.ToString() + " см";
        playerParameters.x = h;
        if (!calculate.activeInHierarchy)
        {
            calculate.SetActive(true);
            results.SetActive(false);
        }
    }

    public void ChangeWeight(float value)
    {
        manikinBody.sizeDelta = new Vector2(78 + value * 100, 140); //83~164
        int w = (int)(10 + value * 90);
        weight.text = w.ToString() + " кг";
        playerParameters.y = w;
        if (!calculate.activeInHierarchy)
        {
            calculate.SetActive(true);
            results.SetActive(false);
        }
    }

    public void Calculate()
    {
        calculate.SetActive(false);
        results.SetActive(true);
        TextMeshProUGUI height = results.transform.Find("height").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI weight = results.transform.Find("weight").GetComponent<TextMeshProUGUI>();
        string hcomp = beastParameters.x > playerParameters.x ? "больше" : "меньше";
        string wcomp = beastParameters.y > playerParameters.y ? "тяжелее" : "легче";
        float hrate = hcomp == "больше" ? beastParameters.x / playerParameters.x : playerParameters.x / beastParameters.x;
        float wrate = hcomp == "тяжелее" ? beastParameters.y / playerParameters.y : playerParameters.y / beastParameters.y;
        string name = GetShorterName(beastCard.name);
        string times = hrate < 5 ? "раза" : "раз";
        if (beastParameters.x == playerParameters.x)
            height.text = equalHeight.Replace("@", name);
        else
            height.text = name + " " + hcomp + " тебя в " + hrate + " " + times;
        times = wrate < 5 ? "раза" : "раз";
        if (beastParameters.y == playerParameters.y)
            weight.text = equalWeight.Replace("@", name);
        else
            weight.text = name + " " + wcomp + " тебя в " + wrate + " " + times;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) firstTouch = Input.mousePosition;
    }
}
