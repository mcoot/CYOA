using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace CYOA.Structures
{
    class StoryReader
    {

        public static Story Read(string filename)
        {
            XmlReader baseReader = XmlReader.Create(filename);

            Story story = new Story();
            var pageList = new List<Page>();
            var branchIds = new List<Dictionary<string, int>>();

            baseReader.MoveToContent();
            //baseReader.Read();
            
            // Check the XML document is a story
            if (baseReader.EOF || baseReader.ReadState != ReadState.Interactive)
            {
                throw new FormatException("XML document empty");
            }
            if (baseReader.NodeType != XmlNodeType.Element || !baseReader.Name.Equals("Story"))
            {
                throw new FormatException("XML document is not a story");
            }

            // Read the story title
            string storyTitle = baseReader["Title"];
            if (storyTitle == null)
            {
                throw new FormatException("Story missing a title");
            }
            story.Title = storyTitle;

            // Read the number of pages
            int numPages;
            if (baseReader["NumPages"] == null || !int.TryParse(baseReader["NumPages"], out numPages))
            {
                throw new FormatException("Story missing page count");
            }

            // Set up the page and branch lists
            for (int i = 0; i < numPages; ++i)
            {
                pageList.Add(null);
                branchIds.Add(null);
            }

            // Read the id of the starting page
            int storyStartId;
            if (baseReader["StartId"] == null || !int.TryParse(baseReader["StartId"], out storyStartId))
            {
                throw new FormatException("Story missing a start page id");
            }

            // Read all pages in the story, story branch ids for later
            XmlReader pageReader;
            while (baseReader.Read())
            {
                if (baseReader.IsStartElement() && baseReader.Name.Equals("Page"))
                {
                    pageReader = baseReader.ReadSubtree();
                    pageReader.Read();
                    ReadPage(pageReader, pageList, branchIds);
                    pageReader.Close();
                }
            }

            baseReader.Close();

            // Check all page ids were accounted for
            if (pageList.Contains(null) || branchIds.Contains(null) || pageList.Count != branchIds.Count)
            {
                throw new FormatException("Story missing sequential pages");
            }

            // Setup the story, and links between page branches
            SetupStoryPages(story, pageList, branchIds, storyStartId);

            return story;
        }

        private static void ReadPage(XmlReader pageReader, List<Page> pageList, List<Dictionary<string, int>> branchIds)
        {
            Page page = new Page();

            // Read the page's id
            int pageId;
            if (pageReader["Id"] == null || !int.TryParse(pageReader["Id"], out pageId))
            {
                throw new Exception("Invalid page id");
            }

            // Setup the page storage
            pageList[pageId] = page;

            // Setup the branch storage
            var branches = new Dictionary<string, int>();
            branchIds[pageId] = branches;

            // Read all lines and branches
            pageReader.Read();
            while (!pageReader.EOF)
            {
                if (pageReader.NodeType == XmlNodeType.Element)
                {
                    switch (pageReader.Name)
                    {
                        case "Line":
                            // Add the line
                            page.AddLine(pageReader.ReadInnerXml());
                            break;

                        case "Branch":
                            // Add the branch to storage for later fixing
                            int branchPageId;
                            if (pageReader["LinkId"] == null || !int.TryParse(pageReader["LinkId"], out branchPageId))
                            {
                                throw new FormatException("Invalid branch page id");
                            }
                            branches.Add(pageReader.ReadInnerXml(), branchPageId);
                            break;
                    }
                }
                else
                {
                    pageReader.Read();
                }
            }
        }

        private static void SetupStoryPages(Story story, List<Page> pageList, List<Dictionary<string, int>> branchIds, int startPageId)
        {
            for (int i = 0; i < pageList.Count; ++i)
            {
                Page page = pageList[i];
                var branches = branchIds[i];

                story.AddPage(page);

                foreach (KeyValuePair<string, int> branch in branches)
                {
                    if (branch.Value < 0 || branch.Value >= pageList.Count)
                    {
                        throw new FormatException("Invalid branch link id");
                    }
                    page.AddBranch(branch.Key, pageList[branch.Value]);
                }
            }

            story.StartPage = pageList[startPageId];
        }

    }
}
