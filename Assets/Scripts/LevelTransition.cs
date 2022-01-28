using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public string Levelname;
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("test");
        SceneManager.LoadScene(Levelname);
    }
}
