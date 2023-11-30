using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class bossScript: MonoBehaviour
{


    private Transform Playert;
    private Movement Player;
    private float health = 25;
    public Canvas endScreen;
    public GameObject player;
    private bool directionOfPlayer;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject skel;
    [SerializeField] private float speed;

    // Use this for initialization
    void Start()
    {

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        Playert = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 displacement = Playert.position - transform.position;
        displacement = displacement.normalized;
        if (Vector2.Distance(Playert.position, transform.position) > 1.0f)
        {
            transform.position += (displacement * speed * Time.deltaTime);

        }
        if (displacement.x < 0 && directionOfPlayer == true)
        {
            Vector3 localScale = transform.localScale;
            directionOfPlayer = !directionOfPlayer;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        else if (displacement.x > 0 && directionOfPlayer == false)
        {
            Vector3 localScale = transform.localScale;
            directionOfPlayer = !directionOfPlayer;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

    }
    private void FixedUpdate()
    {


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "hit" && Player.candamage)
        {
            health -= 1;
            //healthBar.fillAmount = health / 25;
            if (health == 0) {
                Destroy(skel);
                SceneManager.LoadScene("GameOver");
                player.SendMessage("GameOver");
            }
            speed = 6 - health/5;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "hurt")
        {
            Debug.Log("hit");
            Player.health--;
        }
    }


}