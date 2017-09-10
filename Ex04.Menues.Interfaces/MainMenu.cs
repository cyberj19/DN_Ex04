using System.Collections.Generic;

namespace Ex04.Menus.Interfaces
{
    public class MainMenu
    {
        private readonly List<IMenuOptionChosenEventHandler> r_Listeners;
        private readonly MenuItem r_MainMenuNode;

        public MainMenu(string i_Title)
        {
            r_Listeners = new List<IMenuOptionChosenEventHandler>();
            r_MainMenuNode = new MenuItem(null, i_Title, null, r_Listeners);
        }

        // Add a listener. Will be notified on menu item choosing events
        public void AddListener(IMenuOptionChosenEventHandler i_Listener)
        {
            if (!r_Listeners.Contains(i_Listener))
            {
                r_Listeners.Add(i_Listener);
            }
        }

        public MenuItem AddSubMenuItem(string i_Caption, object i_Identifier)
        {
            return r_MainMenuNode.AddItem(i_Caption, i_Identifier);
        }
        
        // Show the menu. will return only when Exit was chosen (Or if there are no items in the menu)
        public void Show()
        {
            if (r_MainMenuNode.HasSubMenues)
            {
                r_MainMenuNode.Show();
            }
        }
    }
}
