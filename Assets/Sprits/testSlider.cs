using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testSlider : MonoBehaviour {
    public UISlider LoadingAnimation;
    public UILabel LoadingPercentText;
    private void Awake()
    {
        StartCoroutine(GenerateCardForHero());
        

    }
    public IEnumerator GenerateCardForHero()
    {
        yield return new WaitForSeconds(1f);
        /*LoadingPercentText.text = Convert.ToString(LoadingAnimation.value);
        AsyncOperation op = SceneManager.LoadSceneAsync("Kill");

        op.allowSceneActivation = false;*/

        while (LoadingAnimation.value < 1f)
        {
            float Percent = UnityEngine.Random.Range(0, 0.3f);
            float randomSecond = UnityEngine.Random.Range(0, 5);
            LoadingAnimation.value += Percent;
            //LoadingPercentText.text= Convert.ToString(LoadingAnimation.value * 100 ) +"%";
            yield return new WaitForSeconds(randomSecond);
        }

        if (LoadingAnimation.value == 1)
        {
            SceneManager.LoadScene("Kill");
        }
    }
}
