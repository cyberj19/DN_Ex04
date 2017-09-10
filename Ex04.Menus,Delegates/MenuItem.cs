using System;
using System.Text;
using System.Collections.Generic;

namespace Ex04.Menus.Delegates
{
    // todo: is ok outside class?
    public delegate void MenuOptionChosenEventHandler();

    public class MenuItem
    {
        private const int k_LengthAndMaxOffsetDifference = 1;
        private const uint k_firstItemIndex = 0;
        private const int k_NeverGenerated = -1;
        private const int k_MenuItemStartOffset = 1;
        private const string k_BackStr = "(0) Back";
        private const string k_ExitStr = "(0) Exit";
        private readonly object r_Identifier;
        private readonly MenuItem r_Parent;
        private readonly string r_Caption;
        private int m_LastNumberOfItemsWhenOutputStringGenerated;
        private List<MenuItem> m_SubMenues; // We access the items more often than inserting new items.
        private string m_LastGeneratedChoiceStr;

        public event MenuOptionChosenEventHandler MenuOptionChosen;

        public bool HasSubMenues
        {
            get
            {
                return m_SubMenues.Count > 1;
            }
        }

        public string Caption
        {
            get
            {
                return r_Caption;
            }
        }

        public object Identifier
        {
            get
            {
                return r_Identifier;
            }
        }

        public MenuItem(MenuItem i_Parent, string i_Caption, object i_Identifier)
        {
            r_Parent = i_Parent;
            r_Caption = i_Caption;
            r_Identifier = i_Identifier;
            m_LastNumberOfItemsWhenOutputStringGenerated = k_NeverGenerated;
            m_LastGeneratedChoiceStr = null;
            m_SubMenues = new List<MenuItem>();
            m_SubMenues.Add(i_Parent);
        }

        // Shows MenuItem's options
        public void Show()
        {
            if (!HasSubMenues)
            {
                ConsoleUtils.ClearScreen();
                notifyListeners();
            }
            else
            {
                MenuItem currChoice = null;

                while (true)
                {
                    currChoice = getMenuItemChoice();
                    if (r_Parent == currChoice)
                    {
                        break;
                    }

                    currChoice.Show();
                }
            }
        }

        // Notify all listeners that this menu item was chosen
        private void notifyListeners()
        {
            if (MenuOptionChosen != null)
            {
                MenuOptionChosen.Invoke();
            }
        }

        // Add a sub menu item
        public MenuItem AddItem(string i_Caption, object i_Identifier)
        {
            MenuItem newItem = new MenuItem(this, i_Caption, i_Identifier);

            m_SubMenues.Add(newItem);

            return newItem;
        }

        // Print the choice-str and ask user for input. Process the input and returns the choice
        private MenuItem getMenuItemChoice()
        {
            int menuItemIndex;
            PositiveRange itemsRange = new PositiveRange(k_firstItemIndex, (uint)m_SubMenues.Count - k_LengthAndMaxOffsetDifference);

            ConsoleUtils.ClearScreen();
            updateMenuStringIfNeccesary();
            writeTitle();
            menuItemIndex = (int)ConsoleUtils.GetPositiveIntInRange(m_LastGeneratedChoiceStr, itemsRange);

            return m_SubMenues[menuItemIndex];
        }

        // Print the title
        private void writeTitle()
        {
            ConsoleUtils.WriteString(Caption);
            ConsoleUtils.NewLine();
        }

        // Updates the menu choice string only if the number of items has changed (So we wont create this string on every print call)
        private void updateMenuStringIfNeccesary()
        {
            int numberOfMenuItems = m_SubMenues.Count;

            if (m_LastNumberOfItemsWhenOutputStringGenerated != numberOfMenuItems)
            {
                m_LastGeneratedChoiceStr = generateChoiceStr();
                m_LastNumberOfItemsWhenOutputStringGenerated = numberOfMenuItems;
            }
        }

        // Generate the current menu-node choice string
        private string generateChoiceStr()
        {
            StringBuilder choiceStrBuilder = new StringBuilder();

            if (r_Parent == null)
            {
                choiceStrBuilder.AppendLine(k_ExitStr);
            }
            else
            {
                choiceStrBuilder.AppendLine(k_BackStr);
            }

            for (int i = k_MenuItemStartOffset; i < m_SubMenues.Count; i++)
            {
                choiceStrBuilder.AppendFormat("({0}) {1} {2}", i, m_SubMenues[i].Caption, Environment.NewLine);
            }

            return choiceStrBuilder.ToString();
        }
    }
}