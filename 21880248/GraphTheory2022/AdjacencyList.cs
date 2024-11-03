using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Data;

namespace GraphTheory2022
{
    internal class AdjacencyList
    {
        public int n;
        public List<int>[] data;
        public List<int> path = new List<int>();
        public void readToAdjacencyList(string filename)
        {
            // Kiem tra file co ten la filename that su ton tai hay khong
            if (!File.Exists(filename)){
                Console.WriteLine("This file does not exist.");
                return;
            }
            // Doc toan bo noi dung tap tin
            string[] lines = File.ReadAllLines(filename);
            // Dong dau tien la so dinh
            n = Int32.Parse(lines[0]);
            // Cac dong tiep theo la danh sach ke cua n dinh
            data = new List<int>[n];
            for (int i = 0; i < n; ++i){
                string[] tokens = lines[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                data[i] = new List<int>();
                for (int j = 0; j < tokens.Length; ++j)
                    data[i].Add(Int32.Parse(tokens[j]));
            }                
        }
        public void printToAdjacencyList()
        {
            Console.WriteLine(n);
            for (int i = 0; i < n; ++i){
                Console.Write(data[i][0] + " - ");
                for (int j = 1; j <= data[i][0]; ++j)
                    Console.Write(data[i][j] + " ");
                Console.WriteLine();
            }
        }
        //Refer from below post
        /*
         * Fleury’s Àlgorithm for printing Eulerian Path or Circuit:
            https://www.geeksforgeeks.org/fleurys-algorithm-for-printing-eulerian-path/
         */
        // add edge u-v
        private void addEdge(int u, int v)
        {
            data[u].Add(v);
            data[v].Add(u);
        }

        // This function removes edge u-v from graph.
        private void removeEdge(int u, int v)
        {
            data[u].Remove(v);
            data[v].Remove(u);
        }              

        // Print Euler tour starting from vertex u
        public void printEulerUtil(int u)
        {
            // Recur for all the vertices
            // adjacent to this vertex
            for (int i = 1; i < data[u].Count; i++)
            {
                int v = data[u][i];

                // If edge u-v is a valid next edge
                if (isValidNextEdge(u, v))
                {
                    Console.Write(u+" ");

                    // This edge is used so remove it now
                    removeEdge(u, v);
                    printEulerUtil(v);
                }
            }
        }

        // The function to check if edge u-v can be
        // considered as next edge in Euler Tout
        private bool isValidNextEdge(int u, int v)
        {
            // The edge u-v is valid in one of the
            // following two cases:

            // 1) If v is the only adjacent vertex of u 
            // ie size of adjacent vertex list is 1
            if (data[u].Count == 2)
            {
                return true;
            }

            // 2) If there are multiple adjacents, then
            // u-v is not a bridge Do following steps 
            // to check if u-v is a bridge
            // 2.a) count of vertices reachable from u
            bool[] isVisited = new bool[this.n];
            int count1 = dfsCount(u, isVisited);

            // 2.b) Remove edge (u, v) and after removing
            // the edge, count vertices reachable from u
            removeEdge(u, v);
            isVisited = new bool[this.n];
            int count2 = dfsCount(u, isVisited);

            // 2.c) Add the edge back to the graph
            addEdge(u, v);
            return (count1 > count2) ? false : true;
        }

        // A DFS based function to count reachable
        // vertices from v
        private int dfsCount(int v, bool[] isVisited)
        {
            // Mark the current node as visited
            isVisited[v] = true;
            int count = 1;

            // Recur for all vertices adjacent
            // to this vertex
            foreach (int i in data[v])
            {
                if (!isVisited[i])
                {
                    count = count + dfsCount(i, isVisited);
                }
            }
            return count;
        }

        /*Định lý 1: Một đa đồ thị liên thông có chu trình Euler khi và
        chỉ khi mỗi đỉnh của nó đều có bậc chẵn.*/
        public bool isEulerianCycle()
        {
            bool isEulerCycle = true; 
            for (int i=0; i<n; i++)
            {
                if (data[i][0] % 2 == 1)
                {
                    isEulerCycle = false;
                    break;
                }
            }
            return isEulerCycle;
        }

        /*Định lý 2: Đa đồ thị liên thông có đường đi Euler nhưng
        không có chu trình Euler nếu và chỉ nếu nó có đúng hai đỉnh
        bậc lẻ.*/
        public bool isEulerianTrail ()
        {
            bool isEulerianTrail = false;
            int odd_deg_counter = 0;
            for (int i = 0; i < n; i++)
            {
                if (data[i][0] % 2 == 1)
                {
                    odd_deg_counter++;
                }
            }
            if (odd_deg_counter == 2) isEulerianTrail = true;

            return isEulerianTrail;
        }

        /*Tính chất 1: Đồ thị có đỉnh treo không thể có chu trình Hamilton*/
        public bool pendant_vertex()
        {
            bool pendantVertex = false;
            for (int i=0; i<n;i++)
            {
                if (data[i][0] == 1)
                    pendantVertex = true;
            }
            return pendantVertex;
        }
        /*Định lý Dirac*/
        public bool Dirac_theorem()
        {
            bool Hamilton_cycle_exist = false;
            if (n>=3)
            {
                for (int i = 0; i < n;i++)
                {
                    if ((double)data[i][0]>= ((double)(n/2)) )
                    {
                        Hamilton_cycle_exist = true;
                    }
                }
            }
            return Hamilton_cycle_exist;
        }
        /*Định lý Ore*/
        public bool Ore_theorem()
        {
            bool Hamilton_cycle_exist = true;
            if (n >= 3)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = i+1; j < n; j++)
                    {
                        if (data[i][0] + data[j][0] < n)
                        {
                            Hamilton_cycle_exist = false;                            
                        }
                    }
                }
            }
            return Hamilton_cycle_exist;
        }

        //Kiem tra 2 dinh co canh noi hay khong
        private bool isAdjacency(int v1, int v2)
        {
            List<int> listV1 = data[v1];

            for (int i = 1; i <= listV1[0]; i++)
            {
                if (listV1[i] == v2) 
                    return true;
            }
            return false;
        }

        //Kiem tra dinh duoc duyet co nen bo vao duong di khong
        bool isSafe(int v)
        {
            int previousVertex = path[path.Count - 1];

            if (!isAdjacency(previousVertex,v)) 
            {
                return false;
            }

            //Neu dinh da ton tai trong duong di
            if(path.Contains(v))
            {
                return false;
            }
            return true;
        }

        public bool isHamilton_cycle()
        {          
            bool isHamilton_cycle =false;
            if (pendant_vertex())
            {
                return false;
            }

            if (Dirac_theorem() && Ore_theorem())
            {
                isHamilton_cycle = true;
            }
            return isHamilton_cycle;
        }       

        public int find_Hamilton_path()
        {
            if (path.Count == n) //Neu so dinh trong duong di bang voi so dinh
            {
                //Kiem tra chu trinh Hamilton
                if (isAdjacency(path[path.Count - 1],path[0]))
                {                  
                    return 2;        //Ton tai chu trinh Hamilton            
                }
                else
                {                 
                    return 1;        //Ton tai duong di Hamilton            
                }                                            
            }

            for (int v = 0; v < n; v++)
            {
                if (isSafe(v))
                {
                    path.Add(v);

                    int status = find_Hamilton_path();
                    if (status > 0)
                    {
                        return status;
                    }
                    else
                    {
                        path.RemoveAt(path.Count - 1);
                    }                    
                }
            }            
            return 0; //Khong ton tai lo trinh
        }
        public void print_Hamilton()
        {
            for (int i = 0; i < path.Count; i++)
            {
                Console.Write(path[i] + " ");
            }            
        }
    }
    
}
