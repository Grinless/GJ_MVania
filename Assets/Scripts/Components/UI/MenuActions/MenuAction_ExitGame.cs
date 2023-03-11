using UnityEngine;

public class MenuAction_ExitGame : MenuAction
{
    internal override void Action()
    {
        Application.Quit(); 
        ///TODO: if there is enough time left, build a game intro/menu screen and
        ///return there instead. 
    }
}