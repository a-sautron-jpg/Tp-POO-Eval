using UnityEngine;

public class PlayerShip : MonoBehaviour
{

    // M�thode pour g�rer les collisions avec le joueur
    //public void HandlePlayerHit(GameObject hitObject)
    //{
    //    // Destruction de l'objet qui a touch� le joueur
    //    Instantiate(explosionPrefab, hitObject.transform.position, Quaternion.identity);

    //    if (hitObject.CompareTag("Enemy"))
    //    {
    //        Destroy(hitObject);
    //        enemies.Remove(hitObject);
    //    }
    //    else if (hitObject.CompareTag("Asteroid"))
    //    {
    //        Destroy(hitObject);
    //        asteroids.Remove(hitObject);
    //    }

    //    // Perte d'une vie
    //    lives--;

    //    if (lives <= 0)
    //    {
    //        GameOver();
    //    }
    //}
}