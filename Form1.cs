using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;

namespace lab7_add
{
    // Базовий клас – музичний елемент (пісня/інструментал)
    public class MusicItem
    {
        // Інкапсуляція через властивості
        public string Name { get; set; }
        public string Genre { get; set; }

        public MusicItem(string name, string genre)
        {
            Name = name;
            Genre = genre;
        }

        // Віртуальний метод – буде перевизначений у нащадках
        public virtual string Play()
        {
            return $"\"{Name}\" – відтворюється музика загального типу ({Genre}).";
        }
    }

    // Пісня – наслідує MusicItem
    public class Song : MusicItem
    {
        public string Artist { get; set; }

        public Song(string name, string artist, string genre)
            : base(name, genre)
        {
            Artist = artist;
        }

        public override string Play()
        {
            return $"Пісня \"{Name}\" – {Artist} у стилі {Genre}.";
        }
    }

    // Інструментальна композиція – теж наслідник MusicItem
    public class Instrumental : MusicItem
    {
        public string Instrument { get; set; }

        public Instrumental(string name, string instrument, string genre)
            : base(name, genre)
        {
            Instrument = instrument;
        }

        public override string Play()
        {
            return $"Інструментал \"{Name}\" на {Instrument} (жанр: {Genre}).";
        }
    }

    // Плейлист – клас, який використовує інші об’єкти (композиція)
    public class Playlist
    {
        public string Title { get; set; }
        private List<MusicItem> items = new List<MusicItem>();

        public Playlist(string title)
        {
            Title = title;
        }

        public void Add(MusicItem item)
        {
            items.Add(item);
        }

        public string Describe()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Плейлист: {Title}");
            sb.AppendLine("Склад плейлиста:");

            foreach (var i in items)
            {
                sb.AppendLine($"- {i.Name} ({i.GetType().Name})");
            }

            return sb.ToString();
        }

        public string PlayAll()
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== Відтворення всіх треків (поліморфізм) ===");

            foreach (var i in items)
            {
                // Викликається відповідний override Play()
                sb.AppendLine(i.Play());
            }

            return sb.ToString();
        }
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Прив’язуємо кнопки до обробників, якщо не зроблено в дизайнері
            button1.Click += button1_Click;
            button2.Click += button2_Click;
        }

        // Start
        private void button1_Click(object sender, EventArgs e)
        {
            var plan = new StringBuilder();
            var log = new StringBuilder();

            // ===== ЛІВЕ ВІКНО: ПЛАН =====
            plan.AppendLine("ДОДАТКОВА ПРОГРАМА: «Музичний плейлист»");
            plan.AppendLine();
            plan.AppendLine("План дій:");
            plan.AppendLine("1. Створити базовий клас MusicItem.");
            plan.AppendLine("2. Створити нащадки Song та Instrumental (наслідування).");
            plan.AppendLine("3. Створити плейлист і додати в нього декілька треків.");
            plan.AppendLine("4. Показати поліморфізм: викликати Play() для всіх елементів як MusicItem.");
            plan.AppendLine("5. Вивести опис плейлиста та результат відтворення у правому полі.");

            // ===== ПРАВЕ ВІКНО: РЕАЛЬНІ ДІЇ =====
            log.AppendLine("=== Створюємо плейлист ===");
            var playlist = new Playlist("Mood: study & chill");

            // Створюємо кілька треків
            var song1 = new Song("Bohemian Rhapsody", "Queen", "Rock");
            var song2 = new Song("Nothing Else Matters", "Metallica", "Metal");
            var inst1 = new Instrumental("Rainy Lo-Fi Beat", "синтезатор", "Lo-Fi");

            log.AppendLine("Додаємо треки до плейлиста...");
            playlist.Add(song1);
            playlist.Add(song2);
            playlist.Add(inst1);
            log.AppendLine();

            // Опис плейлиста
            log.AppendLine(playlist.Describe());
            log.AppendLine();

            // Поліморфізм: PlayAll викликає Play() у кожного треку
            log.AppendLine(playlist.PlayAll());

            // Виводимо текст
            label1.Text = plan.ToString();
            label2.Text = log.ToString();
        }

        // Stop
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Порожній обробник для label1 (бо він підписаний в Designer)
        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
