using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _boosterContainer;
    [SerializeField]
    private GameObject _tripleShotBooster;
    [SerializeField]
    private GameObject _shieldBooster;
    [SerializeField]
    private GameObject _speedBooster;

    private bool _stopSpawning = false;
    private IEnumerator _spawnEnemyRoutine;
    private IEnumerator _spawnBoosterRoutine;

    // Start is called before the first frame update
    void Start()
    {
        _spawnEnemyRoutine = SpawnEnemyRoutine();
        _spawnBoosterRoutine = SpawnBoosterRoutine();
    }

    public void SpawnStart()
    {
        StartCoroutine(_spawnEnemyRoutine);
        StartCoroutine(_spawnBoosterRoutine);
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while(_stopSpawning == false)
        {
            GameObject newEnemey = Instantiate(_enemyPrefab, new Vector3(Random.Range(-9.3f, 9.3f), 7.6f, 0), Quaternion.identity);
            newEnemey.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        }
    }

    IEnumerator SpawnBoosterRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        GameObject newBooster = null;

        while(_stopSpawning == false)
        {

            int boosterType = Random.Range(0, 3);

            switch (boosterType)
            {
                case 0:
                    newBooster = Instantiate(_tripleShotBooster, new Vector3(Random.Range(-9.3f, 9.3f), 7.6f, 0), Quaternion.identity);
                    break;
                case 1:
                    newBooster = Instantiate(_speedBooster, new Vector3(Random.Range(-9.3f, 9.3f), 7.6f, 0), Quaternion.identity);
                    break;
                case 2:
                    newBooster = Instantiate(_shieldBooster, new Vector3(Random.Range(-9.3f, 9.3f), 7.6f, 0), Quaternion.identity);
                    break;
                default:
                    Debug.Log("Booster Type does not exist!");
                    break;

            }

            if(newBooster != null)
            {
                newBooster.transform.parent = _boosterContainer.transform;

            }
            yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
