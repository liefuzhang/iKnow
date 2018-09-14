using System.Collections.Generic;
using System.Linq;
using iKnow.Core.Models;

namespace iKnow.Core.ViewModels
{
    public class SearchUsersFullResultViewModel
    {
        public IEnumerable<AppUser> Users { get; set; }

        public string Search { get; set; }

        public bool IsEmptyResult => Users?.FirstOrDefault() == null;

        public string SearchType => nameof(SearchFullResultViewModel.User);
    }
}