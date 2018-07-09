using System;
using System.Windows.Forms;
using System.Drawing;

namespace HW_Klyushin_1
{
    using System.Runtime.CompilerServices;

    static class Game
    {
        private static BufferedGraphicsContext context;
        public static BufferedGraphics GameBuffer;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static BaseObject[] objs;

        public static Bullet bullet;

        private static BaseObject[] asteroids;

        private static Timer timer;

        //Инициализируем генератор случайных чисел
        private static Random rnd = new Random();

        static Game()
        {
        }

        //метод инициализации буфкра для вывода на форму
        public static void Init(gameForm form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();

            timer = new Timer { Interval = 100 };
            timer.Start();
            
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            GameBuffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            try
            {
                if (Width > 1000 || Width < 0 || Height > 1000 || Height < 0) throw new ArgumentOutOfRangeException();
            }
            catch (ArgumentOutOfRangeException e)
            {
                if (Width > 1000)
                    MessageBox.Show(
                        "Слишком большая ширина окна",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                if (Height > 1000)
                    MessageBox.Show(
                        "Слишком большая высота окна",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                if (Width < 0 || Height < 0)
                        MessageBox.Show(
                            "Отрицательный размер окна",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            Game.Load();

            timer.Tick += Timer_Tick;
        }

        //метод создания отображаемых объектов
        public static void Load()
        {
            //Создаем массив движущихся объектов
            objs = new BaseObject[30];
            for (int i = 0; i < objs.Length; i++)
            {
                if(i < objs.Length - 4)
                {
                    //добавление объектов класса Star
                    objs[i] = new Star(new Point(rnd.Next(0, Width), rnd.Next(0, Height)), new Point(-rnd.Next(1, i + 1), 0), new Size(5, 5));
                }
                else
                {
                    //добавление объектов класса Planet
                    if (Math.Abs(i- objs.Length) == 4)
                    objs[i] = new Earth(new Point(rnd.Next(0, Width), rnd.Next(100, Height - 100)), new Point(-rnd.Next(1, 10), rnd.Next(1, 10)), Planet.PlanetsEnum.Earth);
                    if (Math.Abs(i - objs.Length) == 3)
                        objs[i] = new Anoa(new Point(rnd.Next(0, Width), rnd.Next(100, Height - 100)), new Point(-rnd.Next(1, 10), rnd.Next(1, 10)), Planet.PlanetsEnum.Anoa);
                    if (Math.Abs(i - objs.Length) == 2)
                        objs[i] = new Saturn(new Point(rnd.Next(0, Width), rnd.Next(100, Height - 100)), new Point(-rnd.Next(1, 10), rnd.Next(1, 10)), Planet.PlanetsEnum.Saturn);
                    if (Math.Abs(i - objs.Length) == 1)
                        objs[i] = new Venus(new Point(rnd.Next(0, Width), rnd.Next(100, Height - 100)), new Point(-rnd.Next(1, 10), rnd.Next(1, 10)), Planet.PlanetsEnum.Venus);
                }
            }

            //добавление объектов класса Asteroid
            asteroids = new BaseObject[20];
            for (int i = 0; i < asteroids.Length; i++)
                asteroids[i] = new Asteroid(
                    new Point(rnd.Next(0, Width), rnd.Next(15, Height - 15)),
                    new Point(-rnd.Next(0, i), -rnd.Next(0, i)),
                    new Size(20, 20));

            bullet = new Bullet(new Point(0, rnd.Next(0, 600)), new Point(10, 0), new Size(10, 10));
        }

        //метод обновления состояния объектов
        public static void Update()
        {
            foreach (BaseObject obj in objs)
                obj.Update();
            foreach (Asteroid a in asteroids)
            {
                a.Update();
                if (a.Collision(bullet) || bullet.Position.X >= Width)
                {
                    System.Media.SystemSounds.Hand.Play();
                    bullet = new Bullet(new Point(0, rnd.Next(15, Height - 15)), new Point(10, 0), new Size(10, 10));
                    a.Regenerate();
                }
            }
            bullet.Update();
        }

        //метод отрисовки объектов в буфере с последующим выводом на форму
        public static void Draw()
        {
            GameBuffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objs)
                obj.Draw();
            foreach (Asteroid obj in asteroids)
                obj.Draw();
            bullet.Draw();
            GameBuffer.Render();
        }

        //обработчик события для таймера
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Stop()
        {
            timer.Stop();
            GameBuffer.Dispose();
        }
    }
}
