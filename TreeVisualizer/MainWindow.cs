using System;
using System.Linq;
using System.Windows.Forms;

namespace TreeVisualizer
{
    public partial class MainWindow : Form
    {
        private AVLTree _avlTree;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            _avlTree = new AVLTree(new TreeConfiguration(circleDiameter: 45));
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            if (CheckValidate())
            {
                _avlTree.Insert(int.Parse(txt.Text));
                drawBox.Print(_avlTree);
            }
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (CheckValidate())
            {
                _avlTree.Remove(int.Parse(txt.Text));
                drawBox.Print(_avlTree);
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (CheckValidate())
            {
                _avlTree.Search(int.Parse(txt.Text));
            }
        }

        private bool CheckValidate() {
            if (string.IsNullOrEmpty(txt.Text))
            {
                MessageBox.Show("Поле для ввода не может быть пустым", "Ошибка ввода");
                return false;
            }
            if (!int.TryParse(txt.Text, out int value))
            {
                MessageBox.Show("Ожидалось целое число", "Ошибка ввода");
                return false;
            }
            return true;
        }
    }
}
