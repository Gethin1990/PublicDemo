using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new TreeNode
            {
                value = 0,
                treeNodes = new TreeNode[] {
                    new TreeNode() { value = 1, treeNodes = new TreeNode[] {
                         new TreeNode() { value = 3,treeNodes = new TreeNode[]{ } },
                         new TreeNode() { value = 4,treeNodes = new TreeNode[]{ } }
                    } },
                    new TreeNode() { value = 2, treeNodes = new TreeNode[] {
                        new TreeNode() { value = 4,treeNodes = new TreeNode[]{ } },
                        new TreeNode() { value = 5,treeNodes = new TreeNode[]{ } }
                    } }
                }
            };
            VerifyTreeNode(tree, tree.treeNodes.Length);

            Console.WriteLine(result);
            Console.ReadLine();
        }
        public static int result = 0;

        public static int VerifyTreeNode(TreeNode treeNode, int sonCount)
        {
            var count = treeNode.treeNodes.Length;
            // if count not match root son count
            if (count == 0)
            {
                return 0;
            }
            if (count != sonCount)
            {
                return -1;
            }

            foreach (TreeNode node in treeNode.treeNodes)
            {
                if (node.Equals(treeNode.treeNodes[0]))
                {
                    result++;
                }
                if (VerifyTreeNode(node, treeNode.treeNodes.Length) == -1)
                {
                    result = -1;
                }
            }
            return result;
        }
    }

    public class TreeNode
    {
        public int value;

        public TreeNode[] treeNodes;
    }

}
