using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManger : MonoBehaviour
{
    static string nextScene;

    [SerializeField]Image Bar;


    public static void LoadScene(string scenename)
    {
        nextScene = scenename;
        SceneManager.LoadScene("Loading"); 
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Loadscen());
    }
    IEnumerator Loadscen()
    {
       AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
       op.allowSceneActivation = false;
        float timer = 0f;
        while(!op.isDone)
        {
            yield return null;  
            if(op.progress<0.9f)
            {
                Bar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                Bar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if(Bar.fillAmount>=1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

  
}
