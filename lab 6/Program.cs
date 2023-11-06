using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;

class Program {

    static string pathin = @"C:\YandexDisk\Хлам\лабы\ЭГА\lab 6\in.txt";
    static string pathout = @"C:\YandexDisk\Хлам\лабы\ЭГА\lab 6\out.txt";
    const int cityN = 5;
    static double[,] map = new double[cityN, cityN];
    static List<int> visited = new List<int>();
    static List<int> path = new List<int>();
    static double[] fin = new double[cityN * cityN];
    static void Main() {
        filein();
        //fout(fin);
        //Console.WriteLine();


        Random random = new Random();
        int n = random.Next(cityN);
        visited.Add(n);
        path.Add(n);

        for (int i = 0; i < cityN; i++) {
            path = min(path, map);
        }

        File.WriteAllText(pathout, "");
        fout(path);


    }

    static List<int> min(List<int> l, int[,] m) {
        int min = 9999;
        int index = cityN + 1;
        for (int i = 0; i < cityN; i++) {
            if (m[i, l.Last()] < min - 1 && !l.Contains(i)) { min = m[i, l.Last()]; index = i; }
        }
        if (index != cityN + 1) l.Add(index);
        return (l);
    }

    static List<int> min(List<int> l, double[,] m) {
        double min = 9999;
        int index = cityN + 1;
        int neighbour = cityN + 1;
        foreach (int i in path) {
            for(int j = 0; j < cityN; j++) {
                if (m[i, j] < min && !l.Contains(j)) { min = m[i, j]; index = j; neighbour = l.IndexOf(i); }
            }
        }
        if (index != cityN + 1) {
            l.Add(index);
            int tmp = l[l.Count() - 1];
            for (int i = l.Count() - 1; i > neighbour + 1; i--) l[i] = l[i - 1];
            l[neighbour + 1] = tmp;
        }
        return (l);
    }

    static void fout(List<int> a) {
        for (int i = 0; i < a.Count(); i++) { File.AppendAllText(pathout, Convert.ToString(a[i]) + " "); }
        //Console.WriteLine();
    }

    static void fout(int[] a) {
        for (int i = 0; i < a.Length; i++) { File.AppendAllText(pathout, Convert.ToString(a[i]) + " "); }
        //Console.WriteLine();
    }

    static void filein() {
        string a = File.ReadAllText(pathin);
        string[] b = a.Split(' ', '\n');
        for (int j = 0; j < cityN; j++) {
            for (int i = 0; i < cityN; i++) map[i, j] = Double.Parse(b[i + j * cityN], CultureInfo.InvariantCulture.NumberFormat);
        }
    }
}