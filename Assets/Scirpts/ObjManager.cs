using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    public GameObject cherryPrefabs;
    public GameObject strawberryPrefabs;
    public GameObject grapePrefabs;
    public GameObject orangePrefabs;
    public GameObject persimmonPrefabs;
    public GameObject applePrefabs;
    public GameObject pearPrefabs;
    public GameObject peachPrefabs;
    public GameObject pineApplePrefabs;
    public GameObject mellonPrefabs;
    public GameObject waterMellonPrefabs;

    GameObject[] cherrys;
    GameObject[] strawberrys;
    GameObject[] grapes;
    GameObject[] oranges;
    GameObject[] persimmons;
    GameObject[] apples;
    GameObject[] pears;
    GameObject[] peachs;
    GameObject[] pineApples;
    GameObject[] mellons;
    GameObject[] waterMellons;

    GameObject[] targetPool;

    private void Awake()
    {
        cherrys = new GameObject[10];
        strawberrys = new GameObject[10];
        grapes = new GameObject[10];
        oranges = new GameObject[10];
        persimmons = new GameObject[10];
        apples = new GameObject[10];
        pears = new GameObject[10];
        peachs = new GameObject[10];
        pineApples = new GameObject[7];
        mellons = new GameObject[5];
        waterMellons = new GameObject[3];

        Generate();
    }

    void Generate()
    {
        for (int index = 0; index < cherrys.Length; index++)
        {
            cherrys[index] = Instantiate(cherryPrefabs);
            cherrys[index].SetActive(false);
        }
        for (int index = 0; index < strawberrys.Length; index++)
        {
            strawberrys[index] = Instantiate(strawberryPrefabs);
            strawberrys[index].SetActive(false);
        }
        for (int index = 0; index < grapes.Length; index++)
        {
            grapes[index] = Instantiate(grapePrefabs);
            grapes[index].SetActive(false);
        }
        for (int index = 0; index < oranges.Length; index++)
        {
            oranges[index] = Instantiate(orangePrefabs);
            oranges[index].SetActive(false);
        }
        for (int index = 0; index < persimmons.Length; index++)
        {
            persimmons[index] = Instantiate(persimmonPrefabs);
            persimmons[index].SetActive(false);
        }
        for (int index = 0; index < apples.Length; index++)
        {
            apples[index] = Instantiate(applePrefabs);
            apples[index].SetActive(false);
        }
        for (int index = 0; index < pears.Length; index++)
        {
            pears[index] = Instantiate(pearPrefabs);
            pears[index].SetActive(false);
        }
        for (int index = 0; index < peachs.Length; index++)
        {
            peachs[index] = Instantiate(peachPrefabs);
            peachs[index].SetActive(false);
        }
        for (int index = 0; index < pineApples.Length; index++)
        {
            pineApples[index] = Instantiate(pineApplePrefabs);
            pineApples[index].SetActive(false);
        }
        for (int index = 0; index < mellons.Length; index++)
        {
            mellons[index] = Instantiate(mellonPrefabs);
            mellons[index].SetActive(false);
        }
        for (int index = 0; index < waterMellons.Length; index++)
        {
            waterMellons[index] = Instantiate(waterMellonPrefabs);
            waterMellons[index].SetActive(false);
        }
    }

    public GameObject MakeObj(int level)
    {

        switch (level)
        {
            case 0:
                targetPool = cherrys;
                break;
            case 1:
                targetPool = strawberrys;
                break;
            case 2:
                targetPool = grapes;
                break;
            case 3:
                targetPool = oranges;
                break;
            case 4:
                targetPool = persimmons;
                break;
            case 5:
                targetPool = apples;
                break;
            case 6:
                targetPool = pears;
                break;
            case 7:
                targetPool = peachs;
                break;
            case 8:
                targetPool = pineApples;
                break;
            case 9:
                targetPool = mellons;
                break;
            case 10:
                targetPool = waterMellons;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }
        return null;
    }
}
