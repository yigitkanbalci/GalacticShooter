using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10.0f;
    private float _horizontalAxis;
    private float _verticalAxis;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = 0.0f;

    [SerializeField]
    private int _playerLife = 3;

    [SerializeField]
    private bool _tripleShotActive = false;

    [SerializeField]
    private bool _shieldBoosterActive = false;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;

    private GameObject _playerShield;

    [SerializeField]
    private int _playerScore = 0;

    [SerializeField]
    private GameObject _damageFireFXLeft, _damageFireFXRight;

    [SerializeField]
    private AudioSource _laserFx;

    [SerializeField]
    private AudioSource _powerUpFx;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _playerShield = transform.GetChild(0).gameObject;

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        FireLaser();
    }

    void CalculateMovement()
    {
        _horizontalAxis = Input.GetAxis("Horizontal");
        _verticalAxis = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(_horizontalAxis, _verticalAxis, 0);

        transform.Translate(_speed * Time.deltaTime * direction);


        if (transform.position.y > 7.6)
        {
            transform.position = new Vector3(transform.position.x, -5.5f, 0);
        }
        else if (transform.position.y < -5.0)
        {
            transform.position = new Vector3(transform.position.x, -5.0f, 0);
        }

        if (transform.position.x > 11.2)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3)
        {
            transform.position = new Vector3(11.2f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space) && LaserCooldown())
        {
            if (!_tripleShotActive)
            {
                _nextFire = _fireRate + Time.time;
                Vector3 laserSpawnPosition = new Vector3(transform.position.x, transform.position.y + 0.48f, 0);
                GameObject laserBeam = Instantiate(_laserPrefab, laserSpawnPosition, Quaternion.identity);
            } else
            {
                _nextFire = _fireRate + Time.time;
                Vector3 laserSpawnPosition = new Vector3(transform.position.x + 0.2f, transform.position.y + 1.0f, 0);
                GameObject tripleShotBeam = Instantiate(_tripleShotPrefab, laserSpawnPosition, Quaternion.identity);

            }
            _laserFx.Play();
        }
    }

    bool LaserCooldown()
    {
        if (Time.time > _nextFire)
        {
            return true;
        }

        return false;
    }

    public void TakeDamage()
    {
        if (!_shieldBoosterActive)
        {
            _playerLife--;
            _uiManager.UpdateLivesDisplay(_playerLife);
        }

        if (_playerLife == 2)
        {
            _damageFireFXLeft.SetActive(true);

        } else if (_playerLife == 1)
        {
            _damageFireFXRight.SetActive(true);
        }

        if (_playerLife == 0)
        {
            _uiManager.DisplayGameOver();
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void ActivateTripleShot()
    {
        _tripleShotActive = true;
        _powerUpFx.Play();
        StartCoroutine(TripleShotPowerDown());
    }

    public void ActivateSpeedBooster()
    {
        _speed = 20.0f;
        _powerUpFx.Play();
        StartCoroutine(SpeedBoosterPowerDown());
    }

    public void ActivateShieldBooster()
    {
        _playerShield.SetActive(true);
        _shieldBoosterActive = true;
        _powerUpFx.Play();
        StartCoroutine(ShieldBoosterPowerDown());
    }

    public void UpdateScore()
    {
        _playerScore += 10;
        _uiManager.UpdateScore(_playerScore);
    } 

    IEnumerator TripleShotPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleShotActive = false;
    }

    IEnumerator SpeedBoosterPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _speed = 10.0f;
    }

    IEnumerator ShieldBoosterPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _shieldBoosterActive = false;
        _playerShield.SetActive(false);
    }
}
