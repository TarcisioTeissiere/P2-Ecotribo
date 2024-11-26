using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public void LoadScene(string scene)
    {
        Fade.Instance.TransitionToScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
