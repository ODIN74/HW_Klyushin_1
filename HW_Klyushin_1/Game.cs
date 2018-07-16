using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace HW_Klyushin_1
{
    /// <summary>
    /// Класс игрового процесса
    /// </summary>
    internal static class Game
    {

        /// <summary>
        /// Буфер для отрисовки игры
        /// </summary>
        public static BufferedGraphics GameBuffer;
        


        /// <summary>
        /// Массив объектов фона
        /// </summary>
        private static BaseObject[] objs;

        /// <summary>
        /// Коллекция снарядов
        /// </summary>
        private static List<Bullet> bullets = new List<Bullet>();

        /// <summary>
        /// Экемпляр космического корабля
        /// </summary>
        private static SpaceShip ship;

        /// <summary>
        /// Экземпляр аптечки
        /// </summary>
        private static MedicalKit medicalKit;

        /// <summary>
        /// Графический контекст буфера
        /// </summary>
        private static BufferedGraphicsContext context;
        
        /// <summary>
        /// Коллекция астероидов
        /// </summary>
        private static List<BaseObject> asteroids = new List<BaseObject>();

        /// <summary>
        /// Исходное колличество астероидов в коллекции на момент начала игры
        /// </summary>
        private static int asteroidsCounter = 20;

        /// <summary>
        /// Таймер отрисовки объектов
        /// </summary>
        private static Timer timer;

        /// <summary>
        /// Таймер для генерации аптечки
        /// </summary>
        private static Timer medicalTimer;

        /// <summary>
        /// Флаг для генерации аптечки
        /// </summary>
        private static bool medicalFlag = false;

        /// <summary>
        /// Генератор случайных чисел
        /// </summary>
        private static Random rnd = new Random();

        /// <summary>
        /// Поток для записи лога
        /// </summary>
        private static StreamWriter log;

        /// <summary>
        /// Делегат для записи лога в консоль и файл
        /// </summary>
        public delegate void MessageToReciver();

        /// <summary>
        /// Экземпляр делегата для записи лога в консоль и файл
        /// </summary>
        private static MessageToReciver ActionMessage;

        /// <summary>
        /// Ширина ирового поля
        /// </summary>
        public static int Width { get; set; }

        /// <summary>
        /// Высота игрвого поля
        /// </summary>
        public static int Height { get; set; }

        /// <summary>
        /// Метод инициализации игрового процесса
        /// </summary>
        /// <param name="form">Форма для отображения игрового процесса</param>
        public static void Init(gameForm form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;

            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();

            log = new StreamWriter(@".\LastGame.log");

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

            //генереруем игровые объекты
            Game.Load();

            //подписка на события
            timer.Tick += Timer_Tick;

            medicalTimer.Tick += MedicalTimer_Tick;

            SpaceShip.MessageDie += Finish;

            form.KeyDown += Form_KeyDown;
        }


        /// <summary>
        /// Метод инициализации игровых объектов
        /// </summary>
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

            AsteroidsGenerate(asteroidsCounter);

            //добавляем корабль
            ship = new SpaceShip(new Point(0, Height / 2), new Point(5, 5), new Size(30, 30));

            //добавляем аптечку
            medicalKit = new MedicalKit(new Point(Width, rnd.Next(10, Height - 10)), new Point(10,0), new Size(20, 20));
            ActionMessage = GameStart;
        }

        /// <summary>
        /// Метод обновления состояния игровых объектов с проверкой игровых событий
        /// </summary>
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
            for (var i = 0; i < asteroids.Count; i++)
            {
                if(asteroids[i] == null) continue;
                asteroids[i].Update();
                for (int j = 0; j < bullets.Count; j++)
                {
                    if (bullets[j] != null && bullets[j].Collision(asteroids[i]))
                    {
                        System.Media.SystemSounds.Hand.Play();
                        ActionMessage = AsteroidCollapse;
                        asteroids[i] = null;
                        bullets[j] = null;
                        ship.HitTarget();
                        break;
                    }
                    if (bullets[j]?.Position.X > Game.Width) bullets[j] = null;
                }
                if (asteroids[i] == null) continue;
                if (!ship.Collision(asteroids[i])) continue;
                ship?.ReductionOfEnergy(10);
                ActionMessage = AsteroidCollision;
                asteroids[i] = null;
                System.Media.SystemSounds.Asterisk.Play();

                //проверка состояния корабля
                if (ship.Energy <= 0)
                {
                    ship?.Die();
                    ActionMessage = GameEnd;
                }
            }

            //проверка сбиты ли все астероиды
            if (ListEmpty(asteroids))
            {
                asteroidsCounter++;
                AsteroidsGenerate(asteroidsCounter);
            }
            ActionMessage?.Invoke();
        }

        /// <summary>
        /// Метод отрисовки игровых объектов
        /// </summary>
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
                GameBuffer.Graphics.DrawString("Energy:" +" "+ ship.Energy + "  Points:" + " " + ship.Points,
                    SystemFonts.DefaultFont, Brushes.Red, 0, 0);

            //отрисовка буфера
            GameBuffer.Render();
        }

        /// <summary>
        /// Метод корректного закрытия игровой формы
        /// </summary>
        public static void Stop()
        {
            timer.Stop();
            medicalTimer.Stop();
            GameBuffer.Dispose();
            log.Close();
        }

        /// <summary>
        /// Метод окончания игры
        /// </summary>
        public static void Finish()
        {
            Draw();
            timer.Stop();
            GameBuffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif,
                60, FontStyle.Bold), Brushes.Red, 200, 100);
            GameBuffer.Render();
        }

        /// <summary>
        /// Обработка события нажатия клавишь
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Уведомление</param>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {             
                bullets.Add(new Bullet(new Point(ship.Position.X + 40, ship.Position.Y + 16), new Point(10, 0), new Size(10, 10)));
            }
            if (e.KeyData == Keys.Up) ship.MoveUp();
            if (e.KeyData == Keys.Down) ship.MoveDown();
            if (e.KeyData == Keys.Left) ship.MoveLeft();
            if (e.KeyData == Keys.Right) ship.MoveRight();
        }

        /// <summary>
        /// Обработка игрового таймера
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Уведомление</param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
            ActionMessage = null;
        }

        /// <summary>
        /// Обработка таймера аптечки
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Уведомление</param>
        private static void MedicalTimer_Tick(object sender, EventArgs e)
        {
            medicalFlag = true;
        }

        /// <summary>
        /// Метод записи в лог сообщения о начале игры
        /// </summary>
        private static void GameStart()
        {
            string text = $"{DateTime.Now} {"Игра началась"} Энергия {ship.Energy} Очков {ship.Points}";
            Console.WriteLine(text);
            log.WriteLine(text);
        }

        /// <summary>
        /// Метод записи в лог сообщения об окончании игры
        /// </summary>
        private static void GameEnd()
        {
            string text = $"{DateTime.Now} {"Игра окончена"} Энергия {ship.Energy} Очков {ship.Points}";
            Console.WriteLine(text);
            log.WriteLine(text);
            log.Close();
        }

        /// <summary>
        /// Метод записи в лог сообщения о сбитии астероида
        /// </summary>
        private static void AsteroidCollapse()
        {
            string text = $"{DateTime.Now} {"Уничтожен астероид"} Энергия {ship.Energy} Очков {ship.Points}";
            Console.WriteLine(text);
            log.WriteLine(text);
        }

        /// <summary>
        /// Метод записи в лог сообщения о взятии аптечки
        /// </summary>
        private static void TakenMedicalKid()
        {
            string text = $"{DateTime.Now} {"Взята аптечка"} Энергия {ship.Energy} Очков {ship.Points}";
            Console.WriteLine(text);
            log.WriteLine(text);
        }

        /// <summary>
        /// Метод записи в лог сообщения о столкновении с астероидом
        /// </summary>
        private static void AsteroidCollision()
        {
            string text = $"{DateTime.Now} {"Столкновение с астероидом"} Энергия {ship.Energy} Очков {ship.Points}";
            Console.WriteLine(text);
            log.WriteLine(text);
        }

        /// <summary>
        /// Метод генерации коллекции астероидов
        /// </summary>
        /// <param name="asteroidsCounter">Колличество астероидов</param>
        private static void AsteroidsGenerate(int asteroidsCounter)
        {
            asteroids = new List<BaseObject>();
            for (int i = 0; i < asteroidsCounter; i++)
                asteroids.Add(new Asteroid(
                    new Point(Width, rnd.Next(15, Height - 15)),
                    new Point(-rnd.Next(5, 10), -rnd.Next(1, 5)),
                    new Size(30, 30)));
        }

        /// <summary>
        /// Метод проверки опустошения коллекции
        /// </summary>
        /// <param name="list">Проверяемая коллекция</param>
        /// <returns>True or False</returns>
        private static bool ListEmpty<T>(List<T> list)
        {
            foreach(var memberList in list)
            {
                if (memberList != null) return false;
            }
            return true;
        }
    }
}
