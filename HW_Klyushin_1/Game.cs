using System;
using System.Windows.Forms;
using System.Drawing;

namespace HW_Klyushin_1
{
    static class Game
    {
        private static BufferedGraphicsContext context;
        public static BufferedGraphics Buffer;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static BaseObject[] objs;
        static Game()
        {
        }

        //метод инициализации буфкра для вывода на форму
        public static void Init(System.Windows.Forms.Form form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Game.Load();

            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;

        }

        //метод создания отображаемых объектов
        public static void Load()
        {
            //Инициализируем генератор случайных чисел
            Random rnd = new Random();

            //Создаем массив движущихся объектов
            objs = new BaseObject[60];
            for (int i = 0; i < objs.Length * 2 / 3; i++)
            {
                if(i < objs.Length * 2 / 3 - 4)
                {
                    //добавление объектов класса Star
                    objs[i] = new Star(new Point(rnd.Next(0, 800), rnd.Next(0, 600)), new Point(-rnd.Next(1, i + 1), 0), new Size(5, 5));
                }
                else
                {
                    //добавление объектов класса Planet
                    if (Math.Abs(i- objs.Length * 2 / 3) == 4)
                    objs[i] = new Earth(new Point(rnd.Next(0, 800), rnd.Next(100, 500)), new Point(-rnd.Next(1, 10), rnd.Next(1, 10)), Planet.PlanetsEnum.Earth);
                    if (Math.Abs(i - objs.Length * 2 / 3) == 3)
                        objs[i] = new Anoa(new Point(rnd.Next(0, 800), rnd.Next(100, 500)), new Point(-rnd.Next(1, 10), rnd.Next(1, 10)), Planet.PlanetsEnum.Anoa);
                    if (Math.Abs(i - objs.Length * 2 / 3) == 2)
                        objs[i] = new Saturn(new Point(rnd.Next(0, 800), rnd.Next(100, 500)), new Point(-rnd.Next(1, 10), rnd.Next(1, 10)), Planet.PlanetsEnum.Saturn);
                    if (Math.Abs(i - objs.Length * 2 / 3) == 1)
                        objs[i] = new Venus(new Point(rnd.Next(0, 800), rnd.Next(100, 500)), new Point(-rnd.Next(1, 10), rnd.Next(1, 10)), Planet.PlanetsEnum.Venus);
                }
            }

            //добавление объектов класса Asteroid
            for (int i = objs.Length * 2 / 3; i < objs.Length; i++)
                objs[i] = new Asteroid(new Point(rnd.Next(0, 800), rnd.Next(0, 600)), new Point(-rnd.Next(0,i/3), -rnd.Next(0, i / 3)), new Size(10, 10)); 

        }

        //метод обновления состояния объектов
        public static void Update()
        {
            foreach (BaseObject obj in objs)
                obj.Update();
        }

        //метод отрисовки объектов в буфере с последующим выводом на форму
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objs)
                obj.Draw();
            Buffer.Render();
        }

        //обработчик события для таймера
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
    }
}
