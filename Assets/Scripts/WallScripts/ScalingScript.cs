using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class ScalingScript : MonoBehaviour
{
    public static float width;
    public static float height;
    // Start is called before the first frame update
    void Start()
    {
        width = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
        height = Camera.main.orthographicSize * 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
