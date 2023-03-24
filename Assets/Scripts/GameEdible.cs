using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dish
{
    public GameObject dish;
    [Multiline]
    public string description;
    public bool edible;
}

public class GameEdible : MonoBehaviour
{
    public GameObject game;
    public Text hint;
    public Dish[] dishes;
    public Dish[] dishes2;
    public Dish[] dishes3;
    public GameObject hold1;
    public GameObject hold2;
    public GameObject hold3;
    [Multiline]
    public string welcomeTask;
    public GameObject result;
    [Multiline]
    public string resultText;
    public int left = 6;
    public int currentScore;
    private int currentSection;
    public Text zad;

    void Start()
    {

        
    }

    public void ShowCard(int section)
    {
        
        //print(section);
        currentSection = section;
        game.SetActive(true);
        zad.gameObject.SetActive(true);
        string txt = zad.text;
        switch (section) 
        {
            case 0:
                //print(1);
                zad.text = txt.Replace("!", "летающие насекомые");
                txt = zad.text;
                zad.text = txt.Replace("#", "двое");
                hold1.SetActive(true);
                hold2.SetActive(false);
                hold3.SetActive(false);
                left = 2;
                break;
            case 1:
                //print(2);
                zad.text = txt.Replace("!", "наземные животные");
                txt = zad.text;
                zad.text = txt.Replace("#", "трое");
                hold1.SetActive(false);
                hold2.SetActive(true);
                hold3.SetActive(false);
                left = 3;
                break;
            case 2:
                // print(3);
                zad.text = txt.Replace("!", "полуводные животные");
                txt = zad.text;
                zad.text = txt.Replace("#", "трое");
                hold1.SetActive(false);
                hold2.SetActive(false);
                hold3.SetActive(true);
                left = 3;
                break;
            default:
                //print(4);
                hold1.SetActive(false);
                hold2.SetActive(false);
                hold3.SetActive(false); break;

        }
        
    }

    private void OnDisable()
    {
        zad.text = welcomeTask;
        HideCard();
    }
    public void HideCard()
    {

        CancelInvoke();
            foreach (Dish d in dishes)
            {
                d.dish.GetComponent<ImgFade>().SetOpaque();
            }
            foreach (Dish d in dishes2)
            {
                d.dish.GetComponent<ImgFade>().SetOpaque();
            }
            foreach (Dish d in dishes3)
            {
                d.dish.GetComponent<ImgFade>().SetOpaque();
            }
            game.SetActive(false);
            hold1.SetActive(false);
            hold2.SetActive(false);
            hold3.SetActive(false);
            result.SetActive(false);
            currentScore = 0;
            left = 6;

    }

    void ShowResult()
    {
        game.SetActive(false);
        result.SetActive(true);
        zad.gameObject.SetActive(false);
        //string[] res = resultText.Split(';');
        //string resoveral = res[0];
        //resoveral = resoveral.Replace("#", currentScore.ToString());
        //if ((float)currentScore / 6 == 1) resoveral += "\n" + res[1];
        //else if ((float)currentScore / 6 >= 0.5f) resoveral += "\n" + res[2];
        //else resoveral += "\n" + res[3];
        



        switch (currentSection)
        {
            case 0:
                if (currentScore == 0) result.transform.Find("Text").GetComponent<Text>().text = "Ты отлично знаешь летающих насекомых пермского периода!";
                else result.transform.Find("Text").GetComponent<Text>().text = "Ознакомься с летающими насекомыми пермского периода и попробуй еще раз!";
                break;
            case 1:
                if (currentScore == 0) result.transform.Find("Text").GetComponent<Text>().text = "Ты отлично знаешь наземных животных пермского периода!";
                else result.transform.Find("Text").GetComponent<Text>().text = "Ознакомься с наземными животными пермского периода и попробуй еще раз!";
                break;
            case 2:
                if (currentScore == 0) result.transform.Find("Text").GetComponent<Text>().text = "Ты отлично знаешь полуводных животных пермского периода!";
                else result.transform.Find("Text").GetComponent<Text>().text = "Ознакомься с полуводными животными пермского периода и попробуй еще раз!";
                break;
            default: return;

        }

    }

    public void Pick(int dish)
    {

        if (left <= 0) return;
        switch (currentSection)
        {
            case 0:
                dishes[dish].dish.GetComponent<ImgFade>().Fade(dishes[dish].edible);

                if (!dishes[dish].edible) 
                {
                    left--;
                    if (left <= 0)
                    {
                        Invoke("ShowResult", 1);
                    }
                }
                else currentScore++;
                break;
            case 1:
                dishes2[dish].dish.GetComponent<ImgFade>().Fade(dishes2[dish].edible);
                
                if (!dishes2[dish].edible) 

                {
                    left--;
                    if (left <= 0)
                    {
                        Invoke("ShowResult", 1);
                    }
                }
                else currentScore++;
                break;
            case 2:
                dishes3[dish].dish.GetComponent<ImgFade>().Fade(dishes3[dish].edible);

                if (!dishes3[dish].edible) 

                {
                    left--;
                    if (left <= 0)
                    {
                        Invoke("ShowResult", 1);
                    }
                }
                else currentScore++;
                break;
            default: return;

        }


        //string leftstr = "";
        //if (left > 0)
        //{
        //    if (left == 5) leftstr = "5 блюд.";
        //    else if (left > 1 && left < 5) leftstr = left.ToString() + " блюда.";
        //    else leftstr = "1 блюдо.";
        //    leftstr = "\nОсталось еще " + leftstr;
        //}
        //else
        //{
        //    Invoke("ShowResult", 2);
        //}
        //switch (currentSection)
        //{
        //    case 0:
        //        hint.text = dishes[dish].description + leftstr;
        //        break;
        //    case 1:
        //        hint.text = dishes2[dish].description + leftstr;
        //        break;
        //    case 2:
        //        hint.text = dishes3[dish].description + leftstr;
        //        break;
        //    default: return;

        //}

    }
}
