using UnityEngine;
public class Target : MonoBehaviour
{
    public GameManager gameManager;
    public int pointValue = 1;
    public ParticleSystem explosionParticle;
    public ParticleSystem playerExplosionParticle;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.gameOver && !PlayerController.isGameActive)
        {
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);           
            gameObject.SetActive(false);
        }   
    }

    private void OnTriggerEnter(Collider other) 
    {
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        gameObject.SetActive(false);
        if(gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<AudioManager>().Play("Explosion");
            Instantiate(playerExplosionParticle, transform.position, playerExplosionParticle.transform.rotation);
            gameManager.GameOver();
            Destroy(other.gameObject);
        }
        if(gameObject.CompareTag("Capitalizer"))
        {
            FindObjectOfType<AudioManager>().Play("Shatter");
            gameManager.UpdateScore(pointValue);
            gameManager.AnimateScoreText();
        }  
    }
}
