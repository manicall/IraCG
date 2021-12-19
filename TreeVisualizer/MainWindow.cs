using System;
using System.Linq;
using System.Windows.Forms;

namespace TreeVisualizer
{
    public partial class MainWindow : Form
    {
        private ITree<int> _avlTree;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            _avlTree = new AVLTree<int>(new TreeConfiguration(circleDiameter: 45, arrowAnchorSize: 5));
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt.Text))
            {
                MessageBox.Show("You forgot to enter a value ;)", "Reminder");
                return;
            }
            if (!int.TryParse(txt.Text, out int value))
            {
                MessageBox.Show($"Expected value type of {typeof(int)}", "Error");
                return;
            }
            _avlTree.Insert(value);
            drawBox.Print<AVLTree<int>>(_avlTree);
            
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt.Text))
            {
                MessageBox.Show("You forgot to enter a value ;)", "Reminder");
                return;
            }
            if (!int.TryParse(txt.Text, out int value))
            {
                MessageBox.Show($"Expected value type of {typeof(int)}", "Error");
                return;
            }
            _avlTree.Remove(value);
            drawBox.Print<AVLTree<int>>(_avlTree);
            
        }

    }
}
