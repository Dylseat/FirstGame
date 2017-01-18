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
    bool IsTurnedRight = true;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    LayerMask Ground;
    [SerializeField]
    AudioClip SoundJump;

    Rigidbody2D m_Body;
    Animator m_Anim;
    AudioSource m_Sound;
    const float walkDeadZone = 0.3f;
    int doubleJump = 0;
    bool m_Ground = false;
    float rayonGround = 0.3f;


    // Use this for initialization
    void Start()
    {
        m_Body = GetComponent<Rigidbody2D>();
        m_Anim = GetComponent<Animator>();
        m_Sound = GetComponent<AudioSource>();
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
       
        if (horizontal > 0 && !IsTurnedRight)
        {
            Flip();
        }
        else if (horizontal < 0 && IsTurnedRight)
        {
            Flip();
        }
        ////////////////Jump and double jump//////////////////
        if (jump && (m_Ground || doubleJump == 1)) 
        {
            m_Body.velocity = new Vector2(m_Body.velocity.x, jumpForce);
            m_Sound.PlayOneShot(SoundJump);
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
    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        IsTurnedRight = !IsTurnedRight;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            SceneManager.LoadScene("level1");
        }
    }
}