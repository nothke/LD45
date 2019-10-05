using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twutter : MonoBehaviour
{
    List<Twut> twuts = new List<Twut>();

    void Start()
    {

    }

    Twut CreateTwut(string text)
    {
        Twut twut = new Twut()
        {
            message = text
        };

        twuts.Add(twut);
        return twut;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var tw = CreateTwut("haha! #shit #pussy");
            var tags = tw.GetTags();
            foreach (var tag in tags)
            {
                Debug.Log(tag);
            }
        }
    }
}

public class Twut
{
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