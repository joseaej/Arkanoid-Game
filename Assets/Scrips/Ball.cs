using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] public const float speed = 7f;
    [SerializeField] private LevelController levelController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = rb.linearVelocity.normalized * speed;
    }

    public void Launch(Vector2 dir)
    {
         rb.linearVelocity = dir.normalized * speed;
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Brick"){
            levelController.OnBrickCollider(collision.gameObject.GetComponent<Brick>());
        }
    }
}
