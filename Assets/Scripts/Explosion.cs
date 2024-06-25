using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<AudioSource>().Play();
        Destroy(this.gameObject, 2.3f);

    }

    // Update is called once per frame
    void Update()
    {
    }
}
