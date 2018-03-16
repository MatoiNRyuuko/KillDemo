using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class StartMenu : MonoBehaviour {
    public VideoPlayer vPlayer;
    public UILabel Skip;
    public bool skip = false;

    void Start () {
        vPlayer.Play();
	}

	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (skip == false)
            {
                Skip.text = "再按一次屏幕跳过开头动画";
                skip = true;
            }
                
            else if(skip == true)
            {
                vPlayer.Stop();

                SceneManager.LoadScene("LoadingScene"); 
            }
           
        }
    }

}
