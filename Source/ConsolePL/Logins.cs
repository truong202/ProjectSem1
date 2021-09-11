// using System;
// using Persistance;
// using BL;
// namespace ConsolePL
// {
//     public class Login
//     {
//         public static Staff Run()
//         {
//             string username, password;
//             StaffBL staffBL = new StaffBL();
//             Staff staff;
//             do
//             {
//                 Console.Clear();
//                 Console.WriteLine("╔═══════════════════════════════════════════════╗");
//                 Console.WriteLine("║                                               ║");
//                 Console.WriteLine("║                     LOGIN                     ║");
//                 Console.WriteLine("║                                               ║");
//                 Console.WriteLine("╚═══════════════════════════════════════════════╝");
//                 Console.Write("\n → Username: ");
//                 username = GetUsername();
//                 Console.Write(" → Password: ");
//                 password = GetPassword();
//                 staff = staffBL.Login(new Staff { Username = username, Password = password });
//                 if (staff == null)
//                 {
//                     Console.ForegroundColor = ConsoleColor.Red;
//                     Console.WriteLine("\n Incorrect Username or Password!"); Console.ResetColor();
//                     Console.Write(" Press any key to login again...");
//                     Console.ReadKey(true);
//                 }
//             } while (staff == null);
//             return staff;
//         }
//         private static string GetUsername()
//         {
//             string username;
//             while (true)
//             {
//                 username = Console.ReadLine();
//                 try
//                 {
//                     Staff.CheckUsername(username); return username;
//                 }
//                 catch (Exception e)
//                 {
//                     Console.ForegroundColor = ConsoleColor.Red;
//                     Console.WriteLine(" " + e.Message); Console.ResetColor();
//                     Console.Write(" → Re-enter Username: ");
//                 }
//             }
//         }
//         private static string GetPassword()
//         {
//             ConsoleKey key;
//             string pass;
//             while (true)
//             {
//                 pass = string.Empty;
//                 do
//                 {
//                     var keyInfo = Console.ReadKey(intercept: true);
//                     key = keyInfo.Key;

//                     if (key == ConsoleKey.Backspace && pass.Length > 0)
//                     {
//                         Console.Write("\b \b");
//                         pass = pass[..^1];
//                     }
//                     else if (!char.IsControl(keyInfo.KeyChar))
//                     {
//                         Console.Write("*");
//                         pass += keyInfo.KeyChar;
//                     }
//                 } while (key != ConsoleKey.Enter);
//                 try
//                 {
//                     Staff.CheckPassword(pass); return pass;
//                 }
//                 catch (Exception e)
//                 {
//                     Console.ForegroundColor = ConsoleColor.Red;
//                     Console.WriteLine();
//                     Console.WriteLine(" " + e.Message); Console.ResetColor();
//                     Console.Write(" → Re-enter Password: ");
//                 }
//             }
//         }
//     }
// }