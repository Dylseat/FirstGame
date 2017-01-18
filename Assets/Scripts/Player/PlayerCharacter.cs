using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{

    [SerializeField]
    float speed;
    [SerializeField]
    float jumpForce = 10f;
    [SerializeField]
    Animator m_Anim;

    Rigidbody2D m_Body;
    const float walkDeadZone = 0.3f;
    int doubleJump = 0;
    [SerializeField]
    Transform groundCheck;
    bool m_Ground = false;
    float rayonGround = 0.3f;
    public LayerMask Ground;


    // Use this for initialization
    void Start()
    {
        m_Body = GetComponent<Rigidbody2D>();
        m_Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Ground)
        {
            doubleJump = 1;
        }
    }
    void FixedUpdate()
    {
        m_Ground = Physics2D.OverlapCircle(groundCheck.position, rayonGround, Ground);
        m_Anim.SetBool("Ground", m_Ground);
    }
    public void Move(float horizontal, bool jump)
    {
        m_Body.velocity = new Vector2(speed * horizontal, m_Body.velocity.y);

        ////////////////Jump and double jump//////////////////
        if (jump && (m_Ground || doubleJump == 1)) 
        {
            m_Body.velocity = new Vector2(m_Body.velocity.x, jumpForce);
            if (!m_Ground)
            {
                doubleJump--;
            }
        }
        ///////////Animation managment///////////////
        if (Mathf.Abs(horizontal) < walkDeadZone)
        {
            m_Anim.SetBool("Walk", false);
        }
        else
        {
            m_Anim.SetBool("Walk", true);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            SceneManager.LoadScene("level1");
        }
    }
}