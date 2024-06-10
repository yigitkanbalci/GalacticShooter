using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{

    [SerializeField]
    private float _boosterSpeed = 2.0f;

    [SerializeField]
    private int _boosterType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveBooster();
    }

    void MoveBooster()
    {
        transform.Translate(Vector3.down * _boosterSpeed * Time.deltaTime);

        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.transform.GetComponent<Player>();
            if (player != null)
            {

                if (_boosterType == 0)
                {
                    player.ActivateTripleShot();
                } else if (_boosterType == 1)
                {
                    player.ActivateSpeedBooster();
                } else if (_boosterType == 2)
                {
                    player.ActivateShieldBooster();
                }
                
            }
            Destroy(this.gameObject);
        }
    }
}
