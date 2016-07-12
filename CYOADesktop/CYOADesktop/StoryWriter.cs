using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace CYOA.Structures
{
    class StoryWriter
    {

        public static void Write(string filename, Story story)
        {
            XmlWriter writer = XmlWriter.Create(filename);

            writer.WriteStartDocument();

            // Write the story title / starting page
            writer.WriteStartElement("Story");
            writer.WriteAttributeString("Title", story.Title);
            writer.WriteAttributeString("NumPages", story.Pages.Count.ToString());
            writer.WriteAttributeString("StartId", story.GetPageId(story.StartPage).ToString());

            // Write all pages in the story
            foreach (Page page in story.Pages)
            {
                WritePage(writer, story, page);
            }

            writer.WriteEndElement();

            writer.WriteEndDocument();
            writer.Close();
        }

        private static void WritePage(XmlWriter writer, Story story, Page page)
        {
            writer.WriteStartElement("Page");

            // Write the page's identifier
            writer.WriteAttributeString("Id", story.GetPageId(page).ToString());

            // Write out the lines on this page
            foreach (string line in page.Lines)
            {
                writer.WriteStartElement("Line");
                writer.WriteString(line);
                writer.WriteEndElement();
            }

            // Write out the branches for this page
            foreach (KeyValuePair<string, Page> entry in page.Branches)
            {
                writer.WriteStartElement("Branch");
                writer.WriteAttributeString("LinkId", story.GetPageId(entry.Value).ToString());
                writer.WriteString(entry.Key);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

    }
}
