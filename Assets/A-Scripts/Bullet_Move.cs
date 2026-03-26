using UnityEngine;

public class Bullet_Move : Move_Base
{
    void Start()
    {
        Speed = 10.0f;
    }

    private void Update()
    {
        Move();
    }

    protected override void Move()
    {
        if (rb != null)
        {
            // R�initialiser la v�locit� et appliquer une nouvelle force
            rb.linearVelocity = Vector3.forward * Speed;
        }
        else
        {
            // Fallback au mouvement par transform si pas de Rigidbody
            transform.position += Vector3.forward * Speed * Time.deltaTime;
        }

        // Suppression des balles qui sortent de l'�cran
        if (transform.position.z > 9) // Chang� de y � z
        {
            Destroy(this);
        }
    }
}
