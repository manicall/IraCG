using System;
using System.Linq;
using System.Windows.Forms;

namespace TreeVisualizer
{
    public partial class MainWindow : Form
    {
        private AVLTree avlTree;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            avlTree = new AVLTree(new TreeConfiguration(circleDiameter: 45));
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            if (CheckValidate())
            {
                // hasValue - не дает добавить элемент, если такой уже есть
                if (avlTree.hasValue(int.Parse(txt.Text)))
                {
                    MessageBox.Show("Элемент уже присутствует в дереве", "Ошибка добавления");
                    return;
                }
                // вставка элемента в дерево
                avlTree.Insert(int.Parse(txt.Text));
                // отрисовка дерева на форме
                drawBox.Print(avlTree);
            }
            // установка фокуса на текстовое поле
            txt.Focus();
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (CheckValidate())
            {
                if (!avlTree.hasValue(int.Parse(txt.Text)))
                {
                    MessageBox.Show("Элемент отсутсвует в дереве", "Ошибка удаления");
                    return;
                }
                avlTree.Remove(int.Parse(txt.Text));
                drawBox.Print(avlTree);
            }
            txt.Focus();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (CheckValidate())
            {
                avlTree.Search(int.Parse(txt.Text));
                drawBox.Print(avlTree);
            }
            txt.Focus();
        }

        // проверка на ввод числа в текстовое поле
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
