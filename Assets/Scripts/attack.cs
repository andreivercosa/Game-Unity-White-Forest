using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public float speed;
    public float timeDestroy;

    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }

    
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
