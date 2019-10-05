using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Twutter : MonoBehaviour
{
    public static Twutter e;
    void Awake() { e = this; }

    List<Twut> twuts = new List<Twut>();

    public RectTransform feedTransform;
    public GameObject twutCellPrefab;

    void Start()
    {

    }

    Twut CreateTwut(string author, string text)
    {
        Twut twut = new Twut()
        {
            author = author,
            message = text
        };

        twuts.Add(twut);
        return twut;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string twutText = "haha! #shit #pussy";
            var tw = CreateTwut("Catie", twutText);
            var tags = tw.GetTags();

            foreach (var tag in tags)
            {
                Debug.Log(tag);
            }

            SpawnTwut(tw);
        }
    }

    public void TwutFromMe(string twutText)
    {
        var tw = CreateTwut("Me", twutText);
        var tags = tw.GetTags();

        foreach (var tag in tags)
        {
            Debug.Log(tag);
        }

        SpawnTwut(tw);
    }

    void SpawnTwut(Twut twut)
    {
        GameObject go = Instantiate(twutCellPrefab);
        TwutCell cell = go.GetComponent<TwutCell>();
        cell.rectT.SetParent(feedTransform);
        cell.rectT.SetAsFirstSibling();
        // TODO: Set picture
        cell.nick.text = twut.author;
        cell.text.text = twut.message;
    }

    const string FILENAME = "Lore.txt";

    List<string> tweets = new List<string>();
    List<string> replies = new List<string>();

    public void IO()
    {
        string[] lines = File.ReadAllLines(FILENAME);
        foreach (var line in lines)
        {

            Debug.Log(line);
        }
    }
}

public class Twut
{
    public string author;
    public string message;

    public List<string> GetTags()
    {
        List<string> tags = new List<string>();

        string[] splits = message.Split(' ');

        for (int i = 0; i < splits.Length; i++)
        {
            if (splits[i][0] == '#')
            {
                tags.Add(splits[i].Substring(1, splits[i].Length - 1));
            }
        }

        return tags;
    }
}