using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartAdminBttn : MonoBehaviour
{
    public void Press()
    {
        SceneManager.LoadScene(0);
    }
}
