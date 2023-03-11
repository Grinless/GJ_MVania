public class MenuAction_CloseSettingsMenu : MenuAction
{
    internal override void Action()
    {
        UI_PauseMenuManager.instance.ClosePauseMenu();
    }
}
