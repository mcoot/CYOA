using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYOA.Structures
{
    class GameState
    {
        private Story currentStory;
        private Page currentPage;
        private int currentLine;

        public Story CurrentStory
        {
            get
            {
                return currentStory;
            }
        }
        public Page CurrentPage
        {
            get
            {
                return currentPage;
            }
        }

        public int CurrentLine
        {
            get
            {
                return currentLine;
            }
        }

        public GameState(Story story)
        {
            currentStory = story;
            currentPage = story.StartPage;
            currentLine = 0;
        }

        public bool AreLinesLeft()
        {
            return CurrentLine < currentPage.Lines.Count - 1;
        }

        public void NextLine()
        {
            if (AreLinesLeft())
            {
                currentLine++;
            }
        }

        public bool BranchExists(string option)
        {
            return CurrentPage.BranchExists(option);
        }

        public void TakeBranch(string option)
        {
            if (!BranchExists(option))
            {
                throw new ArgumentException("Invalid branch taken");
            }

            currentLine = 0;
            currentPage = CurrentPage.Branches[option];
        }

    }
}
