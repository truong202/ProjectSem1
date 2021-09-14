using System;
using System.Collections.Generic;
using Persistance;
using System.Text;

namespace ConsolePL
{
    public static class Utility
    {
        public static string[] GetTable(List<string[]> lines)
        {
            int[] lengthDatas = GetLength(lines);
            StringBuilder builder = new StringBuilder();
            builder.Append(GetLine(lengthDatas, "┌", "─", "┬", "┐\n"));
            for (int index = 0; index < lines.Count; index++)
            {
                var line = lines[index];
                builder.Append("│ ");
                for (int i = 0; i < line.Length - 1; i++)
                    builder.Append(line[i].PadRight(lengthDatas[i] + 1) + "│ ");
                builder.Append(line[line.Length - 1].PadRight(lengthDatas[line.Length - 1] + 1) + "│");
                builder.AppendLine();
                if (index < lines.Count - 1)
                    builder.Append(GetLine(lengthDatas, "├", "─", "┼", "┤\n"));
                else
                    builder.Append(GetLine(lengthDatas, "└", "─", "┴", "┘"));
            }
            string[] a = builder.ToString().Split('\n');
            return a;
        }
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
                Console.WriteLine(" Entered incorrectly");
                Console.ResetColor();
                Console.Write(" → Re-enter: ");
            }
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
                    Console.WriteLine(" " + e.Message);
                    Console.ResetColor();
                    Console.Write(" → Re-enter customer name: ");
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
                    Console.WriteLine(" " + e.Message);
                    Console.ResetColor();
                    Console.Write(" → Re-enter phone: ");
                }
            }
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

        private static string GetLine(int[] lengthDatas, string c1, string c2, string c3, string c4)
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
        public static void PrintColor(string content, ConsoleColor fColor, ConsoleColor bColor)
        {
            Console.BackgroundColor = bColor;
            Console.ForegroundColor = fColor;
            Console.Write(content);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(".\b");
            Console.ResetColor();
        }
        public static void PrintColor(string content, int posleft, ConsoleColor fColor, ConsoleColor bColor)
        {
            Console.Write("{0," + (posleft - 1) + "}", "");
            Console.BackgroundColor = bColor;
            Console.ForegroundColor = fColor;
            Console.Write(content);
            Console.ResetColor();
            Console.ForegroundColor = Console.BackgroundColor;
            Console.Write(".\b");
            Console.ResetColor();
        }
        public static void PrintBorder(int width, int height)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < width; i++)
            {
                Console.Write("▄");
            }
            Console.WriteLine();
            for (int i = 1; i < height; i++)
            {
                Console.WriteLine("█{0," + (width - 1) + "}", "█");
            }
            Console.SetCursorPosition(0, height);
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