using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iKnow.Core.Models
{
    public class TopicUser
    {
        public int TopicId { get; set; }
        public Topic Topic { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
