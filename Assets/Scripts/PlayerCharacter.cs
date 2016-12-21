using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour
{

    [SerializeField]
    float speed;
    [SerializeField]
    bool isTurnedRight = true;
    [SerializeField]
    SpriteRenderer m_SpriteRenderer;
    [SerializeField]
    float jumpForce = 250f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {

    }
    public void Move(float horizontal, bool jump)
    {
        transform.position += new Vector3(speed * horizontal * Time.deltaTime, 0.0f, 0.0f);

        if (horizontal > 0 && !isTurnedRight)
        {
            Flip();
        }
        else if (horizontal < 0 && isTurnedRight)
        {
            Flip();
        }

        if (jump) //Jump when button is pressed
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector3(0.0f, jumpForce, 0.0f));
        }
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        //m_SpriteRenderer.flipX = !m_SpriteRenderer.flipX;

        isTurnedRight = !isTurnedRight;
    }
}