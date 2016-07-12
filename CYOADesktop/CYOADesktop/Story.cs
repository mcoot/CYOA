using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYOA.Structures
{
    class Story
    {

        private List<Page> pages;

        public ReadOnlyCollection<Page> Pages
        {
            get
            {
                return pages.AsReadOnly();
            }
        }

        public Page StartPage { get; set; }
        public string Title { get; set; }

        public Story(string title, Page start)
        {
            Title = title;
            StartPage = start;

            pages = new List<Page>();
        }

        public Story(string title)
            : this(title, null) {}

        public Story()
            : this("", null) {}

        public void AddPage(Page page)
        {
            pages.Add(page);
        }

        public Page getPageFromId(int id)
        {
            return pages[id];
        }

        public int GetPageId(Page page)
        {
            return pages.IndexOf(page);
        }

    }
}
