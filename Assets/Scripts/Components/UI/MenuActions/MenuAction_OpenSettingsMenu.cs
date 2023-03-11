public class MenuAction_OpenSettingsMenu : MenuAction
{
    internal override void Action()
    {
        UI_PauseMenuManager.instance.OpenSettingsMenu();
    }
}
