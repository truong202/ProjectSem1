using System;
using System.Collections.Generic;
using Persistance;
using System.Text;

namespace ConsolePL
{
    public static class Utility
    {
        public static List<string> SplitLine(string data, int lengthLine)
        {
            List<string> lines = new List<string>();
            int startIndex = 0, startIndexFind = 0;
            int lastIndex = lengthLine, lengthString = 0;
            int index, preIndex;
            for (int i = 0; lengthString < data.Length; i++)
            {
                index = 0;
                do
                {
                    preIndex = index;
                    index = data.IndexOf(" ", startIndexFind);
                    startIndexFind = index + 1;
                } while (index < lastIndex && index != -1);
                if (preIndex == 0)
                {
                    if (index == -1 || index > lastIndex && data.Length - lengthString > lengthLine)
                    {
                        preIndex = startIndex + lengthLine;
                    }
                    if (data.Length - lengthString <= lengthLine)
                    {
                        preIndex = data.Length;
                    }
                }
                int length = preIndex - startIndex;
                lines.Add(data.Substring(startIndex, length));
                lengthString += lines[i].Length;
                lines[i] = lines[i].Trim();
                lastIndex = startIndex + lengthLine;
                startIndex = preIndex;
                startIndexFind = lengthString + 1;
            }
            return lines;
        }
        public static string Standardize(string value)
        {
            value = value.Trim();
            if (value.Length == 0) return "";
            string CapitalizeLetter = @"ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴÉÈẸẺẼÊẾỀỆỂỄÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠÚÙỤỦŨƯỨỪỰỬỮÍÌỊỈĨĐÝỲỴỶỸ";
            string LowercaseLetter = @"áàạảãâấầậẩẫăắằặẳẵéèẹẻẽêếềệểễóòọỏõôốồộổỗơớờợởỡúùụủũưứừựửữíìịỉĩđýỳỵỷỹ";
            int i = 0;
            while ((i = value.IndexOf("  ", i)) != -1) value = value.Remove(i, 1);
            char[] arr = value.ToCharArray();
            for (int index = 1; index < value.Length; index++)
            {
                if (arr[index - 1] == ' ')
                {
                    if (Char.IsLower(arr[index])) arr[index] = Char.ToUpper(arr[index]);
                    else
                    {
                        int found = LowercaseLetter.IndexOf(arr[index]);
                        if (found != -1) arr[index] = CapitalizeLetter[found];
                    }
                }
                else if (arr[index - 1] != ' ')
                {
                    if (char.IsUpper(arr[index])) arr[index] = char.ToLower(arr[index]);
                    else
                    {
                        int found = CapitalizeLetter.IndexOf(arr[index]);
                        if (found != -1) arr[index] = LowercaseLetter[found];
                    }
                }
            }
            if (Char.IsLower(arr[0])) arr[0] = Char.ToUpper(arr[0]);
            else
            {
                int found = LowercaseLetter.IndexOf(arr[0]);
                if (found != -1) arr[0] = CapitalizeLetter[found];
            }
            value = new string(arr);
            return value;
        }
        public static void ShowPageNumber(int pageCount, int page)
        {
            if (pageCount > 1)
            {
                string nextPage = (page > 0 && page < pageCount) ? "►" : " ";
                string prePage = (page > 1) ? "◄" : " ";
                string pages = prePage + $"      [{page}/{pageCount}]      " + nextPage;
                int position = 60 + pages.Length / 2;
                Console.WriteLine("{0," + position + "}", pages);
            }
        }
        public static int GetNumber(string msg, int numberStart)
        {
            int number;
            Console.CursorVisible = true;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out number) && number >= numberStart) return number;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  Invalid {msg}!");
                Console.ResetColor();
                Console.Write($"  → Re-enter {msg}: ");
            }
        }
        public static int GetNumber(string msg, int numberStart, out ConsoleKeyInfo keyInfo)
        {
            ConsoleKey key;
            string input;
            int value;
            while (true)
            {
                input = string.Empty;
                do
                {
                    keyInfo = Console.ReadKey(true);
                    key = keyInfo.Key;
                    if (key == ConsoleKey.Escape || key == ConsoleKey.RightArrow || key == ConsoleKey.LeftArrow )
                    {
                        Console.CursorVisible = true; return -1;
                    }
                    if (key == ConsoleKey.Backspace && input.Length > 0)
                    {
                        Console.Write("\b \b");
                        input = input[..^1];
                    }
                    else if (!char.IsControl(keyInfo.KeyChar) && keyInfo.KeyChar >= '0' && keyInfo.KeyChar <= '9')
                    {
                        Console.Write(keyInfo.KeyChar);
                        input += keyInfo.KeyChar;
                    }
                } while (key != ConsoleKey.Enter);
                if (int.TryParse(input, out value) && value >= numberStart) return value;
                Console.WriteLine();
                Utility.Write($"  → Invalid {msg}!", ConsoleColor.Red);
                Console.Write("\n  → Re-enter {0}: ", msg);
            }
        }
        public static void Write(string text, ConsoleColor textColor)
        {
            Console.ForegroundColor = textColor;
            Console.Write(text);
            Console.ResetColor();
        }
        public static string GetName()
        {
            string name;
            Console.CursorVisible = true;
            while (true)
            {
                name = Console.ReadLine();
                try
                {
                    Customer.CheckName(name); return name;
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  " + e.Message);
                    Console.ResetColor();
                    Console.Write("  → Re-enter customer name: ");
                }
            }
        }
        public static decimal GetMoney(out ConsoleKeyInfo keyInfo)
        {
            decimal money = 0;
            string moneyString;
            ConsoleKey key;
            while (true)
            {
                moneyString = string.Empty;
                do
                {
                    Console.CursorVisible = true;
                    keyInfo = Console.ReadKey(true);
                    Console.CursorVisible = false;
                    key = keyInfo.Key;
                    if (key == ConsoleKey.Escape || (key == ConsoleKey.X && (keyInfo.Modifiers & ConsoleModifiers.Control) != 0))
                    {
                        Console.CursorVisible = true; return -1;
                    }
                    if (key == ConsoleKey.Backspace && moneyString.Length > 0)
                    {
                        moneyString = moneyString[..^1];
                        decimal.TryParse(moneyString, out money);
                        if (moneyString.Length > 0)
                            Console.Write("\b\b  \r  → Enter money: {0:N0}", money);
                        else
                            Console.Write("\b \r  → Enter money: ");
                    }
                    else if (!char.IsControl(keyInfo.KeyChar) && keyInfo.KeyChar >= '0' && keyInfo.KeyChar <= '9'
                            && money.ToString().Length <= 27)
                    {
                        moneyString += keyInfo.KeyChar;
                        decimal.TryParse(moneyString, out money);
                        Console.Write("\r  → Enter money: {0:N0}", money);
                    }
                } while (key != ConsoleKey.Enter);
                if (decimal.TryParse(moneyString, out money))
                {
                    Console.CursorVisible = true;
                    Console.WriteLine();
                    return money;
                }
            }
        }
        public static string GetPhone()
        {
            string phone;
            Console.CursorVisible = true;
            while (true)
            {
                phone = Console.ReadLine();
                try
                {
                    Customer.CheckPhone(phone); return phone;
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  " + e.Message);
                    Console.ResetColor();
                    Console.Write("  → Re-enter phone: ");
                }
            }
        }
        public static string GetLine(int[] lengthDatas, string c1, string c2, string c3, string c4)
        {
            string line = c1;
            int column = lengthDatas.Length;
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j <= lengthDatas[i] + 1; j++)
                    line += c2;
                line += i < (column - 1) ? c3 : c4;
            }
            return line;
        }
        public static void PrintTitle(string title, bool lastLine)
        {
            string line = "══════════════════════════════════════════════════════════════════════════════════════════════════════════════════";
            int lengthLine = line.Length + 2;
            int posLeft = Utility.GetPosition(title, lengthLine);
            Console.WriteLine("  ╔{0}╗", line);
            Console.WriteLine("  ║{0," + (lengthLine - 1) + "}", "║");
            Console.Write("  ║{0," + (posLeft - 1) + "}", ""); Utility.Write(title, ConsoleColor.Green);
            Console.WriteLine("{0," + (lengthLine - title.Length - posLeft) + "}", "║");
            Console.WriteLine("  ║{0," + (lengthLine - 1) + "}", "║");
            if (lastLine)
                Console.WriteLine("  ╚{0}╝", line);
        }
        public static void PressAnyKey(string msg)
        {
            Console.CursorVisible = false;
            Console.WriteLine($"  Press any key to {msg}...");
            Console.ReadKey(true);
            Console.CursorVisible = true;
        }
        public static int GetPosition(string value, int width)
        {
            int pos = width / 2 - (value.Length % 2 == 0 ? value.Length / 2 : value.Length / 2 + 1);
            return pos;
        }
    }
}