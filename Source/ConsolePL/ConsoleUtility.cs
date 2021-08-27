using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolePL
{
    public class ConsoleUtility
    {
        public static string GetMenu(string title, string[] menuItems)
        {
            StringBuilder builder = new StringBuilder();
            string line = "╟───────────────────────────────────────────────╢\n";
            int position = line.Length / 2 + title.Length / 2 - 1;
            builder.Append("╔═══════════════════════════════════════════════╗\n");
            builder.Append("║                                               ║\n");
            builder.Append(String.Format("║{0," + position + "}{1," + (line.Length - position - 2) + "}\n", title, "║"));
            builder.Append("║                                               ║\n");
            builder.Append(line);
            for (int index = 0; index < menuItems.Length; index++)
            {
                position = line.Length - menuItems[index].Length - (index + 1).ToString().Length - 6;
                builder.Append(String.Format("║  {0}. {1}{2," + position + "}\n", index + 1, menuItems[index], "║"));
                if (index < menuItems.Length - 1)
                {
                    builder.Append(line);
                }
            }
            builder.Append("╚═══════════════════════════════════════════════╝");
            return builder.ToString();
        }
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
        public static string[] LineFormat(string data, int lengthLine)
        {
            int lineCount = (data.Length % lengthLine == 0) ? data.Length / lengthLine : data.Length / lengthLine + 1;
            string[] lines = new string[lineCount];
            int startIndex = 0; int startIndexFind = 0;
            int lastIndex = lengthLine; int preIndex;
            int index = -1;
            for (int i = 0; i < lineCount; i++)
            {
                do
                {
                    preIndex = index;
                    index = data.IndexOf(" ", startIndexFind);
                    startIndexFind = index + 1;
                } while (index < lastIndex && index != -1);
                int length = preIndex - startIndex;
                lines[i] = data.Substring(startIndex, length);
                startIndex = preIndex + 1;
                if (i < lineCount - 1) lastIndex = startIndex + lengthLine;
                else
                    lastIndex = data.Length % lengthLine + startIndex;
            }
            return lines;
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
    }
}