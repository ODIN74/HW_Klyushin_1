using System;
using System.IO;
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

        //массив для отрисовки фона
        public static BaseObject[] objs;

        //массив снарядов
        public static Bullet[] bullets = new Bullet[1];

        //корабль
        public static SpaceShip ship;

        //аптечка
        public static MedicalKit medicalKit;

        //массив астероидов
        private static BaseObject[] asteroids;

        //таймер для отрисовки
        private static Timer timer;

        //таймер для аптечки
        private static Timer medicalTimer;

        //делегат для логирования в консоль и файл
        public delegate void MessageToReciver();
        public static MessageToReciver ActionMessage;

        //стрим для логирования в файл
        private static StreamWriter log = new StreamWriter(@"D:\Основы программирования\C Sharp\Level_2\HW_Klyushin_1\HW_Klyushin_1\bin\Debug\LastGame.log");

        //вспомогательная переменная для организации лигики аптечки
        private static bool medicalFlag = false;

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

            medicalTimer = new Timer { Interval = 120000 };
            medicalTimer.Start();
            
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            GameBuffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Game.Load();

            timer.Tick += Timer_Tick;

            medicalTimer.Tick += MedicalTimer_Tick;

            SpaceShip.MessageDie += Finish;

            form.KeyDown += Form_KeyDown;
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
                    objs[i] = new Earth(new Point(rnd.Next(0, Width), rnd.Next(100, Height - 100)), 
                                        new Point(-rnd.Next(1, 10), rnd.Next(1, 10)), 
                                        Planet.PlanetsEnum.Earth);
                    if (Math.Abs(i - objs.Length) == 3)
                        objs[i] = new Anoa(new Point(rnd.Next(0, Width), rnd.Next(100, Height - 100)),
                                           new Point(-rnd.Next(1, 10), rnd.Next(1, 10)),
                                           Planet.PlanetsEnum.Anoa);
                    if (Math.Abs(i - objs.Length) == 2)
                        objs[i] = new Saturn(new Point(rnd.Next(0, Width), rnd.Next(100, Height - 100)), 
                                             new Point(-rnd.Next(1, 10), rnd.Next(1, 10)), 
                                             Planet.PlanetsEnum.Saturn);
                    if (Math.Abs(i - objs.Length) == 1)
                        objs[i] = new Venus(new Point(rnd.Next(0, Width), rnd.Next(100, Height - 100)), 
                                            new Point(-rnd.Next(1, 10), rnd.Next(1, 10)), 
                                            Planet.PlanetsEnum.Venus);
                }
            }

            //добавление объектов класса Asteroid
            asteroids = new BaseObject[20];
            for (int i = 0; i < asteroids.Length; i++)
                asteroids[i] = new Asteroid(
                    new Point(Width, rnd.Next(15, Height - 15)),
                    new Point(-rnd.Next(5, 10), -rnd.Next(1, 5)),
                    new Size(30, 30));

            //добавляем корабль
            ship = new SpaceShip(new Point(0, Height / 2), new Point(5, 5), new Size(30, 30));

            //добавляем аптечку
            medicalKit = new MedicalKit(new Point(Width, rnd.Next(10, Height - 10)), new Point(10,0), new Size(20, 20));
            ActionMessage = GameStart;
        }

        //метод обновления состояния объектов
        public static void Update()
        {
            //обновление состояния фона
            foreach (BaseObject obj in objs) obj.Update();


            //обновление состояния снарядов
            foreach (var bullet in bullets) bullet?.Update();

            //обновление состояния аптечки
            if (medicalFlag)
                medicalKit.Update();

            //проверка состояния аптечки
            if (ship.Collision(medicalKit) || medicalKit.Position.X < 0)
            {
                if (ship.Collision(medicalKit)) ship.IncreaseEnergy(10);
                medicalFlag = false;
                medicalKit.Regenerate();
                medicalTimer.Stop();
                medicalTimer.Start();
                ActionMessage = TakenMedicalKid;
            }

            //проверка состояния астероидов
            for (var i = 0; i < asteroids.Length; i++)
            {
                if(asteroids[i] == null) continue;
                asteroids[i].Update();
                for (int j = 0; j < bullets.Length; j++)
                {
                    if (bullets[j] != null && bullets[j].Collision(asteroids[i]))
                    {
                        System.Media.SystemSounds.Hand.Play();
                        ActionMessage = AsteroidCollapse;
                        (asteroids[i] as Asteroid).Regenerate();
                        bullets[j] = null;
                        ship.HitTarget();
                    }
                }
                if (!ship.Collision(asteroids[i])) continue;
                ship?.ReductionOfEnergy(10);
                ActionMessage = AsteroidCollision;
                (asteroids[i] as Asteroid).Regenerate();
                System.Media.SystemSounds.Asterisk.Play();

                //проверка состояния корабля
                if (ship.Energy <= 0)
                {
                    ship?.Die();
                    ActionMessage = GameEnd;
                }
            }
            ActionMessage?.Invoke();
        }

        //метод отрисовки объектов в буфере с последующим выводом на форму
        public static void Draw()
        {
            GameBuffer.Graphics.Clear(Color.Black);
            
            //отрисовка фона
            foreach (BaseObject obj in objs)
                obj.Draw();

            //отрисовка астероидов
            foreach (Asteroid a in asteroids)
                if (a != null) a.Draw();

            //отрисовка корабля
            ship?.Draw();

            //отрисовка снарядов
            foreach (var bullet in bullets)
            bullet?.Draw();

            //отрисовка аптечки
            if (medicalFlag)
                 medicalKit.Draw();

            //вывод остатка энергии и количества очков
            if (ship != null)
                GameBuffer.Graphics.DrawString("Energy:" +" "+ ship.Energy + "  Points:" +" " + ship.Points,
                    SystemFonts.DefaultFont, Brushes.Red, 0, 0);

            GameBuffer.Render();
        }

        //управление кораблем
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                Array.Resize(ref bullets,bullets.Length+1);
                bullets[bullets.Length -1] = new Bullet(new Point(ship.Position.X + 40, ship.Position.Y + 16), new Point(10, 0), new Size(10, 10));
            }
            if (e.KeyData == Keys.Up) ship.MoveUp();
            if (e.KeyData == Keys.Down) ship.MoveDown();
            if (e.KeyData == Keys.Left) ship.MoveLeft();
            if (e.KeyData == Keys.Right) ship.MoveRight();
        }

        //обработчик события для таймера отрисовки
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
            ActionMessage = null;
        }

        //обработчик события для таймера аптечки
        private static void MedicalTimer_Tick(object sender, EventArgs e)
        {
            medicalFlag = true;
        }

        //метод для корректного выхода при закрытии формы
        public static void Stop()
        {
            timer.Stop();
            medicalTimer.Stop();
            GameBuffer.Dispose();
            log.Close();
        }

        //метод окончания игры
        public static void Finish()
        {
            Draw();
            timer.Stop();
            GameBuffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif,
                60, FontStyle.Bold), Brushes.Red, 200, 100);
            GameBuffer.Render();
        }

        //метод записи сообщения о начале игры
        private static void GameStart()
        {
            string text = $"{DateTime.Now} {"Игра началась"} Энергия {ship.Energy} Очков {ship.Points}";
            Console.WriteLine(text);
            log.WriteLine(text);
        }

        //метод записи сообщения об окончании игры
        private static void GameEnd()
        {
            string text = $"{DateTime.Now} {"Игра окончена"} Энергия {ship.Energy} Очков {ship.Points}";
            Console.WriteLine(text);
            log.WriteLine(text);
            log.Close();
        }

        //метод записи сообщения о начале игры
        private static void AsteroidCollapse()
        {
            string text = $"{DateTime.Now} {"Уничтожен астероид"} Энергия {ship.Energy} Очков {ship.Points}";
            Console.WriteLine(text);
            log.WriteLine(text);
        }

        //метод записи сообщения о взятии аптечки
        private static void TakenMedicalKid()
        {
            string text = $"{DateTime.Now} {"Взята аптечка"} Энергия {ship.Energy} Очков {ship.Points}";
            Console.WriteLine(text);
            log.WriteLine(text);
        }

        //метод записи сообщения о столкновении с астероидом
        private static void AsteroidCollision()
        {
            string text = $"{DateTime.Now} {"Столкновение с астероидом"} Энергия {ship.Energy} Очков {ship.Points}";
            Console.WriteLine(text);
            log.WriteLine(text);
        }
    }
}
