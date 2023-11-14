using UnityEngine;

public class MoveDown : MonoBehaviour
{   
    public float speed = 8.5f;
    private float bottomBound = -10f;

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.gameOver == false && PlayerController.isGameActive == true)
        {
            //transform.Translate(Vector3.down * Time.fixedDeltaTime * speed);
            transform.position += Vector3.down * Time.deltaTime * speed;
        }
        if (transform.position.y < bottomBound)
        {
            gameObject.SetActive(false);
        }
    }
}
