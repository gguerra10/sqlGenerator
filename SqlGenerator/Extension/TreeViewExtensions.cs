using System.Windows.Forms;

namespace SqlGenerator.Extension
{
    public static class TreeViewExtensions
    {
        public static TreeNode SearchRecursive(this TreeView treeView, string nodeName)
        {
            TreeNode result = null;
            foreach (TreeNode node in treeView.Nodes)
            {
                if (node.Text.Equals(nodeName))
                {
                    result = node;
                    break;
                }
                result = node.SearchRecursive(nodeName);
                if (result != null) break;
            }
            return result;
        }

        private static TreeNode SearchRecursive(this TreeNode treeNode, string nodeName)
        {
            TreeNode result = null;
            foreach (TreeNode node in treeNode.Nodes)
            {
                if(node.Text.Equals(nodeName))
                {
                    result = node;
                    break;
                }
                node.SearchRecursive(nodeName);
            }
            return result;
        }
    }
}
