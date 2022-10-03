using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseWindow;
    private bool isGamePaused = false;

    void Start()
    {

    }

    void Update()
    {
        //------------------------Pause Window dışında bir yere basarsa window kapansın----------------------
        if (isGamePaused && Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            if (!RectTransformUtility.RectangleContainsScreenPoint(pauseWindow.GetComponent<RectTransform>(), mousePos))
            {
                Time.timeScale = 1;
                pauseWindow.SetActive(false);
                isGamePaused = false;
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseWindow.SetActive(true);
        isGamePaused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        StartCoroutine(BirazBekleSonraDevam(1));
    }

    public void MainMenuyeDon()
    {
        Time.timeScale = 1;
        StartCoroutine(BirazBekleSonraDevam(0));
    }

    public void StartGame()
    {
        this.GetComponent<AudioSource>().Play();
        StartCoroutine(BirazBekleSonraDevam(1));
    }

    IEnumerator BirazBekleSonraDevam(int newSceneId)
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("bekledi");
        SceneManager.LoadScene(newSceneId);
    }
}
