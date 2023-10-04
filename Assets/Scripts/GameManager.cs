using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int bulletsShot;

    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectsOfType<GameManager>().Length > 1) { Destroy(gameObject); }
        else { DontDestroyOnLoad(gameObject); }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            if (SceneManager.GetActiveScene().buildIndex == 2) { LoadScene(0); }
            else { RestartLevel(); }
        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CountBullet()
    {
        bulletsShot++;
    }

    public int GetBulletCount()
    {
        return bulletsShot;
    }
}
