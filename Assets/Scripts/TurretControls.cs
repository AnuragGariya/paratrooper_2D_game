using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControls : MonoBehaviour
{
    [SerializeField] private float turretSpeed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform[] muzzles;
    [SerializeField] private GameObject _blast;
    private GameObject b1, b2;
    private bool movingLeft;
    private bool movingRight;
    private bool firing;
    private bool gameOver;

    private void Start()
    {
        GameManager.Instance.turretBase = this.gameObject.GetComponent<SpriteRenderer>();
        GameManager.Instance.turetControler = this;
        GameManager.GameOverSequence += GameOverBoolSet;
    }

    private Vector3 rotationAngles = new Vector3(0f,0f,1f);

    public void TurnLeft()
    {
        if (movingLeft) return;
        StartCoroutine(MoveLeft());
    }

    public void TurnRight()
    {
        if (movingRight) return;
        StartCoroutine(MoveRight());
    }

    public void Fire()
    {
        movingLeft = false;
        movingRight = false;
        if (firing || gameOver) return;

        StartCoroutine(FireBullets());
    }

    private IEnumerator MoveLeft()
    {
        movingLeft = true;
        movingRight = false;

        while (movingLeft && Mathf.Abs(transform.eulerAngles.z - 90) > 1f)
        {
            transform.eulerAngles = transform.eulerAngles + (rotationAngles * turretSpeed * Time.deltaTime);
            yield return CouroutineManager._Instance.WaitForFrameEnd();
        }
    }

    private IEnumerator MoveRight()
    {
        movingLeft = false;
        movingRight = true;

        while (movingRight && Mathf.Abs(transform.eulerAngles.z - 270) > 1f)
        {
            transform.eulerAngles = transform.eulerAngles - (rotationAngles * turretSpeed * Time.deltaTime);
            yield return CouroutineManager._Instance.WaitForFrameEnd();
        }
    }

    private IEnumerator FireBullets()
    {
        firing = true;

        while(!movingLeft && !movingRight && !gameOver)
        {
            b1 =Instantiate(bullet, muzzles[0]);
            b2 = Instantiate(bullet, muzzles[1]);
            b1.transform.parent = null;
            b2.transform.parent = null;
            yield return CouroutineManager._Instance.WaitFor1Sec();
        }

        firing = false;
    }

    private void GameOverBoolSet()
    {
        gameOver = true;
    }

    public void TurretBlast()
    {
        Instantiate(_blast, transform);
    }
}
