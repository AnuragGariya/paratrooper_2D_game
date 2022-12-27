using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool TouchedTheGround;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TouchedTheGround) return;

        if(collision.gameObject.transform.tag == "Ground" && transform.position.x > 0)
        {
            TouchedTheGround = true;
            GameManager.Instance.numberOfEnemyInRight.Add(this);
        }
        else if(collision.gameObject.transform.tag == "Ground" && transform.position.x < 0)
        {
            TouchedTheGround = true;
            GameManager.Instance.numberOfEnemyInLeft.Add(this);
        }
        else if(collision.gameObject.transform.tag == "Enemy" || collision.gameObject.transform.tag == "Turret")
        {
            Destroy(this.gameObject);
        }

        GameManager.Instance.CheckEnemyCount();
    }

    private void OnDestroy()
    {
        if (GameManager.Instance.numberOfEnemyInLeft.Contains(this))
        {
            GameManager.Instance.numberOfEnemyInLeft.Remove(this);
        }
        else if (GameManager.Instance.numberOfEnemyInRight.Contains(this))
        {
            GameManager.Instance.numberOfEnemyInRight.Remove(this);
        }
    }
}
