using System.Collections.Generic;

namespace A_star
{
    public abstract class Node
    {    //  this class is the link between A* algorithm and the actual application using it
        internal Node Previous { get; set; }  //  the node you come from if you follow the path from the start to this node
        internal int G { get; set; }  //  the cost from the start to this node
        internal int H { get; set; }   //  an evaluation of the cost from this node to the destination
        internal int F
        {
            get
            {
                return G + H;
            }
        }   //  G + H : a cost evaluation of the path from the start to the end, passing by this node
        public int GetNodeCost()
        {
            return this.G;
        }
        public abstract List<Node> GetNeighbours(Node node); //  return a list of neighbours for this node
        public abstract int Heuristic(Node current, Node n);  //  return an evaluation of the cost from this node to node n
        public abstract int GetId();    //  return an identifier for this node
        public abstract int GetCost(Node current);    //  return the cost to go from this node to node n
        public abstract void ShowInOpen();  //  to show a node that is in the opne list (can be implemented empty)
        public abstract void ShowInClosed();    //  to show a node that is in the closed list (can be implemented empty)
    }
    public class Astar
    {
        private Dictionary<int, Node> closedList;
        private Dictionary<int, Node> openList;

        public List<Node> SearchPath(Node start, Node end)
        {
            List<Node> pathFound = null;

            this.closedList = new Dictionary<int, Node>();
            this.openList = new Dictionary<int, Node>();

            start.G = 0;    //  compute all informations for this node
            start.H = start.Heuristic(start, end);
            start.Previous = null;  //  no previous node
            AddToList(this.openList, start);  //  add it to the open list
            start.ShowInOpen(); //  show starting node

            while (this.openList.Count != 0 && pathFound == null)
            {
                Node bestNode = this.GetBestNodeInOpenList();   //  pick a best node
                Astar.RemoveFromList(this.openList, bestNode);  //  move it from open list to closed list
                Astar.AddToList(this.closedList, bestNode);
                bestNode.ShowInClosed();    //  show the choosen node

                if (bestNode.GetId() == end.GetId())
                {
                    pathFound = CreatePathFromNode(bestNode);   //  destination reached => create path and return it
                }
                else
                {
                    List<Node> neighbours = bestNode.GetNeighbours(bestNode);
                    foreach (Node neighbour in neighbours)
                    {
                        if (!Astar.IsInside(this.closedList, neighbour))
                        {
                            if (!Astar.IsInside(this.openList, neighbour))
                            {    //  is neighbour a new node?
                                neighbour.G = bestNode.G + bestNode.GetCost(neighbour); //  if yes, compute its informations
                                neighbour.H = neighbour.Heuristic(neighbour, end);
                                neighbour.Previous = bestNode;
                                Astar.AddToList(this.openList, neighbour);  //  add it to the open list
                                neighbour.ShowInOpen(); //  show it
                            }
                            else
                            {
                                //  the neighbour is already in the open list (ie we found another path from the start to it)
                                Node nodeInList = Astar.GetNodeInList(this.openList, neighbour);
                                if (nodeInList.G > bestNode.G + bestNode.GetCost(neighbour))
                                {  //  is the new path better than the old one?
                                    nodeInList.G = bestNode.G + bestNode.GetCost(neighbour);    //  if yes, update G and Previous informations
                                    nodeInList.Previous = bestNode;
                                }
                            }
                        }
                    }
                }
            }

            return pathFound;
        }
        static private void AddToList(Dictionary<int, Node> list, Node n)
        {
            list.Add(n.GetId(), n);
        }
        static private void RemoveFromList(Dictionary<int, Node> list, Node n)
        {
            list.Remove(n.GetId());
        }
        static private bool IsInside(Dictionary<int, Node> list, Node n)
        {
            return list.ContainsKey(n.GetId());
        }
        static private Node GetNodeInList(Dictionary<int, Node> list, Node n)
        {
            return list[n.GetId()];
        }
        private Node GetBestNodeInOpenList()
        {
            Node bestNode = null;

            foreach (Node node in this.openList.Values)
            {
                if (bestNode == null || node.F < bestNode.F)
                {
                    bestNode = node;
                }
            }

            return bestNode;
        }
        static private List<Node> CreatePathFromNode(Node node)
        {
            List<Node> path = new List<Node>();

            while (node != null)
            {
                path.Add(node);
                node = node.Previous;
            }

            return path;
        }
        public Node GiveNextNodeOfPath(Node start, Node end)
        { 
            List<Node> path = this.SearchPath(start, end);
            if (path == null) return null;
            if (path.Count == 1) return path[0];
            return path[path.Count - 2];
        }

        public int GetPathCost(List<Node> path)
        {
            if (path == null) return 10000000;
            if (path.Count == 1) return 0;
            return path[path.Count - 2].G;
        }
    }
}