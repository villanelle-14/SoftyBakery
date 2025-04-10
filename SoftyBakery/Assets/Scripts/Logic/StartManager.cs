using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public bool isListenStart = true;
    private void Update()
    {
        if (!MainUI.Instance.isPause && Input.anyKeyDown)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                return;
            }
            SceneManager.LoadScene("CookingField");
        }
    }
}
