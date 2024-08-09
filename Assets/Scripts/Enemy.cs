using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _enemySpeed = 4.0f;

    private Player _player = null;

    private Player _player1 = null;
    private Player _player2 = null;

    private Animator _enemyAnimator;
    private BoxCollider2D _enemyBoxCollider;

    private AudioSource _explosionSoundFX;

    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 3.0f;
    private float _canFire = -1;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager != null)
        {
            if (_gameManager.isCoOp == true)
            {
                _player1 = GameObject.Find("Player_1").GetComponent<Player>();
                _player2 = GameObject.Find("Player_2").GetComponent<Player>();
            } else
            {
                _player = GameObject.Find("Player").GetComponent<Player>();
            }
        }

        _enemyAnimator = gameObject.GetComponent<Animator>();

        if (_enemyAnimator == null)
        {
            Debug.Log("Cannot get Animator component in Start method of Enemy script!");
        }
        _enemyAnimator.ResetTrigger("OnEnemyDeath");

        _enemyBoxCollider = gameObject.GetComponent<BoxCollider2D>();
        _explosionSoundFX = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser laser = enemyLaser.GetComponentInChildren<Laser>();
            if (laser != null)
            {
                laser._setEnemyLaser();
            }
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y < -5.0f)
        {
            Destroy(this.gameObject);
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
            _enemyAnimator.SetTrigger("OnEnemyDeath");

            _explosionSoundFX.Play();
            Destroy(this.gameObject, 2.5f);
        }

        if (other.CompareTag("Laser"))
        {

            Destroy(other.gameObject);
            if (player != null)
            {
                player.UpdateScore();
            }
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _explosionSoundFX.Play();
            Destroy(this.gameObject, 2.5f);
        }
        _enemyBoxCollider.enabled = false;
    }
}
