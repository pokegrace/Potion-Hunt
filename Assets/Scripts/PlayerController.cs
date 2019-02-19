using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprender;
    private GameObject fence;

    private float moveForce = 10f;
    private float maxSpeed = 4f;
    private int numBlue = 0;
    private int numRed = 0;

    private Scene scene;
    private string sceneName;

    [SerializeField] private GameObject particleHolder;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprender = GetComponent<SpriteRenderer>();
        fence = GameObject.FindGameObjectWithTag("Fence");

        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
    }
    
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector2.left * moveForce, ForceMode2D.Force);
            animator.SetTrigger("MoveTrigger");
            sprender.flipX = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(Vector2.right * moveForce, ForceMode2D.Force);
            animator.SetTrigger("MoveTrigger");
            sprender.flipX = false;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(Vector2.up * moveForce, ForceMode2D.Force);
            animator.SetTrigger("MoveTrigger");
            sprender.flipX = false;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(Vector2.down * moveForce, ForceMode2D.Force);
            animator.SetTrigger("MoveTrigger");
            sprender.flipX = false;
        }
        // diagonal movement
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector2.up * moveForce, ForceMode2D.Force);
            animator.SetTrigger("MoveTrigger");
            sprender.flipX = true;
        }
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector2.down * moveForce, ForceMode2D.Force);
            animator.SetTrigger("MoveTrigger");
            sprender.flipX = true;
        }

        /*if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(0, 0);
        }*/

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Red Potion"))
        {
            if(sceneName.Equals("FirstScene"))
            {
                SceneManager.LoadScene("TitleScene");
            }
            else
            {
                GameObject particles = Instantiate(particleHolder, other.transform.position, other.transform.rotation);
                particles.GetComponent<ParticleSystem>().Play();
                ++numRed;
                StartCoroutine("DestroyLoot", other.gameObject);

                if (numRed >= 4)
                    fence.GetComponent<Animator>().SetTrigger("OpenFence");
            }
        }
        else if (other.gameObject.tag.Equals("Blue Potion"))
        {
            if(sceneName.Equals("SecondScene"))
            {
                SceneManager.LoadScene("TitleScene");
            }
            else
            {
                GameObject particles = Instantiate(particleHolder, other.transform.position, other.transform.rotation);
                particles.GetComponent<ParticleSystem>().Play();
                ++numBlue;
                StartCoroutine("DestroyLoot", other.gameObject);

                if(numBlue >= 2)
                    fence.GetComponent<Animator>().SetTrigger("OpenFence");
            }
        }
        else if (other.gameObject.tag.Equals("Food"))
        {
            SceneManager.LoadScene("TitleScene");
        }
        else if(other.gameObject.name.Equals("NextRoomTrigger"))
        {
            SceneManager.LoadScene("SecondScene");
        }
        else if (other.gameObject.name.Equals("TitleTrigger"))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    IEnumerator DestroyLoot(GameObject loot)
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log(loot.name + " destroyed");
        Destroy(loot);
    }
}
