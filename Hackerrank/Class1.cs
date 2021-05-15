using System.Collections.Generic;
using System.Linq;

namespace AhoCorasick
{
    public class AhoCorasickTree
    {
        internal AhoCorasickTreeNode Root { get; set; }

        public AhoCorasickTree(List<string> keywords)
        {
            Root = new AhoCorasickTreeNode();

            if (keywords != null)
            {
                foreach (var p in keywords)
                    AddPatternToTree(p);
                SetFailureNodes();
            }
        }

        public bool Contains(string text)
        {
            return Contains(text, false);
        }

        public bool ContainsThatStart(string text)
        {
            return Contains(text, true);
        }

        private bool Contains(string text, bool onlyStarts)
        {
            var pointer = Root;

            foreach (var c in text)
            {
                var transition = GetTransition(c, ref pointer);

                if (transition != null)
                    pointer = transition;
                else if (onlyStarts)
                    return false;

                if (pointer.Results.Any())
                    return true;
            }
            return false;
        }

        public IEnumerable<string> FindAll(string text)
        {
            var pointer = Root;

            foreach (var c in text)
            {
                var transition = GetTransition(c, ref pointer);

                if (transition != null)
                    pointer = transition;

                foreach (var result in pointer.Results)
                    yield return result;
            }
        }

        public Dictionary<string, List<int>> FindAllWithIndex(string text)
        {
            Dictionary<string, List<int>> stringWithIndex = new Dictionary<string, List<int>>();

            var pointer = Root;

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                var transition = GetTransition(c, ref pointer);

                if (transition != null)
                    pointer = transition;

                foreach (var result in pointer.Results)
                {
                    if (!stringWithIndex.ContainsKey(result))
                        stringWithIndex[result] = new List<int>();
                    stringWithIndex[result].Add(i - result.Length + 1);
                }
            }

            return stringWithIndex;
        }

        private AhoCorasickTreeNode GetTransition(char c, ref AhoCorasickTreeNode pointer)
        {
            AhoCorasickTreeNode transition = null;
            while (transition == null)
            {
                transition = pointer.GetTransition(c);

                if (pointer == Root)
                    break;

                if (transition == null)
                    pointer = pointer.Failure;
            }
            return transition;
        }

        private void SetFailureNodes()
        {
            var nodes = FailToRootNode();
            FailUsingBFS(nodes);
            Root.Failure = Root;
        }

        private void AddPatternToTree(string pattern)
        {
            var node = Root;
            foreach (var c in pattern)
            {
                node = node.GetTransition(c)
                       ?? node.AddTransition(c);
            }
            node.AddResult(pattern);
        }

        private List<AhoCorasickTreeNode> FailToRootNode()
        {
            var nodes = new List<AhoCorasickTreeNode>();
            foreach (var node in Root.Transitions)
            {
                node.Failure = Root;
                nodes.AddRange(node.Transitions);
            }
            return nodes;
        }

        private void FailUsingBFS(List<AhoCorasickTreeNode> nodes)
        {
            while (nodes.Count != 0)
            {
                var newNodes = new List<AhoCorasickTreeNode>();
                foreach (var node in nodes)
                {
                    var failure = node.ParentFailure;
                    var value = node.Value;

                    while (failure != null && !failure.ContainsTransition(value))
                    {
                        failure = failure.Failure;
                    }

                    if (failure == null)
                    {
                        node.Failure = Root;
                    }
                    else
                    {
                        node.Failure = failure.GetTransition(value);
                        node.AddResults(node.Failure.Results);
                    }

                    newNodes.AddRange(node.Transitions);
                }
                nodes = newNodes;
            }
        }
    }

    internal class AhoCorasickTreeNode
    {
        public char Value { get; private set; }
        public AhoCorasickTreeNode Failure { get; set; }

        private readonly List<string> _results;
        private readonly Dictionary<char, AhoCorasickTreeNode> _transitionsDictionary;
        private readonly AhoCorasickTreeNode _parent;

        public List<string> Results { get { return _results; } }
        public AhoCorasickTreeNode ParentFailure { get { return _parent == null ? null : _parent.Failure; } }
        public List<AhoCorasickTreeNode> Transitions { get { return _transitionsDictionary.Values.ToList(); } }

        public AhoCorasickTreeNode() : this(null, ' ')
        {
        }

        private AhoCorasickTreeNode(AhoCorasickTreeNode parent, char value)
        {
            Value = value;
            _parent = parent;
            _results = new List<string>();
            _transitionsDictionary = new Dictionary<char, AhoCorasickTreeNode>();
        }

        public void AddResult(string result)
        {
            if (!_results.Contains(result))
            {
                _results.Add(result);
            }
        }

        public void AddResults(IEnumerable<string> results)
        {
            foreach (var result in results)
            {
                AddResult(result);
            }
        }

        public AhoCorasickTreeNode AddTransition(char c)
        {
            var node = new AhoCorasickTreeNode(this, c);
            _transitionsDictionary.Add(node.Value, node);
            return node;
        }

        public AhoCorasickTreeNode GetTransition(char c)
        {
            return _transitionsDictionary.ContainsKey(c)
                       ? _transitionsDictionary[c]
                       : null;
        }

        public bool ContainsTransition(char c)
        {
            return _transitionsDictionary.ContainsKey(c);
        }
    }
}
