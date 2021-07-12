using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemiePrebab;
    public float repeatDuration;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnTarget()
    {
        while (gameManager.isGameActive)
        {
            yield return new WaitForSeconds(repeatDuration);
            SpawnEnemies();
        }
    }

    float RandomPosition(int negRange, int plusRange)
    {
        float randomPos = Random.Range(negRange, plusRange);
        return randomPos;
    }

    void SpawnEnemies()
    {
        float randomScale = RandomPosition(1, 1);
        enemiePrebab.transform.localScale = new Vector3(randomScale/2, randomScale/2, randomScale/2);
        Instantiate(enemiePrebab, new Vector3(RandomPosition(-10,10), 25, 45), enemiePrebab.transform.rotation);
    }
}
