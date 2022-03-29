using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MemoryCard : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI hintText;

    TextAsset asset;
    XMLSettings UIelement;

    GameObject cardsPanel;
    GameObject openCard;
    AudioSource clickSound;
    GameObject[] cards;
    Sprite[] cardFaces;
    int foundСards;
    GameManager game;

    public GameObject OpenCard { get => openCard; set => openCard = value; }
    public int FoundСards { get => foundСards; set => foundСards = value; }

    private void Awake()
    {
        game = FindObjectOfType<GameManager>();
        cardsPanel = transform.GetChild(1).gameObject;
        cardFaces = new Sprite[6];
        if (game.LevelNum == 9)
        {
            cardFaces[0] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 9")[0];
            cardFaces[1] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 9")[3];
            cardFaces[2] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 9")[4];
            cardFaces[3] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 9")[5];
            cardFaces[4] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 9")[6];
            cardFaces[5] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 9")[7];
        }
        else if (game.LevelNum == 18)
        {
            cardFaces[0] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 18")[0];
            cardFaces[1] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 18")[1];
            cardFaces[2] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 18")[4];
            cardFaces[3] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 18")[5];
            cardFaces[4] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 18")[6];
            cardFaces[5] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 18")[2];
        }
        else if (game.LevelNum == 27)
        {
            cardFaces[0] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 27")[0];
            cardFaces[1] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 27")[1];
            cardFaces[2] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 27")[3];
            cardFaces[3] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 27")[5];
            cardFaces[4] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 27")[6];
            cardFaces[5] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 27")[2];
        }
        else if (game.LevelNum == 36)
        {
            cardFaces[0] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 36")[0];
            cardFaces[1] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 36")[1];
            cardFaces[2] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 36")[3];
            cardFaces[3] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 36")[5];
            cardFaces[4] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 36")[4];
            cardFaces[5] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 36")[2];
        }
        else if (game.LevelNum == 45)
        {
            cardFaces[0] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 45")[0];
            cardFaces[1] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 45")[1];
            cardFaces[2] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 45")[3];
            cardFaces[3] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 45")[5];
            cardFaces[4] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 45")[4];
            cardFaces[5] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 45")[2];
        }
        else if (game.LevelNum == 54)
        {
            cardFaces[0] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 54")[0];
            cardFaces[1] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 54")[1];
            cardFaces[2] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 54")[3];
            cardFaces[3] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 54")[5];
            cardFaces[4] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 54")[4];
            cardFaces[5] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 54")[2];
        }
        else if (game.LevelNum == 64)
        {
            cardFaces[0] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 64")[0];
            cardFaces[1] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 64")[1];
            cardFaces[2] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 64")[2];
            cardFaces[3] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 64")[3];
            cardFaces[4] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 64")[4];
            cardFaces[5] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 64")[5];
        }
        else if (game.LevelNum == 72)
        {
            cardFaces[0] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 72")[0];
            cardFaces[1] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 72")[1];
            cardFaces[2] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 72")[2];
            cardFaces[3] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 72")[3];
            cardFaces[4] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 72")[4];
            cardFaces[5] = Resources.LoadAll<Sprite>("Sprites/Memory cards/Level 72")[5];
        }

        cards = new GameObject[12];
        FoundСards = 0;
        clickSound = GameObject.FindGameObjectWithTag("Click").GetComponent<AudioSource>();

        asset = Resources.Load<TextAsset>("Localization/" + LocalizationManager.currentLanguage + "/UI");
        UIelement = XMLSettings.Load(asset);
    }
    void Start()
    {
        ShooseCardFaces();
        hintText.text = UIelement.UIelements[2].text;
    }

    public void ShooseCardFaces()
    {
        for (int i = 0; i < cardFaces.Length; i++)
        {
            for (int f = 0; f < 2; f++)
            {
                bool go = true;
                while (go)
                {
                    int randomCard = Random.Range(1, 13);
                    GameObject card = cardsPanel.transform.GetChild(randomCard - 1).gameObject;
                    if (card.GetComponent<Card>().Face == null)
                    {
                        go = false;
                        card.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = cardFaces[i];
                        card.GetComponent<Card>().Face = cardFaces[i];
                    }
                }
            }
        }
    }

    public void CheckProgress()
    {
        if (foundСards == 6)
        {
            StartCoroutine(game.WinnerEffect());
        }
    }
}
