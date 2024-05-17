using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public GameObject _camera;
    private float _length, _startPos;
    
    public float paralaxAmount;
    
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = (_camera.transform.position.x * paralaxAmount);

        transform.position = new Vector3(_startPos + dist, transform.position.y, transform.position.z);
    }
}
