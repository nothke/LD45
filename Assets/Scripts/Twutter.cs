using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Twutter : MonoBehaviour
{
    public static Twutter e;
    void Awake() { e = this; }

    List<Profile> profiles = new List<Profile>();
    List<Twut> twuts = new List<Twut>();

    public RectTransform feedTransform;
    public GameObject twutCellPrefab;

    public Sprite meSprite;
    public Sprite[] avatars;

    Profile me;

    void Start()
    {
        me = new Profile()
        {
            avatarSprite = meSprite,
            name = "Me"
        };

        // Create random animals
        for (int i = 0; i < 10; i++)
        {
            Profile p = new Profile()
            {
                avatarSprite = avatars[Random.Range(0, avatars.Length)],
                name = names[i]
            };
            profiles.Add(p);
        }

        StartCoroutine(RandomTweets());
    }

    IEnumerator RandomTweets()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));
            CreateRandomTwut();
        }
    }

    Twut CreateTwut(Profile from, string text)
    {
        Twut twut = new Twut()
        {
            author = from,
            message = text
        };

        twuts.Add(twut);
        return twut;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Profile randomProfile = profiles[Random.Range(0, profiles.Count)];
            Debug.Log("Pus");

            string twutText = "haha! #shit #pussy";
            var tw = CreateTwut(randomProfile, twutText);
            var tags = tw.GetTags();

            foreach (var tag in tags)
            {
                Debug.Log(tag);
            }

            SpawnTwut(tw);
        }
    }

    void CreateRandomTwut()
    {
        Profile randomProfile = profiles[Random.Range(0, profiles.Count)];
        string twutText = tweetTexts[Random.Range(0, tweetTexts.Length)];
        var tw = CreateTwut(randomProfile, twutText);
        var tags = tw.GetTags();

        foreach (var tag in tags)
        {
            Debug.Log(tag);
        }

        SpawnTwut(tw);
    }

    public void TwutFromMe(string twutText)
    {
        var tw = CreateTwut(me, twutText);
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
        cell.avatar.texture = twut.author.avatarSprite.texture;
        cell.nick.text = twut.author.name;
        cell.text.text = twut.message;
    }

    const string FILENAME = "Lore.txt";

    string[] tweetTexts = new string[]
    {
        "New Song Coming Out Soon. #DJCaz",
        "I like big nips and I can not lie.",
        "Progressing at a steady peace. #ldjam",
        "Went to History Museum. I still dont believe we were made from tigers. #YouLearnEveryday",
        "Aristocats was a good movie. #CantChangeMyMind",
        "Starting with #ldjam. Wish me luck <3.",
        "MewMazing. #GoodDay",
        "Love for everyone <3. #Equality",
        "Cats cant stop interupting with their stupid mewoing. They just blab a lot. #TigerCats",
        "Meerkats are cats too! #CatEquality",
        //"Dogs should rule. We are the dominant species #DogsRule"
    };

    string[] names = new string[]
    {
        "Dogge54",
        "ApocatlipseNow3",
        "Goodboi98",
        "Bud9000",
        "Kewl",
        "max343",
        "fetch_dis34",
        "iPOOP",
        "dooogooo",
        "K9isOK"
    };

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
    public Profile author;
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

public class Profile
{
    public string name;
    public Sprite avatarSprite;
}