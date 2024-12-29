using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class playerMove : MonoBehaviour
{
    public CharacterController2D controller;
    Rigidbody2D cc;
    public float runS = 40f;
    float horiM = 0f;
    public Animator ami;
    bool jp = true;
    bool isAttacking = false;
    public Slider s;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<Rigidbody2D>();
        gm = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        char alpha;

        if (transform.position.x < -8.6)
        {
            transform.position = new Vector2(-8.6f, transform.position.y);
        }
        if (transform.position.y < -8.6)
        {
            SceneManager.LoadSceneAsync(1);
        }
        horiM = Input.GetAxisRaw("Horizontal") * runS;
        ami.SetFloat("speed", Mathf.Abs(horiM));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cc.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                cc.AddForce(Vector2.up * 13, ForceMode2D.Impulse);
                ami.SetBool("isJump", true);
            }

        }
        if (Input.GetKeyDown(KeyCode.Z) && !isAttacking)
        {
            alpha = 'Z';
            Attack(alpha);
        }
        if (Input.GetKeyDown(KeyCode.X) && !isAttacking)
        {
            alpha = 'X';
            Attack(alpha);
        }
        if (Input.GetKey(KeyCode.R))
        {

            SceneManager.LoadSceneAsync(1);
            Time.timeScale = 1;
        }
    }
    public void onLand()
    {
        ami.SetBool("isJump", false);
    }

    void Attack(char alpha)
    {
        if (alpha == 'Z')
        {
            isAttacking = true;
            ami.SetBool("attack", true);

            // Invoke method to reset attacking flag after animation duration
            Invoke("ResetAttackFlag", 0.5f); // Adjust the duration as per your animation
        }
        else if (alpha == 'X')
        {
            isAttacking = true;
            ami.SetBool("attack2", true);
            // Invoke method to reset attacking flag after animation duration
            Invoke("ResetAttackFlag2", 0.5f); // Adjust the duration as per your animation
        }

    }

    void ResetAttackFlag()
    {
        isAttacking = false;
        ami.SetBool("attack", false);
    }

    void ResetAttackFlag2()
    {
        isAttacking = false;
        ami.SetBool("attack2", false);
    }

    private void FixedUpdate()
    {
        controller.Move(horiM * Time.fixedDeltaTime, false, jp);
        jp = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("food"))
        {
            s.value += 0.4f;
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") && isAttacking)
        {
            // Get the enemy's animator component
            Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();

            // Trigger the dead animation of the enemy
            if (enemyAnimator != null)
            {
                enemyAnimator.SetBool("dead", true);
            }

            // Destroy the collided enemy object after a delay
            Destroy(collision.gameObject, 1.0f); // You can adjust the delay as needed
        }

        else if (collision.gameObject.CompareTag("gameO"))
        {
            Time.timeScale = 0;
            gm.GameO();
        }
    }
    public void decH(float v)
    {
        s.value -= v;
        if(s.value <= 0)
        { 
            Time.timeScale = 0;
            gm.GameO();
        }
    }
}

