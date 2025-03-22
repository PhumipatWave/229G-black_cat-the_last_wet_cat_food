using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartLoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
