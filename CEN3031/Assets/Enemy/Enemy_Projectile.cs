using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Projectile : MonoBehaviour {

    public float speed;
    Vector2 dir;//projectile direction
    bool isReady;//dir set

    void Awake()
    {
        isReady = false;
       
    }

	// Use this for initialization
	void Start () {
		
	}

    public void setDir(Vector2 direction)
    {
        dir = direction.normalized;
        isReady = true;

    }
	
	// Update is called once per frame
	void Update () {

        if (isReady)
        {
            Vector2 position = transform.position;

            position += dir * speed * Time.deltaTime;

            transform.position = position; // update position of projectile


        }

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.tag == "Player") || (collision.tag == "Enviroment"))
        {
            Destroy(gameObject);
        }
    }
}
