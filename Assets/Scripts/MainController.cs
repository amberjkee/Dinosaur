using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainController : MonoBehaviour
{
    public string[] sections;
    public GameObject sceneList;
    public TextMeshProUGUI listTitle;
    public GameObject listPrefab;
    public GameObject litPrefab;
    public Sprite[] listIcons;
    public Transform listHolder;
    private GameObject[] listInstances;
    public GameObject keyTest;
    public GameObject sceneView;
    public Image photo;
    public GameObject[] photoSwitchers;
    public TextMeshProUGUI title;
    public TextMeshProUGUI titleLat;
    public TextMeshProUGUI text;
    public TextMeshProUGUI plate;
    public GameObject sceneCompare;
    public GameObject scenePuzzle;
    public GameObject sceneTest;
    public GameObject keyBack;
    public GameObject keyCompare;
    public GameObject keyLit;
    private Animator animator;
    private Card[] beastCards;
    private int currentSection;
    private int currentCard;
    private int currentPhoto;


    void Start()
    {
        animator = GetComponent<Animator>();
        sceneList.SetActive(false);
        sceneView.SetActive(false);
        sceneCompare.SetActive(false);
    }

    public void SwitchSection(int sectionID)
    {
        Strelki.flague();
        if (sectionID == 4)
        {
            animator.SetInteger("Scene", 5);
            listTitle.text = sections[sectionID];
            currentSection = sectionID;
            return;
        }
        keyTest.SetActive(true);
        animator.SetInteger("Scene", 1);
        beastCards = ContentLoader.GetCards(sections[sectionID - 1]);
        listTitle.text = sections[sectionID-1];
        currentSection = sectionID;
    }

    public void SpawnCards()
    {
        
        if (animator.GetInteger("Scene") == 0) return;
        sceneList.SetActive(true);
        StartCoroutine(SpawnBeastList(beastCards));
    }

    public void ShowLiterature()
    {
        if (animator.GetInteger("Scene") == 0) return;
        sceneView.SetActive(false);
        sceneList.SetActive(true);
        SpawnLiterature();


    }

    void SpawnLiterature()
    {
        listHolder.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        if (listInstances != null && listInstances.Length > 0)
        {
            foreach (GameObject l in listInstances) Destroy(l);
        }
        listInstances = new GameObject[1]; //может ли тут быть утечка памяти?
        for (int i = 0; i < listInstances.Length; i++)
        {
            listInstances[i] = Instantiate(litPrefab);
            listInstances[i].transform.SetParent(listHolder, false);
            listInstances[i].GetComponent<TextMeshProUGUI>().text = ContentLoader.GetLiterature();

        }
        StartCoroutine(cor());
    }

    public IEnumerator cor()
    {
        yield return new WaitForSeconds(0.2f);
        Strelki.flague();

    }



    IEnumerator SpawnBeastList(Card[] c)
    {
        listHolder.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        if (listInstances != null && listInstances.Length > 0)
        {
            foreach (GameObject l in listInstances) Destroy(l);
        }
        listInstances = new GameObject[c.Length]; //может ли тут быть утечка памяти?
        for (int i = 0; i < listInstances.Length; i++)
        {
            listInstances[i] = Instantiate(listPrefab);
            listInstances[i].transform.SetParent(listHolder, false);
            string[] titles = c[i].name.Split('\n');
            listInstances[i].transform.Find("text").GetComponent<TextMeshProUGUI>().text = titles[0];
            listInstances[i].transform.Find("icon").GetComponent<Image>().sprite = listIcons[currentSection - 1];
            int act = i;
            listInstances[i].GetComponent<Button>().onClick.RemoveAllListeners();
            listInstances[i].GetComponent<Button>().onClick.AddListener(delegate { ShowBeast(act); Strelki.flague(); }); 
            yield return new WaitForSeconds(0.05f);
        }
        Strelki.flague();
    }

    public void ShowBeast(int cardID)
    {
        sceneList.SetActive(false);
        sceneView.SetActive(true);
        keyTest.SetActive(false);
        keyBack.SetActive(true);
        currentPhoto = 0;
        currentCard = cardID;
        string[] titles = beastCards[cardID].name.Split('\n');
        title.text = titles[0];
        if (titles.Length > 1)
        {
            titleLat.text = titles[1];
        }
        text.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        text.text = beastCards[cardID].article;
        if (beastCards[cardID].imgpaths != null && beastCards.Length > 0)
        {
            photo.sprite = ContentLoader.LoadPhoto(beastCards[cardID].imgpaths[0]);
            plate.text = ContentLoader.GetPhotoDescription(beastCards[cardID].imgpaths[0]);
            if (beastCards[cardID].imgpaths.Length < 2)
            {
                foreach (GameObject s in photoSwitchers) s.SetActive(false);
            } else
            {
                foreach (GameObject s in photoSwitchers) s.SetActive(true);
            }
        }
        string bn = titles[0].Replace("\r", "");
        if (ContentLoader.GetBeastParameters(bn) == Vector2.zero) keyCompare.SetActive(false);
        else keyCompare.SetActive(true);
        keyLit.SetActive(true);
        animator.SetInteger("Scene", 2);
    }

    public void SwitchPhoto(bool forth)
    {
        if (forth)
        {
            if (currentPhoto < beastCards[currentCard].imgpaths.Length - 1) currentPhoto++;
            else currentPhoto = 0;
        }
        else
        {
            if (currentPhoto > 0) currentPhoto--;
            else currentPhoto = beastCards[currentCard].imgpaths.Length - 1;
        }
        if (beastCards[currentCard].imgpaths.Length > 0 && beastCards[currentCard].imgpaths[currentPhoto] != "")
        {
            photo.sprite = ContentLoader.LoadPhoto(beastCards[currentCard].imgpaths[currentPhoto]);
            plate.text = ContentLoader.GetPhotoDescription(beastCards[currentCard].imgpaths[currentPhoto]);
        }
    }

    public void PlayCompare()
    {
        sceneView.SetActive(false);
        sceneCompare.SetActive(true);
        keyLit.SetActive(false);
        sceneCompare.GetComponent<GameCompare>().SetBeast(beastCards[currentCard]);
        animator.SetInteger("Scene", 3);
    }

    public void PlayPuzzle()
    {
        sceneView.SetActive(false);
        scenePuzzle.SetActive(true);
        keyLit.SetActive(false);
        scenePuzzle.GetComponent<GameJigsaw>().ShowCard(beastCards[currentCard]);
        //sceneCompare.GetComponent<GameCompare>().SetBeast(beastCards[currentCard]);
        animator.SetInteger("Scene", 4);
    }


    public void ToLiterature()
    {
        Strelki.flague();
        listTitle.text = sections[sections.Length -1];
        ShowLiterature();
        sceneView.SetActive(false);
        keyBack.SetActive(false);
        keyLit.SetActive(false);
        //sceneCompare.GetComponent<GameCompare>().SetBeast(beastCards[currentCard]);
        animator.SetInteger("Scene", 5);
    }


    public void PlayTest()
    {
        sceneList.SetActive(false);
        sceneView.SetActive(false);
        sceneTest.SetActive(true);
        keyTest.SetActive(false);
        keyBack.SetActive(true);
        keyLit.SetActive(false);
        sceneTest.GetComponent<GameEdible>().ShowCard(currentSection - 1);
        animator.SetInteger("Scene", 6);
    }

    public void Home()
    {
        Strelki.flague();
        sceneList.SetActive(false);
        sceneView.SetActive(false);
        sceneCompare.SetActive(false);
        scenePuzzle.SetActive(false);
        sceneTest.SetActive(false);
        //keyTest.SetActive(false);
        keyBack.SetActive(false);
        keyLit.SetActive(false);
        if (listInstances != null && listInstances.Length > 0)
        {
            foreach (GameObject l in listInstances) Destroy(l);
        }
        animator.SetInteger("Scene", 0);
    }

    public void Return()
    {
        Strelki.flague();
        keyLit.SetActive(false);
        if (animator.GetInteger("Scene") == 2)
        {
            animator.SetInteger("Scene", 1);
            sceneView.SetActive(false);
            sceneList.SetActive(true);
            keyTest.SetActive(true);
            keyBack.SetActive(false);
            
        } else if (animator.GetInteger("Scene") == 3)
        {
            animator.SetInteger("Scene", 2);
            sceneCompare.SetActive(false);
            sceneView.SetActive(true);
            keyLit.SetActive(true);
        }
        else if (animator.GetInteger("Scene") == 4)
        {
            animator.SetInteger("Scene", 2);
            scenePuzzle.SetActive(false);
            sceneView.SetActive(true);
            keyLit.SetActive(true);
        }
        else if (animator.GetInteger("Scene") == 6)
        {
            animator.SetInteger("Scene", 1);
            sceneTest.SetActive(false);
            sceneList.SetActive(true);
            keyTest.SetActive(true);
            keyBack.SetActive(false);
        }
    }

    void Update()
    {
        
    }
}
