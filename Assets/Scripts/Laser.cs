using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _beamSpeed = 8.0f;

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
        transform.Translate(Vector3.up * _beamSpeed * Time.deltaTime);
    }

    void CleanUpBeams()
    {
        if (transform.position.y > 8)
        {
            Destroy(this.gameObject);
        }
    }
}
