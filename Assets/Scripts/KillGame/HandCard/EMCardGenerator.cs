using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMCardGenerator : MonoBehaviour {
    public GameObject CardPrefab;

    private Transform EmfromCard;
    private Transform EmtoCard;

    private void Awake()
    {
        EmfromCard = transform.Find("EmFromcard").transform;
        EmtoCard = transform.Find("EmTocard").transform;
    }
    public GameObject EnemyGenerateCard()
    {
        GameObject go = Instantiate(CardPrefab, EmfromCard);

        iTween.MoveTo(go, EmtoCard.position, 0.8f);
        return go;
    }
}
