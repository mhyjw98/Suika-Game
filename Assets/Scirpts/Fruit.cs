using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameManager manager;
    public int level;
    public bool isDrag;
    public bool isMerge;
    public bool isFall;

    public AudioSource leverUpSound;

    public Rigidbody2D rigid;
    public CircleCollider2D circle;
    public Animator anim;

    int[] scores;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        GenerateScore();
    }

    void Update()
    {
        if (isDrag)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // x축 경계 설정
            float leftBorder = -4.6f + transform.localScale.x / 2f;
            float rightBorder = 4.6f - transform.localScale.x / 2f;

            if (mousePos.x < leftBorder)
                mousePos.x = leftBorder;
            else if (mousePos.x > rightBorder)
                mousePos.x = rightBorder;

            mousePos.y = 8;
            mousePos.z = 0;
            transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f);
            manager.ray.transform.position = Vector3.Lerp(transform.position, mousePos - new Vector3(0, 50, 0), 0.2f);
        }      
    }

    public void Drag()
    {
        isDrag = true;
    }

    public void Drop()
    {
        isDrag = false;
        rigid.simulated = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            isFall = true;
            Fruit other = collision.gameObject.GetComponent<Fruit>();

            if (level == other.level && !isMerge &&!other.isMerge && level < 10)
            {
                float meX = transform.position.x;
                float meY = transform.position.y;
                float otherX = other.transform.position.x;
                float otherY = other.transform.position.y;

                Vector3 mergeVec = new Vector3((meX + otherX) / 2, (meY + otherY) / 2, 0); 
                other.Hide();

                LevelUp(mergeVec, level);
            }
        }else if (collision.gameObject.CompareTag("Floor"))
        {
            isFall = true;
        }
    }

    public void Hide()
    {
        isMerge = true;

        rigid.simulated = false;
        circle.enabled = false;

        StartCoroutine(HideRoutine());
    }

    IEnumerator HideRoutine()
    {
        yield return null;
        if(manager.isOver == false)
            manager.score += scores[level];

        isMerge = false;
        gameObject.SetActive(false);
    }

    void GenerateScore()
    {
        scores = new int[10];
        scores[0] = 1;

        for (int i = 1; i < scores.Length; i++)
            scores[i] = scores[i - 1] + i + 1;
    }

    void LevelUp(Vector3 targetVec, int level)
    {
        isMerge = true;

        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0;

        StartCoroutine(LevelUpRoutine(targetVec, level));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish") && isFall)         
            StartCoroutine(manager.GameOver());
    }

    IEnumerator LevelUpRoutine(Vector3 targetVec, int level)
    {
        yield return null;
        level++;
        leverUpSound.Play();
        gameObject.SetActive(false);

        if (level < 6)
        {
            manager.maxLevel = Mathf.Max(level, manager.maxLevel);
        }
        isMerge = false;

        manager.FruitLevelUp(targetVec, level);                 
    }
}
