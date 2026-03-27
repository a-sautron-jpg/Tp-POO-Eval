using UnityEngine;

public class Entity_Move : Move_Base, IDestructable
{
    public GameObject playerDamageEffect;


    protected override void Move()
    {
        if (transform.position.z < -12)
        {
            if(gameManager == null)
            {
                gameManager = FindAnyObjectByType<GameManager>();
            }
            gameManager.lives--;

            // Effet visuel pour montrer que l'ennemi a travers�
            if (playerDamageEffect != null)
            {
                Instantiate(playerDamageEffect, transform.position, Quaternion.identity);
            }

            // Destruction de l'ennemi
            Destroy(this.gameObject);

            //V�rifier si le joueur n'a plus de vies

            if (gameManager.lives <= 0)
            {
                gameManager.GameOver();
            }
        }
    }

    protected override void Start()
    {
        base.Start();
    }


    // Utilisons OnCollisionEnter au lieu de OnTriggerEnter
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager = FindAnyObjectByType<GameManager>();

            explosionManager = FindAnyObjectByType<ExplosionManager>();

            // Perte d'une vie
            gameManager.lives--;

            if (gameManager.lives <= 0)
            {
                gameManager.GameOver();
            }

            // Le joueur a touché un entité
            collision.gameObject.GetComponent<IDestructor>().DamageCible(this);
        }
    }

    public virtual void SelfDestruct()
    {
        // M�thode pour g�rer les collisions avec le joueur

        // Destruction de l'objet qui a touch� le joueur
        explosionPrefab = (GameObject)Resources.Load("Prefabs\\Explode");
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        
    }
}


