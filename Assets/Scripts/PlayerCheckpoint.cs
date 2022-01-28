using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    public GameObject player;
    public Vector2 checkpointPosition;
    public ParticleSystem blood;
    public SpriteRenderer spriteRend;

    public static PlayerCheckpoint instance;

    private void Awake()
    {
        checkpointPosition = new Vector2(0, 0);
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Death();
        }
    }
    private void OnTriggerStay2D(Collider2D checkpoint)
    {
        checkpointPosition = player.gameObject.transform.position;
    }
    public void Death()
    {
        spriteRend.color = new Color(0, 0, 0, 0.0f);
        print("Triggering Death Function");
        blood.gameObject.SetActive(true);
        Invoke("Respawn", 1);

    }
    public void Respawn()
    {
        print("Triggering Respawn Function");
        player.gameObject.transform.position = checkpointPosition;
        blood.gameObject.SetActive(false);
        spriteRend.color = Color.white;
    }
}
