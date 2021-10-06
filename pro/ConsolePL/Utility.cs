using System;
using System.Collections.Generic;
using Persistance;
using System.Text;

namespace ConsolePL
{
    public static class Utility
    {
        public static List<string> LineFormat(string data, int lengthLine)
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
        public static int GetNumber(int numberStart)
        {
            int number;
            Console.CursorVisible = true;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out number) && number >= numberStart) return number;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  Entered incorrectly");
                Console.ResetColor();
                Console.Write("  → Re-enter: ");
            }
        }
        public static void PrintNameStore(int posLeft, int postTop)
        {
            string[] name = { "╦   ╔═╗ ╔═╗ ╔╦╗ ╔═╗ ╔═╗    ╔═╗ ╔╦╗ ╔═╗ ╦═╗ ╔═╗",
                              "║   ╠═╣ ╠═╝  ║  ║ ║ ╠═╝    ╚═╗  ║  ║ ║ ╠╦╝ ╠╣ ",
                              "╩═╝ ╩ ╩ ╩    ╩  ╚═╝ ╩      ╚═╝  ╩  ╚═╝ ╝╚═ ╚═╝"};
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            for (int i = 0; i < name.Length; i++)
            {
                Console.SetCursorPosition(posLeft, postTop + i);
                Console.Write(name[i]);
            }
            Console.ResetColor();
        }
        public static void PrintBox(int width, int x, int y)
        {
            Write("┌", x, y); for (int i = 0; i < width - 2; i++) Console.Write("─"); Console.WriteLine("┐");
            Write("│", x, y + 1); Console.Write("{0," + (width - 1) + "}", "│");
            Write("└", x, y + 2); for (int i = 0; i < width - 2; i++) Console.Write("─"); Console.Write("┘");
        }
        public static void Write(string text, ConsoleColor textColor)
        {
            Console.ForegroundColor = textColor;
            Console.Write(text);
            Console.ResetColor();
        }
        public static void Write(string text, ConsoleColor textColor, ConsoleColor bColor)
        {
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = bColor;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void Write(string text, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }
        public static void Write(string text, ConsoleColor textColor, int x, int y)
        {
            Console.ForegroundColor = textColor;
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            Console.ResetColor();
        }
        public static void Write(string text, ConsoleColor textColor, ConsoleColor bColor, int x, int y)
        {
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = bColor;
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            Console.ResetColor();
        }
        public static ConsoleKeyInfo GetMoney(ref Decimal? value, string msg, ConsoleKey[] validKeys)
        {
            string input = string.Empty;
            ConsoleKey key;
            ConsoleKeyInfo keyInfo;
            decimal newValue = value ?? 0;
            if (value != null)
                input = value.ToString();
            do
            {
                Console.CursorVisible = true;
                keyInfo = Console.ReadKey(true);
                Console.CursorVisible = false;
                key = keyInfo.Key;
                if (key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input = input[..^1];
                    decimal.TryParse(input, out newValue);
                    if (input.Length > 0)
                        Console.Write("\b\b  \r  → Enter {0}: {1:N0}", msg, newValue);
                    else
                        Console.Write("\b \r  → Enter {0}: ", msg);
                }
                else if (keyInfo.KeyChar >= '0' && keyInfo.KeyChar <= '9' && newValue.ToString().Length <= 27)
                {
                    if (input.Length == 1 && input[0] == '0')
                    {
                        if (keyInfo.KeyChar != '0')
                        {
                            input = input[..^1];
                            Console.Write("\b \b");
                        }
                        else
                            continue;
                    }
                    input += keyInfo.KeyChar;
                    decimal.TryParse(input, out newValue);
                    Console.Write("\r  → Enter money: {0:N0}", value);
                }
                if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0 || Array.Exists(validKeys, ch => ch.Equals(key))) break;
            } while (key != ConsoleKey.Enter);
            value = newValue;
            return keyInfo;
        }
        public static ConsoleKeyInfo GetInt(ref int? value, ConsoleKey[] validKeys)
        {
            ConsoleKey key;
            string input = string.Empty;
            ConsoleKeyInfo keyInfo;
            int num;
            if (value != null)
                input = value.ToString();
            do
            {
                keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;
                if (key == ConsoleKey.Backspace && input.Length > 0)
                {
                    Console.Write("\b \b");
                    input = input[..^1];
                }
                else if (keyInfo.KeyChar >= '0' && keyInfo.KeyChar <= '9' && input.Length < 9)
                {
                    if (input.Length == 1 && input[0] == '0')
                    {
                        if (keyInfo.KeyChar != '0')
                        {
                            input = input[..^1];
                            Console.Write("\b \b");
                        }
                        else
                            continue;
                    }
                    Console.Write(keyInfo.KeyChar);
                    input += keyInfo.KeyChar;
                }
                if (input == "") value = null;
                else if (int.TryParse(input, out num)) value = num;
                if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0 || Array.Exists(validKeys, ch => ch.Equals(key))) break;
            } while (key != ConsoleKey.Enter);
            return keyInfo;
        }
        public static ConsoleKeyInfo GetPassword(ref string value, int length, ConsoleKey[] validKeys)
        {
            ConsoleKey key;
            ConsoleKeyInfo keyInfo;
            string subValue = value.Length <= length ? value : "..." + value.Substring(value.Length - length + 3, length - 3);
            do
            {
                Console.CursorVisible = true;
                keyInfo = Console.ReadKey(true);
                Console.CursorVisible = false;
                key = keyInfo.Key;
                if (key == ConsoleKey.Backspace && value.Length > 0)
                {
                    for (int i = 0; i < subValue.Length; i++)
                        Console.Write("\b \b");
                    value = value[..^1];
                    subValue = value.Length <= length ? value : "..." + value.Substring(value.Length - length + 3, length - 3);
                    Console.Write(subValue);
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    if (value.Length > 0)
                        for (int i = 0; i < subValue.Length; i++)
                            Console.Write("\b \b");
                    value += keyInfo.KeyChar;
                    subValue = value.Length <= length ? value : "..." + value.Substring(value.Length - length + 3, length - 3);
                    Console.Write(subValue);
                }
                if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0 || Array.Exists(validKeys, ch => ch.Equals(key))) break;
            } while (key != ConsoleKey.Enter);
            return keyInfo;
        }

        public static ConsoleKeyInfo GetString(ref string value, int length, ConsoleKey[] validKeys)
        {
            ConsoleKey key;
            ConsoleKeyInfo keyInfo;
            string subValue = value.Length <= length ? value : "..." + value.Substring(value.Length - length + 3, length - 3);
            do
            {
                Console.CursorVisible = true;
                keyInfo = Console.ReadKey(true);
                Console.CursorVisible = false;
                key = keyInfo.Key;
                if (key == ConsoleKey.Backspace && value.Length > 0)
                {
                    for (int i = 0; i < subValue.Length; i++)
                        Console.Write("\b \b");
                    value = value[..^1];
                    subValue = value.Length <= length ? value : "..." + value.Substring(value.Length - length + 3, length - 3);
                    Console.Write(subValue);
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    if (value.Length > 0)
                        for (int i = 0; i < subValue.Length; i++)
                            Console.Write("\b \b");
                    value += keyInfo.KeyChar;
                    subValue = value.Length <= length ? value : "..." + value.Substring(value.Length - length + 3, length - 3);
                    Console.Write(subValue);
                }
                if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0 || Array.Exists(validKeys, ch => ch.Equals(key))) break;
            } while (key != ConsoleKey.Enter);
            return keyInfo;
        }
        public static void ShowMessage(string[] message, ConsoleColor textColor)
        {
            string line = "┌──────────────────────────────────────────────────────────────┐";
            int posLeft = Utility.GetPosition(line, 120);
            Utility.Write("┌──────────────────────────────────────────────────────────────┐\n", textColor, posLeft, 10);
            Utility.Write("│                                                              │\n", textColor, posLeft, Console.CursorTop);
            Utility.Write("│                                                              │\n", textColor, posLeft, Console.CursorTop);
            Utility.Write("│                                                              │\n", textColor, posLeft, Console.CursorTop);
            for (int i = 0; i < message.Length; i++)
            {
                int position = Utility.GetPosition(message[i], 120);
                Utility.Write("│                                                              │", textColor, posLeft, Console.CursorTop);
                Utility.Write(message[i], textColor, position, Console.CursorTop);
                Console.WriteLine();
            }
            Utility.Write("│                                                              │\n", textColor, posLeft, Console.CursorTop);
            Utility.Write("│                                                              │", textColor, posLeft, Console.CursorTop);
            Utility.Write("                  Press any key to continue\n", posLeft + 1, Console.CursorTop);
            Utility.Write("│                                                              │\n", textColor, posLeft, Console.CursorTop);
            Utility.Write("│                                                              │\n", textColor, posLeft, Console.CursorTop);
            Utility.Write("└──────────────────────────────────────────────────────────────┘\n", textColor, posLeft, Console.CursorTop);
            Console.ReadKey(true);
            Utility.Clear(posLeft, 10, line.Length, Console.CursorTop - 10);
        }
        private static int[] GetLength(List<string[]> lines)
        {
            var numElements = lines[0].Length;
            var lengthDatas = new int[numElements];
            for (int column = 0; column < numElements; column++)
            {
                lengthDatas[column] = lines[0][column].Length;
                for (int rows = 1; rows < lines.Count; rows++)
                    if (lengthDatas[column] <= lines[rows][column].Length) lengthDatas[column] = lines[rows][column].Length;
            }
            return lengthDatas;
        }
        public static void Clear(int left, int top, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(left, top + i);
                Console.Write("{0," + width + "}", "");
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

        public static void ShowPageNumber(int page, int pageCount, int status, int width)
        {
            int posLeft = Utility.GetPosition($"<<      [{page}/{pageCount}]      >>", width);
            ConsoleColor textColor = ConsoleColor.Cyan;
            ConsoleColor backgroundDefault = ConsoleColor.Black;
            ConsoleColor background = ConsoleColor.Red;
            Console.SetCursorPosition(posLeft, Console.CursorTop);
            if (page > 0 && pageCount > 1)
            {
                if (page == 1)
                {
                    switch (status)
                    {
                        case 0:
                            Console.Write($"        [{page}/{pageCount}]      ");
                            Utility.Write(">>", textColor, backgroundDefault);
                            break;
                        case 1:
                            Console.Write($"        [{page}/{pageCount}]      ");
                            Utility.Write(">>", textColor, background);
                            break;
                    }
                }
                else if (page > 1 && page < pageCount)
                {
                    switch (status)
                    {
                        case -1:
                            Utility.Write("<<", textColor, background);
                            Console.Write($"      [{page}/{pageCount}]      ");
                            Utility.Write(">>", textColor, backgroundDefault);
                            break;
                        case 0:
                            Utility.Write("<<", textColor, backgroundDefault);
                            Console.Write($"      [{page}/{pageCount}]      ");
                            Utility.Write(">>", textColor, backgroundDefault);
                            break;
                        case 1:
                            Utility.Write("<<", textColor, backgroundDefault);
                            Console.Write($"      [{page}/{pageCount}]      ");
                            Utility.Write(">>", textColor, background);
                            break;
                    }
                }
                else if (page == pageCount)
                {
                    switch (status)
                    {
                        case -1:
                            Utility.Write("<<", textColor, background);
                            Console.Write($"      [{page}/{pageCount}]        ");
                            break;
                        case 0:
                            Utility.Write("<<", textColor, backgroundDefault);
                            Console.Write($"      [{page}/{pageCount}]        ");
                            break;
                    }
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
                    if (key == ConsoleKey.Escape || key == ConsoleKey.RightArrow || key == ConsoleKey.LeftArrow || key == ConsoleKey.DownArrow
                        || key == ConsoleKey.UpArrow || (key == ConsoleKey.X && (keyInfo.Modifiers & ConsoleModifiers.Control) != 0))
                    {
                        return -1;
                    }
                    if (key == ConsoleKey.Backspace && moneyString.Length > 0)
                    {
                        moneyString = moneyString[..^1];
                        for (int i = 0; i < money.ToString("N0").Length; i++)
                            Console.Write("\b \b");
                        if (moneyString.Length > 0)
                        {
                            decimal.TryParse(moneyString, out money);
                            Console.Write("{0:N0}", money);
                        }
                    }
                    else if (!char.IsControl(keyInfo.KeyChar) && keyInfo.KeyChar >= '0' && keyInfo.KeyChar <= '9'
                            && money.ToString().Length <= 27)
                    {
                        if (moneyString.Length > 0)
                        {
                            for (int i = 0; i < money.ToString("N0").Length; i++)
                                Console.Write("\b \b");
                        }
                        moneyString += keyInfo.KeyChar;
                        decimal.TryParse(moneyString, out money);
                        Console.Write("{0:N0}", money);
                    }
                } while (key != ConsoleKey.Enter);
                if (decimal.TryParse(moneyString, out money))
                {
                    Console.WriteLine();
                    return money;
                }
            }
        }
        public static void PrintTitle(string title, bool lastLine)
        {
            string line = "══════════════════════════════════════════════════════════════════════════════════════════════════════════════════";
            int lengthLine = line.Length + 2;
            int posLeft = Utility.GetPosition(title, lengthLine);
            Console.WriteLine("  ╔{0}╗", line);
            Console.WriteLine("  ║{0," + (lengthLine - 1) + "}", "║");
            Console.Write("  ║{0," + (posLeft - 1) + "}", ""); Utility.Write(title, ConsoleColor.Green, ConsoleColor.Black);
            Console.WriteLine("{0," + (lengthLine - title.Length - posLeft) + "}", "║");
            Console.WriteLine("  ║{0," + (lengthLine - 1) + "}", "║");
            if (lastLine)
                Console.WriteLine("  ╚{0}╝", line);
        }
        public static void PrintBorder(int width, int height)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < width; i++)
            {
                Console.Write("▄");
            }
            Console.WriteLine();
            for (int i = 1; i < height; i++)
            {

                Console.Write("█");
                Console.SetCursorPosition(width - 1, Console.CursorTop);
                Console.WriteLine("█");
            }
            for (int i = 0; i < width; i++)
            {
                Console.Write("▀");
            }
            Console.ResetColor();
        }
        public static int GetPosition(string value, int width)
        {
            int pos = width / 2 - (value.Length % 2 == 0 ? value.Length / 2 : value.Length / 2 + 1);
            return pos;
        }
    }
}