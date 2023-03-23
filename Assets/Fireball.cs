using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    [SerializeField] int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var deltaZ = speed * Time.deltaTime;
        transform.Translate(0, 0, deltaZ);
    }

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerCharacter>();
        if (player != null)
            player.Hurt(damage);

        Destroy(gameObject);
    }
}
