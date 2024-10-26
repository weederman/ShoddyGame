using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public void gameover()
    {
        Time.timeScale = 1f;
        GameManager.Instance.GameOver();
        SceneManager.LoadScene("StartScene");
        gameObject.SetActive(false);
    }
}
