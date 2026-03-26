using UnityEngine;

public class Entity_Move : Move_Base
{
    public GameObject playerDamageEffect;

    protected override void Move()
    {
        if (transform.position.z < -12)
        {
            gameManager.lives--;

            // Effet visuel pour montrer que l'ennemi a travers�
            if (playerDamageEffect != null)
            {
                Instantiate(playerDamageEffect, transform.position, Quaternion.identity);
            }

            // Destruction de l'ennemi
            Destroy(this);

            //V�rifier si le joueur n'a plus de vies

            if (gameManager.lives <= 0)
            {
                gameManager.GameOver();
            }
        }
    }
}
