using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _beamSpeed = 8.0f;
    private bool _isEnemyLaser = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveBeam();
        CleanUpBeams();
    }

    void MoveBeam()
    {
        if (_isEnemyLaser == true)
        {
            MoveDown();
        } else
        {
            MoveUp();
        }
    }

    void CleanUpBeams()
    {
        if (transform.position.y > 8)
        {
            Destroy(this.gameObject);
        }
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _beamSpeed * Time.deltaTime);
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * _beamSpeed * Time.deltaTime);

    }

    public void _setEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage();
                Destroy(this.gameObject);
            }
        }
    }
}
