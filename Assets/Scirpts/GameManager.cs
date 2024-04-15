using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ObjManager objManager;
    public Fruit lastFruit;
    public GameObject[] fruits;
    public GameObject ray;
    public Transform fruitGroup;
    public GameObject playGround;
    public GameObject gamePanel;
    public GameObject gameStartPanel;
    public GameObject gameOverPanel;

    public Text scoreTxt;
    public Text bestScoreTxt;
    public Text panelScoreTxt;
    public Text panelBestScoreTxt;

    public AudioSource bgmPlayer;
    public AudioSource dropSound;

    public int score;
    public int maxLevel;

    public bool isOver;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Start()
    {        
        bestScoreTxt.text = PlayerPrefs.GetInt("MaxScore").ToString();
    }

    private void Update()
    {
        scoreTxt.text = score.ToString();
    }

    public void GameStart()
    {
        gameStartPanel.SetActive(false);
        playGround.SetActive(true);
        gamePanel.SetActive(true);

        bgmPlayer.Play();
        NextFruit();
    }
    Fruit GetFruit()
    {
        int fruitIndex = Random.Range(0, maxLevel);
        GameObject instant = objManager.MakeObj(fruitIndex);  
        ray.SetActive(true);
        ray.transform.position = Vector3.zero - new Vector3(0, 2, 0);
        Fruit instantFruit = instant.GetComponent<Fruit>();
        return instantFruit;
    }

    void NextFruit()
    {
        if (isOver)      
            return;
        
        Fruit newFruit = GetFruit();
        lastFruit = newFruit;
        lastFruit.manager = this;
        lastFruit.transform.position = fruitGroup.transform.position;
        lastFruit.circle.enabled = true;
        lastFruit.isFall = false;
        lastFruit.rigid.simulated = false;
        lastFruit.gameObject.SetActive(true);

        StartCoroutine(WaitNext());
    }

    IEnumerator WaitNext()
    {
        while(lastFruit != null)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2.5f);

        NextFruit();
    }

    public void FruitLevelUp(Vector3 targetVec, int level)
    {       
        GameObject levelUpFruit = objManager.MakeObj(level);
        Fruit instantFruit = levelUpFruit.GetComponent<Fruit>();
        instantFruit.manager = this;
        instantFruit.transform.position = targetVec;
        instantFruit.circle.enabled = true;
        instantFruit.isFall = false;
        instantFruit.rigid.simulated = false;
        instantFruit.rigid.simulated = true;
    }

    public void TouchDown()
    {
        if (lastFruit == null)
            return;

        lastFruit.Drag();
    }
    public void TouchUp()
    {
        if (lastFruit == null)
            return;

        ray.SetActive(false);
        lastFruit.Drop();
        dropSound.Play();
        lastFruit = null;
    }

    public IEnumerator GameOver()
    {
        if (isOver)
            yield return null;

        isOver = true;

        Fruit[] Fruits = FindObjectsOfType<Fruit>();

        for (int index = 0; index < Fruits.Length; index++)
        {
            // 물리효과 비활성화
            Fruits[index].rigid.simulated = false;

            // 흔들리는 애니메이션 실행
            //Fruit fruit = Fruits[index].GetComponent<Fruit>();
            //fruit.anim.SetTrigger("isOver");
        }

        // 최고 점수 기록
        int maxScore = Mathf.Max(score, PlayerPrefs.GetInt("MaxScore"));
        PlayerPrefs.SetInt("MaxScore", maxScore);
        bestScoreTxt.text = maxScore.ToString();

        // UI 활성화
        gameOverPanel.SetActive(true);
        panelScoreTxt.text = score.ToString();
        panelBestScoreTxt.text = maxScore.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
