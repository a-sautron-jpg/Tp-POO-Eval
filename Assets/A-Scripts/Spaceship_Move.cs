using UnityEngine;

public class Spaceship_Move : Entity_Move
{
    void Start()
    {
        Speed = 3.0f;
    }

    private void Update()
    {
        Move();
    }
    protected override void Move()
    {
        if (rb != null)
        {
            // Appliquer directement une v�locit� au Rigidbody
            rb.linearVelocity = Vector3.back * Speed;
        }
        else
        {
            // Fallback au mouvement par transform si pas de Rigidbody
            transform.position += Vector3.back * Speed * Time.deltaTime;
        }

        base.Move();
    }

    public void SelfDestruct()
    {
        // M�thode pour g�rer les collisions avec le joueur

        // Destruction de l'objet qui a touch� le joueur
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

    }
}
