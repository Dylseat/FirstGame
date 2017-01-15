using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour
{

    [SerializeField]
    float speed;
    [SerializeField]
    float jumpForce = 10f;
    [SerializeField]
    Animator m_Anim;

    const float walkDeadZone = 0.1f;
    Rigidbody2D m_Body;

    public Transform groundCheck;
    bool m_Ground = false;
    float rayonGround = 0.3f;
    public LayerMask Ground;
    [SerializeField] bool isTurnedRight;

    // Use this for initialization
    void Start()
    {
        m_Body = GetComponent<Rigidbody2D>();
        m_Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        m_Ground = Physics2D.OverlapCircle(groundCheck.position, rayonGround, Ground);
        m_Anim.SetBool("Ground", m_Ground);
    }
    public void Move(float horizontal, bool jump)
    {
        m_Body.velocity = new Vector2(speed * horizontal, m_Body.velocity.y);

        if (jump && m_Ground) //Jump when button is pressed
        {
            m_Body.velocity = new Vector2(m_Body.velocity.x, jumpForce);
        }
        if (Mathf.Abs(horizontal) < walkDeadZone)
        {
            m_Anim.SetBool("Walk", false);
        }
        else
        {
            m_Anim.SetBool("Walk", true);
        }
    }
}