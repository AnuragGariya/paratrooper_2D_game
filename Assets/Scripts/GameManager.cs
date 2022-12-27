using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<Enemy> numberOfEnemyInLeft;
    public List<Enemy> numberOfEnemyInRight;
    public SpriteRenderer turretBase;
    public TurretControls turetControler;
    public delegate void GameOverAction();
    public static event GameOverAction GameOverSequence;
    public int currentScoreValue;
    [SerializeField] Text highScore;
    [SerializeField] Text currentScore;
    [SerializeField] Text finalText;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        GameOverSequence += StartEndSequence;
        currentScore.text = currentScoreValue.ToString();
        highScore.text = SavedData.HighScore.ToString();
    }

    public void CheckEnemyCount()
    {
        if(numberOfEnemyInLeft.Count>=3 || numberOfEnemyInRight.Count>=3)
        {
            if(GameOverSequence!=null)
            {
                GameOverSequence();
            }
        }
    }

    private void StartEndSequence()
    {
       if(numberOfEnemyInLeft.Count >= 3)
        {
            float enemyWidth = numberOfEnemyInLeft[0].gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
            float enemyHeight = numberOfEnemyInLeft[0].gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
            float turretWidth = turretBase.bounds.size.x;
            Sequence seq = DOTween.Sequence();

            seq.Append(numberOfEnemyInLeft[0].gameObject.transform.DOMoveX(-(enemyWidth / 2 + turretWidth / 2), 0.5f))
                .Append(numberOfEnemyInLeft[1].gameObject.transform.DOMoveX(-(enemyWidth + turretWidth / 2 + enemyWidth / 2), 0.5f))
                .Append(numberOfEnemyInLeft[2].gameObject.transform.DOMoveX(-(enemyWidth * 2 + turretWidth / 2 + enemyWidth / 2), 0.5f))
                .Append(numberOfEnemyInLeft[2].gameObject.transform.DOMove(new Vector3(-(enemyWidth / 2 + turretWidth / 2), (enemyHeight + enemyHeight / 2), 0f), 0.5f));

            seq.Play().OnComplete(()=>
            {
                turetControler.TurretBlast();
                SaveScore();
            });
        }
        else if (numberOfEnemyInRight.Count >= 3)
        {
            float enemyWidth = numberOfEnemyInRight[0].gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
            float enemyHeight = numberOfEnemyInRight[0].gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
            float turretWidth = turretBase.bounds.size.x;
            Sequence seq = DOTween.Sequence();

            seq.Append(numberOfEnemyInRight[0].gameObject.transform.DOMoveX((enemyWidth / 2 + turretWidth / 2), 0.5f))
                .Append(numberOfEnemyInRight[1].gameObject.transform.DOMoveX((enemyWidth + turretWidth / 2 + enemyWidth / 2), 0.5f))
                .Append(numberOfEnemyInRight[2].gameObject.transform.DOMoveX((enemyWidth * 2 + turretWidth / 2 + enemyWidth / 2), 0.5f))
                .Append(numberOfEnemyInRight[2].gameObject.transform.DOMove(new Vector3((enemyWidth / 2 + turretWidth / 2), (enemyHeight + enemyHeight / 2), 0f), 0.5f));

            seq.Play().OnComplete(() =>
            {
                turetControler.TurretBlast();
                SaveScore();
            });
        }
    }

    private void SaveScore()
    {
        SavedData.HighScore = int.Parse(currentScore.text) > SavedData.HighScore ? int.Parse(currentScore.text) : SavedData.HighScore;
        finalText.text = currentScoreValue.ToString();
        finalText.gameObject.transform.parent.gameObject.SetActive(true);
    }

    public void UpdateScoreUI()
    {
        currentScore.text = currentScoreValue.ToString();
    }
}
