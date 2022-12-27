using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    private Vector3 position;
    private Vector3 motionVector = new Vector3(1f,0f,0f);
    [SerializeField] private float speed;
    [SerializeField] private GameObject blast;
    [SerializeField] private GameObject enemy;

    private void Awake()
    {
        position = this.gameObject.transform.position;
        position = position * -1;
    }

    private void Start()
    {
        Invoke(nameof(DropEnemy), Random.Range(1f,4f));
    }

    private void Update()
    {
        if(Mathf.Abs(position.x - transform.position.x) < 0.5f)
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.localPosition = transform.localPosition + (motionVector * speed * Time.deltaTime);
        }
    }

    public void OnDestroyAction()
    {
        GameObject _blast = Instantiate(blast,gameObject.transform);
        _blast.transform.parent = null;
        Destroy(this.gameObject);
    }

    private void DropEnemy()
    {
        GameObject _enemy = Instantiate(enemy, transform);
        _enemy.transform.parent = null;

        if(_enemy.transform.position.x > 0)
        {
            _enemy.transform.eulerAngles = new Vector3(0f,180f,0f);
        }
        else
        {
            _enemy.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }
}
