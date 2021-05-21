using System.Windows.Forms;

namespace SqlGenerator.Extensions
{
    public static class TreeViewExtensions
    {
        /// <summary>
        /// Node recursive search based on name
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
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
