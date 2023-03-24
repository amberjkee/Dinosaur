using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System;

[System.Serializable]
public class Card
{
    public string name;
    public string[] imgpaths;
    public string article;
}

[System.Serializable]
public class GalleryCard
{
    public string name;
    public string path;
}

public static class ContentLoader
{
    public static string path = Path.GetDirectoryName(Application.dataPath) + @"\content";

    public static Card GetCard(string p)
    {
        Card c = new Card();
        p = path + "\\" + p;
        c.name = Path.GetFileName(p);
        c.imgpaths = Directory.GetFiles(p, "*.png");
        if (File.Exists(p + "\\" + "name.txt")) c.name = File.ReadAllText(p + "\\" + "name.txt");
        if (File.Exists(p + "\\" + "text.txt")) c.article = File.ReadAllText(p + "\\" + "text.txt");
        return c;
    }

    public static Card[] GetCards(string p)
    {
        string cardsPath = path + "\\" + p;
        string[] c = Directory.GetDirectories(cardsPath);
        Card[] cards = new Card[c.Length];
        for (int i = 0; i < c.Length; i++) cards[i] = GetCard(p + "\\" + Path.GetFileName(c[i]));
        return cards;
    }

    public static string[] GetCardPathes(string p)
    {
        string cardPath = path + "\\" + p;
        return Directory.GetDirectories(cardPath);
    }

    public static Vector2 GetBeastParameters(string name)
    {
        string p = path + "\\Игры\\" + name;
        Vector2 param = Vector2.zero;
        if (!Directory.Exists(p)) return param;
        string t = "";
        string[] txts = Directory.GetFiles(p, "*.txt");
        if (txts.Length > 0) t = File.ReadAllText(txts[0]);
        if (t != "")
        {
            string[] par;
            par = t.Split('\n');
            if (par != null && par.Length > 1)
            {
                param.x = (float)float.Parse(Regex.Replace(par[0], "[^0-9+,0-9+]", ""));
                param.y = (float)float.Parse(Regex.Replace(par[1], "[^0-9+,0-9+]", ""));
            }
        }
        return param;
    }

    public static Sprite LoadPhoto(string p)
    {
        Sprite img = null;
        try
        {
            byte[] imgraw = File.ReadAllBytes(p);
            Texture2D imgtex = new Texture2D(1000, 1000);
            imgtex.LoadImage(imgraw);
            
            img = Sprite.Create(imgtex, new Rect(0, 0, imgtex.width, imgtex.height), Vector2.zero);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.ToString());
        }
        return img;
    }


    public static string GetVideo()
    {
        string p = Path.GetDirectoryName(Application.dataPath) + @"/content";
        string[] videos = Directory.GetFiles(p, "*.mp4");
        if (videos.Length > 0)
            return videos[0];
        else return "";
    }

    public static string GetPhotoDescription(string photo)
    {
        string localpath = Path.GetDirectoryName(photo);
        string file = Path.GetFileNameWithoutExtension(photo);
        if (File.Exists(localpath + "\\" + file + ".txt"))
            return File.ReadAllText(localpath + "\\" + file + ".txt");
        return "";
    }

    public static string GetLiterature()
    {
        return File.ReadAllText(path + "\\" + "literature.txt");
    }

    public static string[] LoadPieces(string name)
    {
        string[] files = Directory.GetFiles(path + "\\Игры\\" + name, "*.png");
        string[] result = new string[25];
        //int counter = 0;
        //foreach (var s in files)
        //{
        //    Debug.Log(s + "  "+ Convert.ToString(counter));
        //    counter++;
        //}
        try
        {
            result[0] = files[13];
            result[1] = files[9];
            result[2] = files[11];
            result[3] = files[21];
            result[4] = files[10];
            result[5] = files[7];
            result[6] = files[15];
            result[7] = files[1];
            result[8] = files[19];
            result[9] = files[17];
            result[10] = files[6];
            result[11] = files[20];
            result[12] = files[3];
            result[13] = files[12];
            result[14] = files[22];
            result[15] = files[0];
            result[16] = files[23];
            result[17] = files[14];
            result[18] = files[2];
            result[19] = files[16];
            result[20] = files[8];
            result[21] = files[4];
            result[22] = files[18];
            result[23] = files[5];
            result[24] = files[24];
            return result;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}
