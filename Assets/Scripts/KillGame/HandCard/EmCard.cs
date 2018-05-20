using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmCard : MonoBehaviour {

    private Transform card01;
    private Transform card02;
    private GameObject getCardGo;
    private bool needDestroy = false;

    private List<GameObject> Emcards = new List<GameObject>();

    private float xOffset;
    private EMCardGenerator emCardGenerator;
    private void Awake()
    {
        card01 = transform.Find("card01");
        card02 = transform.Find("card02");
        xOffset = card02.position.x - card01.position.x;
        emCardGenerator = GetComponent<EMCardGenerator>();
    }
    private void Start()
    {
        StartCoroutine(StartPlayGetcard());
    }
    private void Update()
    {
        if (needDestroy)
        {
            DestroyEnemyCard();
            needDestroy = false;
        }
    }

    public void AddCard(GameObject cardGo)
    {
        GameObject go;
        go = cardGo;

        Vector3 toPosition = card01.position + new Vector3(xOffset, 0, 0) * (Emcards.Count);

        iTween.MoveTo(go, toPosition, 0.8f);

        Emcards.Add(go);
    }
    public void RemoveCard(GameObject go)
    {
        Emcards.Remove(go);
    }
    private IEnumerator StartPlayGetcard()
    {
        for (int i = 0; i < 4; i++)
        {
            getCardGo = emCardGenerator.EnemyGenerateCard();
            yield return new WaitForSeconds(1f);
            AddCard(getCardGo);
            yield return new WaitForSeconds(1);
        }
    }
    public void DestroyEnemyCard()
    {
        GameObject.Destroy(Emcards[Emcards.Count-1]);
        Emcards.Remove(Emcards[Emcards.Count - 1]);
    }
    public void DestroyEnemyCardSync()
    {
        needDestroy = true;
    }
    public int GetEnemyHandCardCount()
    {
        return Emcards.Count;
    }
}
