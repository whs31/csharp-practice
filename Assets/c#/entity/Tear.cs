using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tear : MonoBehaviour
{
    public float damage = 3.5f;
    public float range = 5f;
    public float shotSpeed = 2f;
    public Vector2 direction;

    [SerializeField]
    private GameObject texture;
    [SerializeField]
    private GameObject tearPoof;

    private float lifetime;

    void Start()
    {
        texture.transform.position = new Vector3(transform.position.x, 0.2f + transform.position.y + range / 27f, transform.position.z);
        lifetime = range / (shotSpeed * 5);
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position + direction.normalized * Time.fixedDeltaTime * shotSpeed * 4);
        lifetime -= Time.fixedDeltaTime;
        float scaledLifetime = 0.2f + lifetime * 5 * shotSpeed / range;
        texture.transform.position = new Vector3(transform.position.x, transform.position.y + range * scaledLifetime / 27f, transform.position.z);
        if (lifetime <= 0)
        {
            TearDestroy();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            TearDestroy();
        }
    }

    void TearDestroy()
    {
        GameObject poof = Instantiate(tearPoof, transform.position, transform.rotation);
        Destroy(poof, 1f);
        Destroy(gameObject);
    }
}
