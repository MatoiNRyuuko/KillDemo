using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour {
    public UISlider LoadingAnimation;
    public UILabel LoadingPercentText;
    private void Update()
    {
        StartCoroutine(StartLoading("Kill"));
    }
    /*public void LoadGame()
    {
        StartCoroutine(StartLoading("Kill"));
    }*/
    private IEnumerator StartLoading(string sceneName)
    {
        int displayProgress = 0;
        int toProgress = 0;
        Debug.LogError("kasi");
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);  //异步对象  
        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            toProgress = (int)op.progress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                SetLoadingPercentage(displayProgress);
                yield return new WaitForEndOfFrame();
            }
        }

        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            SetLoadingPercentage(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = false;

    }

    public void SetLoadingPercentage(int DisplayProgress)
    {
        LoadingAnimation.value = DisplayProgress * 0.01f;
        LoadingPercentText.text = DisplayProgress.ToString() + "%";
    }
}
