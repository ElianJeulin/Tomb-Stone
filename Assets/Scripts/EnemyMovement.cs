using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = Random.Range(400, 1500);
        if (transform.position.y < 17)
        {
            enemyRb.AddForce(Vector3.back * speed * Time.deltaTime);
        }
        if (transform.position.y < -20)
        {
            Destroy(gameObject);
        }
    }
}
