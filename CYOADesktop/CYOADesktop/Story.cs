using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYOAStructures
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

        public Story()
        {
            StartPage = null;
        }

        public void AddPage(Page page)
        {
            pages.Add(page);
        }

    }
}
