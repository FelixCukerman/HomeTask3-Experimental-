using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Quaries
{
    class UserSelection
    {
        public User user { get; set; }
        public Post lastPost { get; set; }
        public uint commentByLastPost { get; set; }
        public uint unrealizedTask { get; set; }
        public Post mostCommentedPost { get; set; }
        public Post postByMostLike { get; set; }
    }
}
