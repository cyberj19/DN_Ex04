namespace Ex04.Menus.Delegates
{
    public class MainMenu
    {
        private readonly MenuItem r_MainMenuNode;

        public MainMenu(string i_Title)
        {
            r_MainMenuNode = new MenuItem(null, i_Title, null);
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
