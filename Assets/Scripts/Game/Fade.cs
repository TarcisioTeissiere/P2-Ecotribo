using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public static Fade Instance { get; private set; }

    public Animator anim;
    private bool isFading = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Faz o objeto não ser destruído entre cenas
        }
        else
        {
            Destroy(gameObject); // Se já houver um Fade, destrua este objeto
        }
    }

    public void TransitionToScene(string sceneName)
    {
        if (!isFading)
        {
            StartCoroutine(FadeAndLoadScene(sceneName));
        }
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        isFading = true;

        // Fade Out
        anim.SetBool("fade", true); 
        yield return new WaitForSeconds(1.5f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        anim.SetBool("fade", false);
        yield return new WaitForSeconds(1.5f);

        isFading = false;
    }
}
