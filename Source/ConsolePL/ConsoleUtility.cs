using System;
using System.Text;

namespace ConsolePL
{
    public class ConsoleUtility
    {
        public static string GetMenu(string title, string[] menuItems)
        {
            StringBuilder builder = new StringBuilder();        
            string line = "╟───────────────────────────────────────────────╢\n";
            int position = line.Length / 2 + title.Length / 2 -1;
            builder.Append("╔═══════════════════════════════════════════════╗\n");
            builder.Append("║                                               ║\n");
            builder.Append(String.Format("║{0," + position + "}{1," + (line.Length - position -2) + "}\n", title, "║"));
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

    }
}