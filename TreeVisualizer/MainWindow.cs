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
            if ( CheckValidate())
            {
                _avlTree.Insert(int.Parse(txt.Text));
                drawBox.Print<AVLTree<int>>(_avlTree);
            }
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (CheckValidate())
            {
                _avlTree.Remove(int.Parse(txt.Text));
                drawBox.Print<AVLTree<int>>(_avlTree);
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {

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
