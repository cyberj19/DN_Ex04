using System;

namespace Ex04.Menus.Test
{
    class Test : Interfaces.IMenuOptionChosenEventHandler
    {
        public enum eMenuOption
        {
            VersionAndSpaces,
            CountSpaces,
            ShowVersion,
            ShowDateOrTime,
            ShowDate,
            ShowTime
        }

        private readonly Interfaces.MainMenu r_InterfacesMainMenu;
        private readonly Delegates.MainMenu r_DelegatesMainMenu;

        public Test()
        {
            r_DelegatesMainMenu = new Delegates.MainMenu("Main Menu");
            r_InterfacesMainMenu = new Interfaces.MainMenu("Main Menu");
            initInterfaceTest();
            initDelegatesTest();
        }

        public void run()
        {
            r_InterfacesMainMenu.Show();
            r_DelegatesMainMenu.Show();
        }

        private void initDelegatesTest()
        {
            Delegates.MenuItem versionAndSpaces = r_DelegatesMainMenu.AddSubMenuItem("Version and Spaces", eMenuOption.VersionAndSpaces);
            Delegates.MenuItem showDateOrTime = r_DelegatesMainMenu.AddSubMenuItem("Show Date/Time", eMenuOption.ShowDateOrTime);
            Delegates.MenuItem countSpaces = versionAndSpaces.AddItem("Count Spaces", eMenuOption.CountSpaces);
            Delegates.MenuItem showVersion = versionAndSpaces.AddItem("Show Version", eMenuOption.ShowVersion);
            Delegates.MenuItem showTime = showDateOrTime.AddItem("Show Time", eMenuOption.ShowTime);
            Delegates.MenuItem showDate = showDateOrTime.AddItem("Show Date", eMenuOption.ShowDate);

            countSpaces.MenuOptionChosen += CountSpaces_MenuOptionChosen;
            showVersion.MenuOptionChosen += ShowVersion_MenuOptionChosen;
            showTime.MenuOptionChosen += ShowTime_MenuOptionChosen;
            showDate.MenuOptionChosen += ShowDate_MenuOptionChosen;
        }

        private void ShowDate_MenuOptionChosen()
        {
            showDate();
        }

        private void ShowTime_MenuOptionChosen()
        {
            showTime();
        }

        private void ShowVersion_MenuOptionChosen()
        {
            showVersion();
        }

        private void CountSpaces_MenuOptionChosen()
        {
            countSpaces();
        }

        private void initInterfaceTest()
        {
            addInterfaceMenuSubItems();
            r_InterfacesMainMenu.AddListener(this);
        }

        private void addInterfaceMenuSubItems()
        {
            Interfaces.MenuItem versionAndSpaces = r_InterfacesMainMenu.AddSubMenuItem("Version and Spaces", eMenuOption.VersionAndSpaces);
            Interfaces.MenuItem showDateOrTime = r_InterfacesMainMenu.AddSubMenuItem("Show Date/Time", eMenuOption.ShowDateOrTime);

            versionAndSpaces.AddItem("Count Spaces", eMenuOption.CountSpaces);
            versionAndSpaces.AddItem("Show Version", eMenuOption.ShowVersion);

            // todo: ask in forum whos first (II is first?...) this is a same todo for the add delegates sub items..
            showDateOrTime.AddItem("Show Time", eMenuOption.ShowTime);
            showDateOrTime.AddItem("Show Date", eMenuOption.ShowDate);
        }

        public void HandleMenuOptionSelection(object i_Identifier)
        {
            eMenuOption menuOption = (eMenuOption)i_Identifier;

            switch (menuOption)
            {
                case eMenuOption.CountSpaces:
                    countSpaces();
                    break;
                case eMenuOption.ShowVersion:
                    showVersion();
                    break;
                case eMenuOption.ShowDate:
                    showDate();
                    break;
                case eMenuOption.ShowTime:
                    showTime();
                    break;
            }
        }

        private void countSpaces()
        {
            string input;

            Console.WriteLine("Please insert text:");
            input = Console.ReadLine();
            Console.WriteLine("Amount of spaces: {0}", input.Split().Length - 1);
            waitForUser();
        }

        private void showVersion()
        {
            Console.WriteLine("Version: 15.3.4.0");
            waitForUser();
        }

        private void showDate()
        {
            Console.WriteLine(DateTime.Now.ToString());
            waitForUser();
        }

        private void showTime()
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
            waitForUser();
        }

        private void waitForUser()
        {
            Console.WriteLine("Please press enter to continue....");
            Console.ReadLine();
        }
    }
}
