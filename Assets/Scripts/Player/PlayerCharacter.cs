using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{

    [SerializeField]
    float speed = 12;
    [SerializeField]
    float jumpForce = 15f;
    [SerializeField]
    float jumpPushForce = 10f;
    [SerializeField]
    bool IsTurnedRight = true;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    Transform wallCheck;
    [SerializeField]
    LayerMask Ground;
    [SerializeField]
    LayerMask Wall;
    [SerializeField]
    AudioClip SoundJump;
    [SerializeField]
    int maxHealth = 3;

    Rigidbody2D m_Body;
    Animator m_Anim;
    AudioSource m_Sound;
    const float walkDeadZone = 0.3f;
    int doubleJump = 0;
    bool m_Ground = false;
    bool m_Wall = false;
    float rayonGround = 0.15f;
    float wallTouchRadius = 0.4f;
    public int currentHealth;
    bool playerDead;


    // Use this for initialization
    void Start()
    {
        m_Body = GetComponent<Rigidbody2D>();
        m_Anim = GetComponent<Animator>();
        m_Sound = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Ground)
        {
            doubleJump = 1;
        }

        if (m_Wall)
        {
            m_Ground = false;
            doubleJump = 0;
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            PlayerDead();
        }
    }
    void FixedUpdate()
    {
        m_Ground = Physics2D.OverlapCircle(groundCheck.position, rayonGround, Ground);
        m_Wall = Physics2D.OverlapCircle(wallCheck.position, wallTouchRadius, Wall);
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

        if (m_Wall && jump)
        {
            WallJump();
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

    void WallJump()
    {
        m_Sound.PlayOneShot(SoundJump);
        m_Body.velocity = new Vector2(jumpPushForce, jumpForce);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            currentHealth--;
        }
    }
    void PlayerDead()
    {
        m_Body.velocity = new Vector2(m_Body.velocity.x, jumpForce);

        StartCoroutine(Restart());
    }
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("level1");
    }
}