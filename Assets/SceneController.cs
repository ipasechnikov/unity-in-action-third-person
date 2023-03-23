using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // Serialized variable for linking to the prefab object
    [SerializeField] GameObject enemyPrefab;

    // Variable to keep track of the enemy instance in the scene
    private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == null)
        {
            enemy = Instantiate(enemyPrefab);
            enemy.transform.position = new Vector3(0, 1, 0);
            var angle = Random.Range(0, 360);
            enemy.transform.Rotate(0, angle, 0);
        }
    }
}
