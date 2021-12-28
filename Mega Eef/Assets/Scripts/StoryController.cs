using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject storyPanel;
    [SerializeField] GameObject[] storyImages;
    [SerializeField] float waitTime = 60f;

    public bool tellingStory = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && tellingStory == true)
        {
            LoadMenu();
        }
    }

    public void StartAudio()
    {
        audioSource.Play();
    }

    public void LoadMenu()
    {
        tellingStory = false;
        audioSource.Stop();
        menuPanel.SetActive(true);
        storyPanel.SetActive(false);
        foreach (GameObject image in storyImages)
            image.SetActive(false);
        StartCoroutine(RestartScreen());
    }

    IEnumerator RestartScreen()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(0);
    }

    public void PlayButton(float waitTime)
    {
        StopCoroutine(RestartScreen());
        StartCoroutine(LoadSelectScreen(waitTime));
    }

    IEnumerator LoadSelectScreen(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(1);
    }

    public void SetStory()
    {
        tellingStory = true;
    }

    public void QuitGame()
    {
        print("You have Quit the game!");
        Application.Quit();
    }
}
