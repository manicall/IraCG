using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TreeVisualizer
{
    public class AVLTree
    {
        protected TreeConfiguration _configuration;
        protected Node _root;

        public AVLTree(TreeConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Insert(int value)
        {
            _root = Insert(_root, value);
        }

        public void Remove(int value)
        {
            _root = Remove(_root, value);
        }

        public void Search(int key)
        {
            if (Search(_root, key) != null)
            {
                MessageBox.Show("Узел со значением " + key + " присутствует в дереве", "Успех");
            }
            else
            {
                MessageBox.Show("Узел со значением " + key + " отсутствует в дереве", "Неуспех");
            }
        }

        private Node Insert(Node root, int value)
        {
            if (root == null)
                root = new Node(value);

            else if (root.Value.CompareTo(value) > 0)
            {
                root.Left = Insert(root.Left, value);
                root = BalanceTree(root);
            }
            else
            {
                root.Right = Insert(root.Right, value);
                root = BalanceTree(root);
            }
            return root;
        }

        private Node Remove(Node root, int value)
        {
            if (root == null)
                return root;

            if (root.Value.CompareTo(value) > 0)
            {
                root.Left = Remove(root.Left, value);
                if (GetBalanceFactor(root) == -2)
                {
                    if (GetBalanceFactor(root.Right) <= 0)
                        root = RotateRR(root);
                    else
                        root = RotateRL(root);
                }
            }
            else if (root.Value.CompareTo(value) < 0)
            {
                root.Right = Remove(root.Right, value);
                if (GetBalanceFactor(root) == -2)
                {
                    if (GetBalanceFactor(root.Left) <= 0)
                        root = RotateLL(root);
                    else
                        root = RotateLR(root);
                }
            }

            else
            {
                Node parent;
                if (root.Right != null)
                {
                    parent = root.Right;
                    while (parent.Left != null)
                    {
                        parent = parent.Left;
                    }
                    root.Value = parent.Value;
                    root.Right = Remove(root.Right, parent.Value);
                    if (GetBalanceFactor(root) == 2)
                    {
                        if (GetBalanceFactor(root.Left) >= 0)
                            root = RotateLL(root);
                        else
                            root = RotateLR(root);
                    }
                }
                else
                    return root.Left;
            }
            return root;
        }

        private Node Search(Node root, int value)
        {
            if (root == null)
                return null;

            if (value < root.Value)
            {
                if (value == root.Value)
                {
                    return root;
                }
                else
                    return Search(root.Left, value);
            }
            else
            {
                if (value == root.Value)
                {
                    return root;
                }
                else
                    return Search(root.Right, value);
            }
        }


        public IEnumerable<NodeInfo> GetAllNodes()
        {
            var nodeCollection = new List<Node>();

            GetAllNodes(_root, nodeCollection);

            var nodeInfos = nodeCollection.ToDictionary(
                x => x,
                y => new NodeInfo
                {
                    IsBstNode = false,
                    IsAvlNode = true,
                    Value = y.Value.ToString(),
                    IsLeaf = y.Left == null && y.Right == null
                }
            );

            CalculateNodePositions(_root, nodeInfos, offset: 0, depth: 0);
            AggregateChildNotePositions(_root, null, nodeInfos);

            foreach (var node in nodeCollection)
            {
                nodeInfos[node].Height = GetHeight(node);
            }
            return nodeInfos.Values;
        }

        private Node BalanceTree(Node root)
        {
            int balanceFactor = GetBalanceFactor(root);
            if (balanceFactor > 1)
            {
                if (GetBalanceFactor(root.Left) > 0)
                    root = RotateLL(root);
                else
                    root = RotateLR(root);
            }
            else if (balanceFactor < -1)
            {
                if (GetBalanceFactor(root.Right) > 0)
                    root = RotateRL(root);
                else
                    root = RotateRR(root);
            }
            return root;
        }

        private int GetBalanceFactor(Node root)
        {
            int left = GetHeight(root.Left);
            int right = GetHeight(root.Right);
            int factor = left - right;
            return factor;
        }

        private int GetHeight(Node root)
        {
            int height = 0;
            if (root != null)
            {
                int left = GetHeight(root.Left);
                int right = GetHeight(root.Right);
                int max = GetMax(left, right);
                height = max + 1;
            }
            return height;
        }

        private int GetMax(int left, int right)
        {
            return left > right ? left : right;
        }

        private Node RotateRR(Node parent)
        {
            Node pivot = parent.Right;
            parent.Right = pivot.Left;
            pivot.Left = parent;
            return pivot;
        }

        private Node RotateLL(Node parent)
        {
            Node pivot = parent.Left;
            parent.Left = pivot.Right;
            pivot.Right = parent;
            return pivot;
        }

        private Node RotateLR(Node parent)
        {
            Node pivot = parent.Left;
            parent.Left = RotateRR(pivot);
            return RotateLL(parent);
        }

        private Node RotateRL(Node parent)
        {
            Node pivot = parent.Right;
            parent.Right = RotateLL(pivot);
            return RotateRR(parent);
        }



        protected int CalculateNodePositions(Node root, IDictionary<Node, NodeInfo> nodeInfos, int offset, int depth)
        {
            if (root == null)
            {
                return 0;
            }

            int circleDiameterOffset = _configuration.CircleDiameter - (int)(_configuration.CircleDiameter / Math.PI);

            int left = CalculateNodePositions(root.Left, nodeInfos, offset, depth + 1);
            int right = CalculateNodePositions(root.Right, nodeInfos, offset + left + circleDiameterOffset, depth + 1);

            nodeInfos[root].Position =
                new Position
                {
                    Y = depth * _configuration.CircleDiameter,
                    X = left + offset
                };
            return left + right + circleDiameterOffset;
        }

        protected void AggregateChildNotePositions(Node root, Node parent, IDictionary<Node, NodeInfo> nodeInfos)
        {
            if (root == null)
            {
                return;
            }

            AggregateChildNotePositions(root.Left, root, nodeInfos);
            AggregateChildNotePositions(root.Right, root, nodeInfos);

            if (parent != null)
            {
                nodeInfos[root].IsRightChild = parent.Right == root;
                nodeInfos[root].IsLeftChild = parent.Left == root;
            }

            if (root.Left != null)
                nodeInfos[root].LeftChildPosition =
                    new Position
                    {
                        X = nodeInfos[root.Left].Position.X,
                        Y = nodeInfos[root.Left].Position.Y
                    };
            if (root.Right != null)
                nodeInfos[root].RightChildPosition =
                    new Position
                    {
                        X = nodeInfos[root.Right].Position.X,
                        Y = nodeInfos[root.Right].Position.Y
                    };
        }

        protected void GetAllNodes(Node root, ICollection<Node> collection)
        {
            if (root == null)
            {
                return;
            }
            collection.Add(root);
            GetAllNodes(root.Left, collection);
            GetAllNodes(root.Right, collection);
        }

        protected Node GetMinValueNode(Node root)
        {
            Node currentNode = root;
            while (currentNode != null && currentNode.Left != null)
            {
                currentNode = currentNode.Left;
            }
            return currentNode;
        }

        protected Node GetMaxValueNode(Node root)
        {
            Node currentNode = root;
            while (currentNode != null && currentNode.Right != null)
            {
                currentNode = currentNode.Right;
            }
            return currentNode;
        }

        public TreeConfiguration GetConfiguration()
        {
            return _configuration;
        }
    }
}
