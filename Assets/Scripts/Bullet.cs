using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private Vector3 MovementDirection = new Vector3(1f,0f,0f);
    private float time;

    private void Start()
    {
        StartCoroutine(MoveForward());
    }

    private IEnumerator MoveForward()
    {
        while(true && time<5)
        {
            this.gameObject.transform.localPosition = this.gameObject.transform.localPosition + (MovementDirection * bulletSpeed * Time.deltaTime);
            time = time + Time.deltaTime;
            yield return CouroutineManager._Instance.WaitForFrameEnd();
        }
        Destroy(this.gameObject.transform.parent.gameObject);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Helicopter")
        {
            GameManager.Instance.currentScoreValue++;
            GameManager.Instance.UpdateScoreUI();
            collision.gameObject.GetComponent<Helicopter>().OnDestroyAction();
            time = 6f;
        }
        else if (collision.transform.tag == "Enemy")
        {
            GameManager.Instance.currentScoreValue++;
            GameManager.Instance.UpdateScoreUI();
            Destroy(collision.gameObject);
            time = 6f;
        }
    }
}
