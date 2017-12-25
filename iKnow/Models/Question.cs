﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iKnow.Models {
    public class Question {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

    }
}