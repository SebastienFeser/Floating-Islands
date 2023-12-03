using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScript : MonoBehaviour
{

    public float amp = 1;
    public float freq = 1;
    private float initPos;

    private void Awake()
    {
        initPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, Mathf.Sin(Time.time * freq) * amp + initPos);
    }
}
