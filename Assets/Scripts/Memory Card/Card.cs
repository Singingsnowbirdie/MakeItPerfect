using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler
{
    Sprite face;
    MemoryCard game;
    AudioSource cardSound;

    public Sprite Face { get => face; set => face = value; }

    void Awake()
    {
        game = FindObjectOfType<MemoryCard>();
        cardSound = GameObject.FindGameObjectWithTag("Card").GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        cardSound.Play();
        GetComponent<Animation>().Play("Flip Card To Face");

        if (game.OpenCard == null)
        {
            game.OpenCard = gameObject;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else
        {
            StartCoroutine(BlockFlips());
            if (game.OpenCard.GetComponent<Card>().Face == face)
            {
                game.FoundСards++;
                game.CheckProgress();
                game.OpenCard = null;
            }
            else
            {
                    StartCoroutine(FlipCardToShirt(gameObject));
                    StartCoroutine(FlipCardToShirt(game.OpenCard));
                    game.OpenCard = null;
            }
        }
    }

    IEnumerator FlipCardToShirt(GameObject card)
    {
        yield return new WaitForSeconds(1.5f);
        cardSound.Play();
        card.GetComponent<Animation>().Play("Flip Card To Shirt");
        yield return new WaitForSeconds(1);
        card.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    IEnumerator BlockFlips()
    {
        transform.parent.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        yield return new WaitForSeconds(1f);
        transform.parent.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

    }
}
