using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Quaries
{
    class PostSelection
    {
        public Post post { get; set; }
        public Comment LongestComment { get; set; }
        public Comment CommentByMostLike { get; set; }
        public uint CommentAmount { get; set; }
    }
}