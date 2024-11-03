using System;
using System.IO;

namespace GraphTheory2022
{
    internal class Program
    {
        static void Main(string[] args)
        {   
            //khai bao danh sach ke
            string graph_A = "..\\..\\..\\Graph_A.txt";
            string graph_B = "..\\..\\..\\Graph_B.txt";
            string graph_C = "..\\..\\..\\Graph_C.txt";
            string graph_D = "..\\..\\..\\Graph_D.txt";
            string graph_path = String.Empty;
            //
            int i = 0;
            while (i != 5)
            {
                Console.WriteLine("\nXin chon danh sach ke bieu dien do thi theo so 1,2,3,4 theo danh sach sau\n" +
                "1. Do thi A \n" +
                "2. Do thi B \n" +
                "3. Do thi C \n" +
                "4. Do thi D \n" +
                "5. THOAT");
                i = int.Parse(Console.ReadLine());
                switch (i)
                {
                    case 1:
                        graph_path = graph_A;
                        break;
                    case 2:
                        graph_path = graph_B;
                        break;
                    case 3:
                        graph_path = graph_C;
                        break;
                    case 4:
                        graph_path = graph_D;
                        break;
                    case 5:
                        return;
                        break;
                    default:
                        Console.WriteLine("Do thi khong ban chon khong hop le");
                        break;
                }

                AdjacencyList graph = new AdjacencyList();
                graph.readToAdjacencyList(graph_path);

                Console.WriteLine("Chon phuong an tham quan nha \n" +
                    "1. Phuong an A \n" +
                    "2. Phuong an B");
                int plan = int.Parse(Console.ReadLine());
                if (plan == 1)
                {
                    phuonganA(graph);
                }
                else if (plan == 2)
                {
                    phuonganB(graph);
                }
                else
                {
                    Console.WriteLine("Phuong an lua chon khong hop le");
                }
            }
        }

        public static int DinhSanVuon()
        {
            Console.WriteLine("Xin moi nhap chi muc dinh san vuon: ");
            int n = int.Parse(Console.ReadLine());
            return n;
        }

        public static void phuonganA (AdjacencyList graph)
        {
            if (graph.isEulerianCycle())
            {
                Console.WriteLine("2 - Lo Trinh co the quay lai san vuon");
                graph.printEulerUtil(DinhSanVuon());
            }
            else if (graph.isEulerianTrail())
            {
                Console.WriteLine("1 - Lo trinh ket thuc tai mot phong nao do trong nha");
                graph.printEulerUtil(DinhSanVuon());
            }
            else
            {
                Console.WriteLine("0 - Khong ton tai lo trinh");
            }
        }

        public static void phuonganB (AdjacencyList graph)
        {
            graph.path.Add(DinhSanVuon());
            int status = graph.find_Hamilton_path();

            if (status == 0)
            {
                Console.WriteLine("0 - Khong ton tai lo trinh");
            }
            else if (status == 1)
            {
                Console.WriteLine("1 - Lo trinh ket thuc tai mot phong nao do trong nha");
                graph.print_Hamilton();
            }
            else if (status == 2)
            {
                Console.WriteLine("2 - Lo Trinh co the quay lai san vuon");
                graph.path.Add(graph.path[0]);
                graph.print_Hamilton();
            }
        }
    }
}
