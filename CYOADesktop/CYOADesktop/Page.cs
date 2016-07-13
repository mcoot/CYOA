using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYOA.Structures
{
    class Page
    {

        private Dictionary<string, Page> branches;
        private List<string> lines;
        
        public ReadOnlyDictionary<string, Page> Branches
        {
            get
            {
                return new ReadOnlyDictionary<string, Page>(branches);
            }
        }

        public ReadOnlyCollection<string> Lines
        {
            get
            {
                return lines.AsReadOnly();
            }
        }

        public Page()
        {
            branches = new Dictionary<string, Page>();
            lines = new List<string>();
        }

        public void AddLine(string line)
        {
            lines.Add(line);
        }

        public void AddBranch(string option, Page page)
        {
            branches.Add(option, page);
        }

        public void AddBranch(string option)
        {
            AddBranch(option, null);
        }

       /// <summary>
       /// Set the page which a given option branches to
       /// </summary>
       /// <param name="option">The option branch to modify</param>
       /// <param name="page">The page to branch to</param>
       public void SetBranchPage(string option, Page page)
        {
            if (!(branches.ContainsKey(option))) {
                throw new ArgumentException(
                    String.Format("Branch with option name {0} does not exist", option), "option");
            }

            branches[option] = page;
        }

       public bool BranchExists(string option)
        {
            return Branches.ContainsKey(option);
        } 

       public bool IsDeadEnd()
        {
            return (branches.Count <= 0);
        }

    }
}
