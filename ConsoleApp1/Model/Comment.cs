using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Comment
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public uint Likes { get; set; }
    }
}
