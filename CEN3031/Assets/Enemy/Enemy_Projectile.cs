using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Projectile : MonoBehaviour {

    public float speed;
    public float lifespan = 5; // how long before the projectile is automatically destroyed
    Vector2 dir;//projectile direction
    bool isReady;//dir set
    float destroyTimer;
    public int damage = 1; // damage value ***ONLY ADDED THIS FOR TESTING HEALTH BAR, FEEL FREE TO MODIFY IT***

    void Awake()
    {
        isReady = false;
       
    }

	// Use this for initialization
	void Start () {
        destroyTimer = lifespan;
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

        destroyTimer -= Time.deltaTime;
        if (destroyTimer <= 0)
            Destroy(gameObject);

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.tag == "Player") || (collision.tag == "Enviroment"))
        {
            Destroy(gameObject);
        }
    }
}
