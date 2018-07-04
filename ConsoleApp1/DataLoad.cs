using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using ConsoleApp1.Quaries;

namespace ConsoleApp1
{
    class DataLoad
    {
        //получить всех пользователей, включая Post и Todo к-е к ним привязаны по id
        public static async Task<List<User>> GetUsers()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://5b128555d50a5c0014ef1204.mockapi.io/users");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var json = await responseContent.ReadAsStringAsync();

                var allusers = JsonConvert.DeserializeObject<List<User>>(json);
                var allposts = await GetPosts();
                var alltodos = await GetTodos();

                foreach(var user in allusers)
                {
                    user.Posts = new List<Post>();
                    user.Todos = new List<Todo>();

                    foreach(var post in allposts)
                    {
                        if(post.UserId == user.Id)
                        {
                            user.Posts.Add(post);
                        }
                    }

                    foreach (var todo in alltodos)
                    {
                        if (todo.UserId == user.Id)
                        {
                            user.Todos.Add(todo);
                        }
                    }
                }
                return allusers;
            }
            else
            {
                throw new Exception("Bad request!");
            }
        }
        //получить все посты, включая комменты к-е привязаны к ним по id
        public static async Task<List<Post>> GetPosts()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://5b128555d50a5c0014ef1204.mockapi.io/posts");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var json = await responseContent.ReadAsStringAsync();

                var allposts = JsonConvert.DeserializeObject<List<Post>>(json);
                var allcomments = await GetComments();

                foreach (var post in allposts)
                {
                    post.Comments = new List<Comment>();

                    foreach (var comment in allcomments)
                    {
                        if (comment.PostId == post.Id)
                        {
                            post.Comments.Add(comment);
                        }
                    }
                }
                return allposts;
            }
            else
            {
                throw new Exception("Bad request!");
            }
        }
        //Получить все Todo
        public static async Task<List<Todo>> GetTodos()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://5b128555d50a5c0014ef1204.mockapi.io/todos");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var json = await responseContent.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<Todo>>(json);
            }
            else
            {
                throw new Exception("Bad request!");
            }
        }
        //Получить все комменты
        public static async Task<List<Comment>> GetComments()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://5b128555d50a5c0014ef1204.mockapi.io/comments");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var json = await responseContent.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<Comment>>(json);
            }
            else
            {
                throw new Exception("Bad request!");
            }
        }
        //Получить количество комментов под постами конкретного пользователя(по айди) (список из пост-количество)
        public static async Task<List<AmountComment>> GetAmountComment(int id)
        {
            List<AmountComment> amountComments = new List<AmountComment>();
            var allposts = await DataLoad.GetPosts();
            allposts = allposts.Where(x => x.UserId == id).ToList<Post>();

            foreach(var posts in allposts)
            {
                uint i = 0;
                foreach(var comment in posts.Comments)
                {
                    i++;
                }
                amountComments.Add(new AmountComment { post = posts,  count = i});
            }
            return amountComments;
        }
        //Получить список комментов под постами конкретного пользователя (по айди), где body коммента < 50 символов (список из комментов)
        public static async Task<List<Comment>> GetCommentList(int id)
        {
            var allcomment = await DataLoad.GetComments();
            return allcomment = allcomment.Where(x => x.UserId == id && x.Body.Length < 50).ToList<Comment>();
        }
        //Получить список(id, name) из списка todos которые выполнены для конкретного пользователя(по айди)
        public static async Task<List<TodoList>> GetTodosList(int id)
        {
            List<TodoList> todoLists = new List<TodoList>();
            var alltodos = await DataLoad.GetTodos();
            alltodos = alltodos.Where(x => x.UserId == id).ToList<Todo>();

            foreach(var todo in alltodos)
                todoLists.Add(new TodoList { Id = todo.Id, Name = todo.Name });

            return todoLists;
        }
        //Получить список пользователей по алфавиту (по возрастанию) с отсортированными todo items по длине name (по убыванию)
        public static async Task<List<User>> GetUserByAlphabets()
        {
            var allusers = await DataLoad.GetUsers();
            allusers = allusers.OrderBy(x => x.Name).ToList<User>();

            foreach(var item in allusers)
            {
                item.Todos = item.Todos.OrderByDescending(x => x.Name.Length).ToList<Todo>();
            }
            return allusers;
        }
        /*Получить следующую структуру(передать Id пользователя в параметры)
        User
        Последний пост пользователя(по дате)
        Количество комментов под последним постом
        Количество невыполненных тасков для пользователя
        Самый популярный пост пользователя(там где больше всего комментов с длиной текста больше 80 символов)
        Самый популярный пост пользователя(там где больше всего лайков)*/
        public static async Task<List<UserSelection>> GetUserSelections(int id)
        {
            List<UserSelection> userSelections = new List<UserSelection>();
            var allusers = await DataLoad.GetUsers();
            
            foreach(var item in allusers.Where(x => x.Id == id))
            {
                Post last = item.Posts.OrderByDescending(x => x.CreatedAt).First();
                uint amountComment = 0;
                uint amountTask = 0;

                foreach(var post in item.Posts.Where(x => x.CreatedAt == last.CreatedAt))
                {
                    foreach (var comment in post.Comments)
                        amountComment++;
                }

                foreach(var task in item.Todos)
                {
                    if (task.IsComplete == false)
                        amountTask++;
                }

                userSelections.Add(new UserSelection
                                 {
                                     user = item,
                                     lastPost = last,
                                     commentByLastPost = amountComment,
                                     unrealizedTask = amountTask,
                                     mostCommentedPost = item.Posts.OrderByDescending(x => x.Comments.Count).First(),
                                     postByMostLike = item.Posts.OrderByDescending(x => x.Likes).First()
                                 });
            }
            return userSelections;
        }
        /*Получить следующую структуру (передать Id поста в параметры)
        Пост
        Самый длинный коммент поста
        Самый залайканный коммент поста
        Количество комментов под постом где или 0 лайков или длина текста < 80*/
        public static async Task<List<PostSelection>> GetPostSelections(int id)
        {
            List<PostSelection> postSelections = new List<PostSelection>();
            var allposts = await DataLoad.GetPosts();

            foreach(var item in allposts.Where(x => x.Id == id))
            {
                var comment = item.Comments.Where(x => x.Likes == 0 || x.Body.Length > 80).ToList<Comment>();

                postSelections.Add(new PostSelection
                                    {
                                        post = item,
                                        LongestComment = item.Comments.OrderByDescending(x => x.Body.Length).First(),
                                        CommentByMostLike = item.Comments.OrderByDescending(x => x.Likes).First(),
                                        CommentAmount = (uint)comment.Count
                                    });
            }

            return postSelections;
        }
    }
}
