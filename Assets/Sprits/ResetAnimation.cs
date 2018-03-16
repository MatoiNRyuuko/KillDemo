using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimation : MonoBehaviour
{

    public TweenAlpha t1;
    public TweenScale t2;
    public TweenAlpha t3;
    public void ResetTween()
    {


        /*UITweener tweener = this.gameObject.GetComponent<UITweener>();
        tweener.ResetToBeginning();*/
        t1.ResetToBeginning();
        t2.ResetToBeginning();
        t3.ResetToBeginning();
        TweenAlpha a = this.gameObject.GetComponent<TweenAlpha>();
        a.value = 0;
    }
}