using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;

class GraphColoring
{
    static Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>()
    {
                {
    "Adana", new List<string> { "Adıyaman", "Mersin", "Hatay", "Osmaniye", "Kahramanmaraş" } },
        { "Adıyaman", new List<string> { "Gaziantep", "Şanlıurfa", "Diyarbakır", "Malatya", "Kahramanmaraş", "Adana" } },
    {"Afyonkarahisar", new List<string> { "Uşak", "Kütahya", "Eskişehir", "Konya", "Denizli", "Burdur", "Isparta" }},
    {"Ağrı", new List<string> { "Erzurum", "Muş", "Iğdır", "Van", "Kars" }},
    {"Aksaray", new List<string> { "Nevşehir", "Niğde", "Konya" }},
    {"Amasya", new List<string> { "Tokat", "Samsun", "Ordu", "Çorum", "Yozgat" }},
    {"Ankara", new List<string> { "Kırıkkale", "Aksaray", "Eskişehir", "Çankırı", "Kırşehir", "Bolu" }},
    {"Antalya", new List<string> { "Burdur", "Isparta", "Konya", "Mersin" }},
    {"Ardahan", new List<string> { "Kars", "Artvin" }},
    {"Artvin", new List<string> { "Rize", "Erzurum", "Ardahan" }},
    {"Aydın", new List<string> { "İzmir", "Muğla", "Denizli" }},
    {"Balıkesir", new List<string> { "Çanakkale", "Bursa", "Manisa", "Kütahya" }},
    {"Bartın", new List<string> { "Karabük", "Zonguldak", "Kastamonu" }},
    {"Batman", new List<string> { "Siirt", "Mardin", "Şırnak", "Diyarbakır" }},
    {"Bayburt", new List<string> { "Trabzon", "Gümüşhane", "Erzincan" }},
    {"Bilecik", new List<string> { "Sakarya", "Bursa", "Kütahya", "Eskişehir" }},
    {"Bingöl", new List<string> { "Muş", "Erzincan", "Tunceli", "Diyarbakır" }},
    {"Bitlis", new List<string> { "Van", "Muş", "Siirt" }},
    {"Bolu", new List<string> { "Ankara", "Zonguldak", "Karabük", "Düzce" }},
    {"Burdur", new List<string> { "Antalya", "Isparta", "Afyonkarahisar", "Denizli" }},
    {"Bursa", new List<string> { "Balıkesir", "Yalova", "Bilecik", "Kütahya" }},
    {"Çanakkale", new List<string> { "Balıkesir", "Tekirdağ" }},
    {"Çankırı", new List<string> { "Ankara", "Karabük", "Kastamonu", "Çorum" }},
    {"Çorum", new List<string> { "Amasya", "Tokat", "Yozgat", "Kırıkkale", "Çankırı", "Sinop" }},
    {"Denizli", new List<string> { "Muğla", "Burdur", "Afyonkarahisar", "Manisa", "Aydın" }},
    {"Diyarbakır", new List<string> { "Batman", "Mardin", "Şırnak", "Siirt", "Şanlıurfa", "Adıyaman", "Bingöl" }},
    {"Düzce", new List<string> { "Bolu", "Sakarya", "Zonguldak", "Bolu" }},
    {"Edirne", new List<string> { "Kırklareli", "Tekirdağ" }},
    {"Elazığ", new List<string> { "Tunceli", "Bingöl", "Malatya", "Diyarbakır", "Muş" }},
    {"Erzincan", new List<string> { "Bayburt", "Gümüşhane", "Erzurum", "Tunceli", "Bingöl", "Malatya" }},
    {"Erzurum", new List<string> { "Ağrı", "Bayburt", "Gümüşhane", "Artvin", "Rize", "Trabzon", "Bingöl", "Muş" }},
    {"Eskişehir", new List<string> { "Afyonkarahisar", "Kütahya", "Bilecik", "Ankara" }},
    {"Gaziantep", new List<string> {  "Şanlıurfa", "Adıyaman" }},
    {"Giresun", new List<string> { "Ordu", "Trabzon" }},
    {"Gümüşhane", new List<string> { "Trabzon", "Bayburt", "Erzincan", "Erzurum" }},
    {"Hakkari", new List<string> { "Van", "Şırnak" }},
    {"Hatay", new List<string> { "Osmaniye", "Adana" }},
    {"Iğdır", new List<string> { "Kars", "Ağrı" }},
    {"Isparta", new List<string> { "Antalya", "Burdur", "Afyonkarahisar", "Konya" }},
    {"İstanbul", new List<string> { "Kocaeli", "Tekirdağ" }},
    {"İzmir", new List<string> { "Manisa", "Aydın" }},
    {"Kahramanmaraş", new List<string> { "Osmaniye", "Adıyaman", "Gaziantep" }},
    {"Karabük", new List<string> { "Bartın", "Zonguldak", "Çankırı", "Bolu" }},
    {"Karaman", new List<string> { "Mersin", "Konya", "Antalya" }},
    {"Kars", new List<string> { "Ağrı", "Ardahan", "Iğdır", "Erzurum" }},
    {"Kastamonu", new List<string> { "Sinop", "Çorum", "Çankırı", "Karabük", "Bartın" }},
    {"Kayseri", new List<string> { "Sivas", "Yozgat", "Nevşehir", "Niğde", "Adana" }},
    {"Kırıkkale", new List<string> { "Çankırı", "Çorum", "Yozgat", "Ankara" }},
    {"Kırklareli", new List<string> { "Tekirdağ", "Edirne" }},
    {"Kırşehir", new List<string> { "Nevşehir", "Yozgat", "Aksaray", "Ankara" }},
    {"Kocaeli", new List<string> { "Sakarya", "İstanbul" }},
    {"Konya", new List<string> { "Karaman", "Antalya", "Isparta", "Afyonkarahisar", "Niğde", "Aksaray" }},
    {"Kütahya", new List<string> { "Eskişehir", "Afyonkarahisar", "Uşak", "Manisa", "Bursa", "Balıkesir" }},
    {"Malatya", new List<string> { "Elazığ", "Tunceli", "Erzincan", "Kahramanmaraş", "Adıyaman" }},
    {"Manisa", new List<string> { "İzmir", "Denizli", "Uşak", "Kütahya", "Balıkesir" }},
    {"Mardin", new List<string> { "Şırnak", "Batman", "Diyarbakır" }},
    {"Mersin", new List<string> { "Adana", "Karaman", "Kahramanmaraş", "Osmaniye" }},
    {"Muğla", new List<string> { "Aydın", "Denizli" }},
    {"Muş", new List<string> { "Bingöl", "Erzurum", "Bitlis", "Batman", "Diyarbakır" }},
    {"Nevşehir", new List<string> { "Aksaray", "Niğde", "Kırşehir", "Kayseri" }},
    {"Niğde", new List<string> { "Konya", "Aksaray", "Nevşehir", "Kayseri" }},
    {"Ordu", new List<string> { "Samsun", "Amasya", "Tokat", "Sivas", "Giresun" }},
    {"Osmaniye", new List<string> { "Hatay", "Adana", "Kahramanmaraş" }},
    {"Rize", new List<string> { "Artvin", "Trabzon", "Erzurum" }},
    {"Sakarya", new List<string> { "Düzce", "Bilecik", "Bursa", "Kocaeli" }},
    {"Samsun", new List<string> { "Ordu", "Amasya", "Tokat", "Çorum", "Sinop" }},
    {"Siirt", new List<string> { "Şırnak", "Batman", "Diyarbakır", "Bitlis" }},
    {"Sinop", new List<string> { "Samsun", "Çorum", "Kastamonu" }},
    {"Sivas", new List<string> { "Giresun", "Ordu", "Tokat", "Kayseri", "Yozgat", "Malatya" }},
    {"Şanlıurfa", new List<string> { "Mardin", "Diyarbakır", "Adıyaman", "Gaziantep" }},
    {"Şırnak", new List<string> { "Hakkari", "Siirt", "Mardin", "Batman" }},
    {"Tekirdağ", new List<string> { "Edirne", "İstanbul", "Kırklareli", "Çanakkale" }},
    {"Tokat", new List<string> { "Amasya", "Samsun", "Ordu", "Sivas", "Yozgat", "Giresun" }},
    {"Trabzon", new List<string> { "Rize", "Bayburt", "Gümüşhane", "Giresun" }},
    {"Tunceli", new List<string> { "Erzincan", "Elazığ", "Bingöl", "Malatya" }},
    {"Uşak", new List<string> { "Afyonkarahisar", "Manisa", "Kütahya" }},
    {"Van", new List<string> { "Hakkari", "Bitlis", "Siirt", "Ağrı" }},
    {"Yalova", new List<string> { "Bursa" }},
    {"Yozgat", new List<string> { "Sivas", "Çorum", "Tokat", "Amasya", "Kırıkkale", "Kırşehir" }},
    {"Zonguldak", new List<string> { "Bartın", "Karabük", "Düzce", "Bolu" }
}
    };

    static Dictionary<string, int> renkler = new Dictionary<string, int>();

    static void ColorVertices(object vertex)
    {
        string v = (string)vertex;
        List<string> komşular = graph[v];
        HashSet<int> komşurenkler = new HashSet<int>();

        // Komşu düğümlerin renklerini kontrol et
        foreach (var komşu in komşular)
        {
            if (renkler.ContainsKey(komşu))
            {
                komşurenkler.Add(renkler[komşu]);
            }
        }

        // Minimum renk kullanarak düğümü boyama
        for (int renk = 0; renk < graph.Count; renk++)
        {
            if (!komşurenkler.Contains(renk))
            {
                renkler[v] = renk;
                break;
            }
        }
    }
   
    static void Main()
    {
       
        
        List<Thread> threads = new List<Thread>();
        Semaphore semaphore = new Semaphore(8, 8); 

        foreach (string vertex in graph.Keys)
        {
            semaphore.WaitOne();
            Thread thread = new Thread(() =>
            {
                ColorVertices(vertex);
                semaphore.Release(); 
            });
            threads.Add(thread);
            thread.Start();
        }

       
        foreach (Thread thread in threads)
        {
            thread.Join();
        }
      
        DrawGraph();
        
        foreach (var kvp in renkler)
        {
            Console.WriteLine($"Şehir {kvp.Key} {kvp.Value} numaralı renkle boyandı");
        }

        

    }

    static void DrawGraph()
    {
        int width = 800;
        int height = 1600;

        Bitmap bitmap = new Bitmap(width, height);
        Graphics graphics = Graphics.FromImage(bitmap);
        graphics.SmoothingMode = SmoothingMode.AntiAlias;

        // Sabit bir renk paleti tanımla
        Color[] colorPalette = new Color[] { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange };

        Dictionary<string, Point> cityPositions = new Dictionary<string, Point>();

        int xOffset = 50;
        int yOffset = 50;
        int spacing = 100;
        int citiesPerRow = 5; // Bir satırdaki şehir sayısı

        int currentX = xOffset;
        int currentY = yOffset;

        int citiesInCurrentRow = 0;

        foreach (var kvp in graph)
        {
            string city = kvp.Key;

            if (citiesInCurrentRow >= citiesPerRow)
            {
                // Bir sonraki satıra geç
                currentX = xOffset;
                currentY += spacing;
                citiesInCurrentRow = 0;
            }

            Point position = new Point(currentX, currentY);
            cityPositions[city] = position;

            currentX += spacing;
            citiesInCurrentRow++;
        }

        // Her düğümü belirli bir renkle çiz
        foreach (var kvp in cityPositions)
        {
            string city = kvp.Key;
            Point position = kvp.Value;

            int colorIndex = renkler[city];
            Color color = colorPalette[colorIndex];

            Rectangle rect = new Rectangle(position.X - 15, position.Y - 15, 30, 30);

            SolidBrush brush = new SolidBrush(color);
            graphics.FillEllipse(brush, rect);
            graphics.DrawEllipse(new Pen(Color.Black), rect);
            graphics.DrawString(city, new Font("Arial", 8), new SolidBrush(Color.Black), position.X - 20, position.Y - 25);
        }

        // Kenarları çiz
        foreach (var kvp in graph)
        {
            string sourceCity = kvp.Key;
            Point sourcePosition;

            sourcePosition = cityPositions[sourceCity];

            foreach (var destinationCity in kvp.Value)
            {
                Point destinationPosition;

                destinationPosition = cityPositions[destinationCity];
                graphics.DrawLine(new Pen(Color.Black), sourcePosition, destinationPosition);
            }
        }

        bitmap.Save("graph.png");
        Console.WriteLine("Graf 'graph.png' olarak kaydedildi.");
    }



}
