using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private float speed;
    [SerializeField] private Vector2 startingDirection;

    private List<Rigidbody2D> body;
    private Rigidbody2D rb2D;
    private IEnumerator pelletConversionCoroutine;

    private void Awake()
    {
        rb2D = this.GetComponent<Rigidbody2D>();
        body = new List<Rigidbody2D>
        {
            rb2D
        };
    }

    void Start()
    {
        rb2D.velocity = startingDirection * speed;
    }

    private IEnumerator LerpPelletToTail()
    {
        yield return null;
    }

    private IEnumerator ConvertPelletToBody(GameObject pellet)
    {
        Rigidbody2D pelletRb2D = pellet.GetComponent<Rigidbody2D>();
        CircleCollider2D pelletCollider2D = pellet.GetComponent<CircleCollider2D>();

        //pelletCollider2D.isTrigger = false;

        body.Add(pelletRb2D);
        //pelletRb2D.position = rb2D.position - rb2D.velocity.normalized * 0.6f;
        //pelletRb2D.velocity = rb2D.velocity;
        yield return LerpPelletToTail();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            StartCoroutine(ConvertPelletToBody(collision.gameObject));
        }   
    }

    void Update()
    {
       for(int i = 0; i < body.Count; i++)
       {
            if (i == 0)
                continue;
            body[i].position = body[i - 1].position - body[i - 1].velocity.normalized * 0.6f;
            body[i].velocity = (body[i - 1].position - body[i].position).normalized * speed;
       }
    }
}
