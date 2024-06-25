using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _enemySpeed = 4.0f;

    private Player _player;

    private Animator _enemyAnimator;
    private BoxCollider2D _enemyBoxCollider;

    private AudioSource _explosionSoundFX;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("Cannot get Player component in Start method of Enemy script!");
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
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y < - 5.0f)
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
            if (_player != null)
            {
                _player.UpdateScore();
            }
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _explosionSoundFX.Play();
            Destroy(this.gameObject, 2.5f);
        }
        _enemyBoxCollider.enabled = false;
    }
}
