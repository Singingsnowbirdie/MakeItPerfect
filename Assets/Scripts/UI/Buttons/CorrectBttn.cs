using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorrectBttn : MonoBehaviour
{
    GameObject image;
    AudioSource correctSound;

    enum Anim
    {
        appearance,
        dissolution,
        flipZ,
        moveXY,
        resize,
        rotationXY,
        rotationY,
        rotationX,
        xPosMin30,
        replace,
        no
    }

    [SerializeField] Anim currentAnim;
    [SerializeField] GameObject extraBttn;

    GameObject effect;
    GameManager game;

    private void Awake()
    {
        game = FindObjectOfType<GameManager>();
        image = transform.parent.gameObject;
        correctSound = GameObject.FindGameObjectWithTag("Correct").GetComponent<AudioSource>();
        effect = transform.GetChild(0).gameObject;
    }

    public void Press()
    {
        correctSound.Play();
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        if(extraBttn != null)
        {
            extraBttn.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        if (currentAnim == Anim.appearance)
        {
            image.GetComponent<Animation>().Play("Appearance");
        }
        if (currentAnim == Anim.replace)
        {
            image.GetComponent<Animation>().Play("Dissolution");
            image.transform.parent.transform.GetChild(0).GetComponent<Animation>().Play("Appearance");
        }
        else if (currentAnim == Anim.dissolution)
        {
            image.GetComponent<Animation>().Play("Dissolution");
        }
        else if (currentAnim == Anim.flipZ)
        {
            image.GetComponent<Animation>().Play("Flip Z");
        }
        else if (currentAnim == Anim.moveXY)
        {
            image.GetComponent<Animation>().Play("Move XY");
        }
        else if (currentAnim == Anim.resize)
        {
            image.GetComponent<Animation>().Play("Resize");
        }
        else if (currentAnim == Anim.rotationY)
        {
            image.GetComponent<Animation>().Play("Rotation Y");
        }
        else if (currentAnim == Anim.rotationX)
        {
            image.GetComponent<Animation>().Play("Rotation X");
        }
        else if (currentAnim == Anim.rotationXY)
        {
            image.GetComponent<Animation>().Play("Rotation XY");
        }
        else if (currentAnim == Anim.xPosMin30)
        {
            image.GetComponent<Animation>().Play("XPos Min30");
        }

        effect.SetActive(true);
        effect.GetComponent<ParticleSystem>().Play();
        image.GetComponent<State>().Solved = true;
        game.CorrectAnswer();
    }
}
