using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tear : MonoBehaviour
{
    public float damage = 3.5f;
    public float range = 5f;
    public float shotSpeed = 2f;
    public Vector2 direction;

    public float eyeHeight = 1f;

    [SerializeField]
    private Sprite[] texture_dmg;
    [SerializeField]
    private GameObject texture;

    [SerializeField]
    private GameObject[] tearPoof;

    private float lifetime;

    void Start()
    {
        if (damage < 1) { texture.GetComponent<SpriteRenderer>().sprite = texture_dmg[0]; } else
        if (damage < 2) { texture.GetComponent<SpriteRenderer>().sprite = texture_dmg[1]; } else
        if (damage < 3) { texture.GetComponent<SpriteRenderer>().sprite = texture_dmg[2]; } else
        if (damage < 4) { texture.GetComponent<SpriteRenderer>().sprite = texture_dmg[3]; } else 
        if (damage < 5) { texture.GetComponent<SpriteRenderer>().sprite = texture_dmg[4]; } else 
        if (damage < 7) { texture.GetComponent<SpriteRenderer>().sprite = texture_dmg[5]; } else 
        if (damage < 9) { texture.GetComponent<SpriteRenderer>().sprite = texture_dmg[6]; } else 
        if (damage < 11) { texture.GetComponent<SpriteRenderer>().sprite = texture_dmg[7]; } else 
        if (damage < 12) { texture.GetComponent<SpriteRenderer>().sprite = texture_dmg[8]; } else 
        if (damage < 20) { texture.GetComponent<SpriteRenderer>().sprite = texture_dmg[9]; } else 
        if (damage < 30) { texture.GetComponent<SpriteRenderer>().sprite = texture_dmg[10]; } else 
        if (damage < 45) { texture.GetComponent<SpriteRenderer>().sprite = texture_dmg[11]; } else 
        if (damage < 60) { texture.GetComponent<SpriteRenderer>().sprite = texture_dmg[12]; } else 
        { texture.GetComponent<SpriteRenderer>().sprite = texture_dmg[13]; }
            texture.transform.position = new Vector3(transform.position.x, transform.position.y + eyeHeight, transform.position.z);
        lifetime = range / (shotSpeed * 5);
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position + direction.normalized * Time.fixedDeltaTime * shotSpeed * 4);
        lifetime -= Time.fixedDeltaTime;
        float scaledLifetime = lifetime * 5 * shotSpeed / range;
        texture.transform.position = new Vector3(transform.position.x, transform.position.y + eyeHeight * scaledLifetime, transform.position.z);
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
        GameObject poof = Instantiate(tearPoof[0], transform.position, transform.rotation);
        Destroy(poof, 1f);
        Destroy(gameObject);
    }
}
