using System;
using System.Collections.Generic;
using ConsoleApp1.Quaries;

namespace ConsoleApp1
{
    static class Menu
    {
        private static int index = 0;

        private static string drawMenu(List<string> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.WriteLine(items[i]);
                }
                else
                {
                    Console.WriteLine(items[i]);
                }
                Console.ResetColor();
            }

            ConsoleKeyInfo ckey = Console.ReadKey();

            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (index == items.Count - 1)
                {
                }
                else { index++; }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (index <= 0)
                {
                }
                else { index--; }
            }
            else if (ckey.Key == ConsoleKey.Enter)
            {
                return items[index];
            }
            else
            {
                return "";
            }

            Console.Clear();
            return "";
        }

        public static async void Start()
        {
            List<string> menuItems = new List<string>() {
                "Получить количество комментов под постами конкретного пользователя (по id)",
                "Получить список комментов под постами конкретного пользователя (по айди), где body коммента < 50 символов",
                "Получить список (id, name) из списка todos которые выполнены для конкретного пользователя (по id)",
                "Получить список пользователей по алфавиту (по возрастанию) с отсортированными todo items по длине name (по убыванию)",
                "Получить уникальную структуру User",
                "Получить уникальную структуру Post"
            };

            Console.CursorVisible = false;
            while (true)
            {
                string selectedMenuItem = Menu.drawMenu(menuItems);

                switch (selectedMenuItem)
                {
                    case "Получить количество комментов под постами конкретного пользователя (по id)":
                        {
                            Console.Clear();
                            Console.WriteLine("Enter id: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            var collection = await DataLoad.GetAmountComment(id);

                            foreach(var item in collection)
                            {
                                Console.WriteLine($"Post   : {item.post}");
                                Console.WriteLine($"Count  : {item.count}");

                                Console.WriteLine(new string('-', 85));
                            }

                            break;
                        }

                    case "Получить список комментов под постами конкретного пользователя (по айди), где body коммента < 50 символов":
                        {
                            Console.Clear();
                            Console.WriteLine("Enter id: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            var collection = await DataLoad.GetCommentList(id);

                            foreach (var item in collection)
                            {
                                Console.WriteLine($"Id     : {item.Id}");
                                Console.WriteLine($"Body   : {item.Body}");

                                Console.WriteLine(new string('-', 85));
                            }

                            break;
                        }

                    case "Получить список (id, name) из списка todos которые выполнены для конкретного пользователя (по id)":
                        {
                            Console.Clear();
                            Console.WriteLine("Enter id: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            var collection = await DataLoad.GetTodosList(id);

                            foreach (var item in collection)
                            {
                                Console.WriteLine($"Id     : {item.Id}");
                                Console.WriteLine($"Name   : {item.Name}");

                                Console.WriteLine(new string('-', 85));
                            }

                            break;
                        }

                    case "Получить список пользователей по алфавиту (по возрастанию) с отсортированными todo items по длине name (по убыванию)":
                        {
                            Console.Clear();
                            var collection = await DataLoad.GetUserByAlphabets();

                            foreach (var item in collection)
                            {
                                Console.WriteLine($"Id     : {item.Id}");
                                Console.WriteLine($"Name   : {item.Name}");

                                Console.WriteLine(new string('-', 85));
                            }

                            break;
                        }

                    case "Получить уникальную структуру User":
                        {
                            Console.Clear();
                            Console.WriteLine("Enter id: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            var collection = await DataLoad.GetUserSelections(id);

                            foreach (var item in collection)
                            {
                                Console.WriteLine($"Name           : {item.user.Name}");
                                Console.WriteLine($"Post           : {item.lastPost.Body}");
                                Console.WriteLine($"Most commented post:\n {item.mostCommentedPost.Title}");
                                Console.WriteLine($"Most liked post:\n {item.postByMostLike.Title}");
                                Console.WriteLine($"Unrealized task: {item.unrealizedTask}");

                                Console.WriteLine(new string('-', 85));
                            }

                            break;
                        }

                    case "Получить уникальную структуру Post":
                        {
                            Console.Clear();
                            Console.WriteLine("Enter id: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            var collection = await DataLoad.GetPostSelections(id);

                            foreach (var item in collection)
                            {
                                Console.WriteLine($"Post:\n {item.post.Title}");
                                Console.WriteLine($"Comment Amount                 : {item.CommentAmount}");
                                Console.WriteLine($"Comment by most like           : {item.CommentByMostLike}");
                                Console.WriteLine($"Longest comment:\n {item.LongestComment.Body}");

                                Console.WriteLine(new string('-', 85));
                            }

                            break;
                        }

                    case "Exit":
                        {
                            Environment.Exit(0);
                            break;
                        }
                }
            }
        }
    }
}