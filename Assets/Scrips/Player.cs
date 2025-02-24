using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const float SPEED = 7f;
    private Rigidbody2D rb;
    private bool isDragging = false;
    private Vector3 offset;
    private FixedJoint2D joint;
    [SerializeField] private Ball ball;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<FixedJoint2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        StartCoroutine(UnlockeBall());
    }

    void Update()
    {
        if (!isDragging)
        {
            float dx = Input.GetAxis("Horizontal");
            rb.linearVelocity = new Vector2(dx * SPEED, rb.linearVelocity.y);
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        offset = transform.position - mouseWorldPos;
        rb.linearVelocity = Vector2.zero;
    }

    void OnMouseDrag()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        rb.MovePosition(mouseWorldPos + offset);
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    private IEnumerator UnlockeBall()
    {
        yield return new WaitForSeconds(2f);
        joint.enabled = false;
        ball.Launch(Vector2.up);
    }
    public IEnumerator Restart()
    {
        yield return new WaitForSeconds(1f); 
        joint.enabled = true; 
        transform.position = Vector3.zero; 
        yield return UnlockeBall(); 
    }

}
