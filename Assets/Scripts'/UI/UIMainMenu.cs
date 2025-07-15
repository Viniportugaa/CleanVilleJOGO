using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject levelScreen;
    [SerializeField] private GameObject mainmenuScreen;
    [SerializeField] private GameObject TitleImage;
    [SerializeField] private GameObject SpeciallevelScreen;

    private void Awake()
    {
        levelScreen.SetActive(false);
        mainmenuScreen.SetActive(true);
        TitleImage.SetActive(true);
        SpeciallevelScreen.SetActive(false);
    }
    //Quit game/exit play mode if in Editor
    public void Quit()
    {
        Application.Quit(); //Quits the game (only works in build)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode
#endif
    }
    public void SelectLevelPage()
    {
        mainmenuScreen.SetActive(false);
        TitleImage.SetActive(false);
        levelScreen.SetActive(true);
    }

    public void SelectLevelSpecialPage()
    {
        mainmenuScreen.SetActive(false);
        TitleImage.SetActive(false);
        levelScreen.SetActive(false);
        SpeciallevelScreen.SetActive(true);
    }


    public void ExitLevelPage()
    {
        levelScreen.SetActive(false);
        mainmenuScreen.SetActive(true);
        TitleImage.SetActive(true);
    }


    public void Play(int level)
    {
        SceneManager.LoadScene(level);
    }
}
