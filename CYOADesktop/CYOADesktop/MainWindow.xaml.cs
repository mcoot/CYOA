using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CYOADesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CYOAStructures.Story testStory;

        public MainWindow()
        {
            InitializeComponent();
        }

        private CYOAStructures.Story genTestStory()
        {
            CYOAStructures.Story story = new CYOAStructures.Story("Bob Goes To The Fish Market");

            CYOAStructures.Page p1 = new CYOAStructures.Page();
            CYOAStructures.Page p2 = new CYOAStructures.Page();
            CYOAStructures.Page p3 = new CYOAStructures.Page();
            CYOAStructures.Page p4 = new CYOAStructures.Page();

            p1.AddLine("Bob went to the fish market.");
            p1.AddLine("There, Bob saw several fish for purchase.");
            p1.AddBranch("Purchase a fish", p2);
            p1.AddBranch("Go home", p3);

            p2.AddLine("Bob bought a fish.");
            p2.AddLine("Having completed his task, Bob decided to go home and sleep.");
            p2.AddBranch("Go home", p3);
            p2.AddBranch("Jump in a lake instead", p4);

            p3.AddLine("Bob went home.");
            p3.AddLine("Bob tucked himself into bed and went to sleep.");
            p3.AddLine("The End");

            p4.AddLine("Rather than going home, Bob jumped in a lake.");
            p4.AddLine("Bob couldn't swim, and died.");
            p4.AddLine("The End");

            story.AddPage(p1);
            story.AddPage(p2);
            story.AddPage(p3);
            story.AddPage(p4);

            story.StartPage = p1;

            return story;
        }

        private void btnTestWrite_Click(object sender, RoutedEventArgs e)
        {
            CYOAStructures.StoryWriter.Write("testStory.xml", testStory);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            testStory = genTestStory();
        }
    }
}
