using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignWall : MonoBehaviour
{
    public float awayFromCenter;
    public GameObject leftWall;
    public GameObject rightWall;
    public Vector3 leftWallPosition;
    public Vector3 rightWallPosition;
    // Start is called before the first frame update
    void Start()
    {
        awayFromCenter = ScalingScript.width / 2;

        leftWallPosition = new(-awayFromCenter - 1.2f, leftWall.transform.position.y, leftWall.transform.position.z);
        rightWallPosition = new(awayFromCenter + 1.2f, rightWall.transform.position.y, rightWall.transform.position.z);

        Instantiate(leftWall, leftWallPosition, Quaternion.identity);
        Instantiate(rightWall, rightWallPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
