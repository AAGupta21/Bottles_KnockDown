using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    [SerializeField] private List<Transform> PosList = null;
    [SerializeField] private List<GameObject> BottlePrefab  = null;
    
    public static bool isCreated = false;

    private void Update()
    {
        if (GameManager.gameStage == GameStage.SetGame)
        {
            Instantiate(BottlePrefab[Random.Range(0, BottlePrefab.Count)], PosList[Random.Range(0, PosList.Count)].position, Quaternion.identity, transform);
            GameManager.gameStage = GameStage.Shooting;
        }

        if (GameManager.gameStage == GameStage.RemoveBottles)
            RemoveBottles();
    }

    private void RemoveBottles()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("Bottles");

        foreach (GameObject go in g)
            Destroy(go);

        GameManager.gameStage = GameStage.CleanBottles;
    }
}