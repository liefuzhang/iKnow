using System.Collections.Generic;
using System.Linq;
using iKnow.Core.Models;

namespace iKnow.Core.ViewModels
{
    public class SearchTopicsFullResultViewModel
    {
        public IEnumerable<Topic> Topics { get; set; }

        public string Search { get; set; }

        public bool IsEmptyResult => Topics?.FirstOrDefault() == null;

        public string SearchType => nameof(SearchFullResultViewModel.Topic);
    }
}