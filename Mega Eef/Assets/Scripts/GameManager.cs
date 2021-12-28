using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerController player;
    [SerializeField] GameObject playerPrefab;
    LevelManager currentLevelManager;

    public bool[] charactersDefeated;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        player = Instantiate(playerPrefab).GetComponent<PlayerController>();
        player.gameObject.SetActive(false);

        SceneManager.sceneLoaded += SceneLoaded;
    }

    

    public void CharacterButtonSelected(int id)
    {
        if (charactersDefeated[id] == false)
        {
            // Start
            SceneManager.LoadScene(id + 3);
        }
        else
        {
            // Deny soundf
        }

        /*  LEVEL IDS:
         *  
         *  CODY  - 3
         *  JESSE - 4
         *  TB    - 5
         * 
        */
    }

    void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        currentLevelManager = FindObjectOfType<LevelManager>();
        player.transform.position = currentLevelManager.currentSpawnPoint.transform.position;
        player.gameObject.SetActive(true);
    }

    
}
