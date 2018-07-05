using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static void Load()
        {
            Random rnd = new Random();
            objs = new BaseObject[60];
            for (int i = 0; i < objs.Length * 2 / 3; i++)
            {
                if(i < objs.Length * 2 / 3 - 4)
                {
                    objs[i] = new Star(new Point(rnd.Next(0, 800), rnd.Next(0, 600)), new Point(-rnd.Next(1, i + 1), 0), new Size(5, 5));
                }
                else
                {
                    if (Math.Abs(i- objs.Length * 2 / 3) == 4)
                    objs[i] = new Planet(new Point(rnd.Next(0, 800), rnd.Next(100, 500)), new Point(-rnd.Next(1, 10), rnd.Next(1, 10)), new Size(5, 5), Planet.PlanetEnum.Earth);
                    if (Math.Abs(i - objs.Length * 2 / 3) == 3)
                        objs[i] = new Planet(new Point(rnd.Next(0, 800), rnd.Next(100, 500)), new Point(-rnd.Next(1, 10), rnd.Next(1, 10)), new Size(5, 5), Planet.PlanetEnum.Anoa);
                    if (Math.Abs(i - objs.Length * 2 / 3) == 2)
                        objs[i] = new Planet(new Point(rnd.Next(0, 800), rnd.Next(100, 500)), new Point(-rnd.Next(1, 10), rnd.Next(1, 10)), new Size(5, 5), Planet.PlanetEnum.Saturn);
                    if (Math.Abs(i - objs.Length * 2 / 3) == 1)
                        objs[i] = new Planet(new Point(rnd.Next(0, 800), rnd.Next(100, 500)), new Point(-rnd.Next(1, 10), rnd.Next(1, 10)), new Size(5, 5), Planet.PlanetEnum.Venus);
                }
            }
            for (int i = objs.Length * 2 / 3; i < objs.Length; i++)
                objs[i] = new Asteroid(new Point(rnd.Next(0, 800), rnd.Next(0, 600)), new Point(-rnd.Next(0,i/3), -rnd.Next(0, i / 3)), new Size(10, 10)); 

        }
        public static void Update()
        {
            foreach (BaseObject obj in objs)
                obj.Update();
        }
        public static void Draw()
        {
            // Проверяем вывод графики
            //Buffer.Graphics.Clear(Color.Black);
            //Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            //Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            //Buffer.Render();

            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objs)
                obj.Draw();
            Buffer.Render();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
    }
}
