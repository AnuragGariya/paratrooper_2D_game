using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    private Animator anim;
    private float lifetime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        anim.Play("Blast");
    }

    private void Update()
    {
        lifetime = lifetime + Time.deltaTime;
        if(lifetime>0.8f)
        {
            Destroy(this.gameObject);
        }
    }
}
