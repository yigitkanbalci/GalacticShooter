using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _enemySpeed = 4.0f;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y < -3.9)
        {
            transform.position = new Vector3(Random.Range(-9.3f, 9.3f), 7.6f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.transform.GetComponent<Player>();

        if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                player.TakeDamage();
            }

            Destroy(this.gameObject);
        }

        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.UpdateScore();
            }
            Destroy(this.gameObject);
        }
    }
}
