
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private string additionalSceneName = "game";
    private static bool _isGameSceneLoaded; 
    private void Awake()
    {
        // 检查附加场景是否已加载
        if (!_isGameSceneLoaded)
        {
            SceneManager.LoadScene(additionalSceneName, LoadSceneMode.Additive);
            _isGameSceneLoaded = true; // 标记已加载
        }
    }

    private bool IsSceneLoaded(string sceneName)
    {
        // 检查场景是否已经被加载
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}
