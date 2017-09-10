using System;

namespace Ex04.Menus.Interfaces
{
    static class ConsoleUtils
    {
        // Clear the screen
        public static void ClearScreen()
        {
            Console.Clear();
        }

        // Get a string from the user
        public static string GetString(string i_MsgStr)
        {
            string inputStr = string.Empty;

            Console.WriteLine(i_MsgStr);

            while (inputStr.Length == 0)
            {
                inputStr = Console.ReadLine();
                inputStr = inputStr.Trim();
            }

            return inputStr;
        }
        
        // Write new line
        public static void NewLine()
        {
            WriteString(string.Empty);
        }
        
        // Get a positive number from the user. If the user inserts 'i_ExcludingStr', the function will return null instead
        public static uint GetPositiveIntInRange(string i_MessageForUser, PositiveRange i_InputRange)
        {
            string outStr = string.Format(
                "{0} {1}Please choose (Range: {2}-{3}):", 
                i_MessageForUser, 
                Environment.NewLine, 
                i_InputRange.Min, 
                i_InputRange.Max);
            uint retNum = GetObjectFromUser<uint>(outStr);

            while (!i_InputRange.IsInRange(retNum))
            {
                retNum = GetObjectFromUser<uint>("Input is not in range. Please insert again:");
            }

            return retNum;
        }

        // Write a string
        public static void WriteString(string i_Str)
        {
            Console.WriteLine(i_Str);
        }

        // Get type from user (With casting)
        public static T GetObjectFromUser<T>(string i_RequestStr)
        {
            return (T)GetObjectFromUser(i_RequestStr, typeof(T));
        }

        // Get type from user
        public static object GetObjectFromUser(string i_RequestStr, Type i_Type)
        {
            object retObj = null;
            string requestStr = i_RequestStr;
            string errStr = "Bad input was inserted. Please insert again:";

            while (true)
            {
                try
                {
                    retObj = Convert.ChangeType(GetString(requestStr), i_Type);
                    break;
                }
                catch (Exception)
                {
                    requestStr = errStr;
                }
            }

            return retObj;
        }
    }
}
