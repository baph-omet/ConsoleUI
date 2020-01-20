using System;
using System.Threading;

namespace ConsoleUI {
    public class GameProgram {
        public static SceneManager SceneManager;
        private static bool Running;

        protected internal static int[] WindowSize;

        protected static Scene StartingScene = null;

        protected static void Initialize() {
            SceneManager = new SceneManager();
            Running = true;
            Console.SetWindowSize(WindowSize[0], WindowSize[1]);
            Console.SetBufferSize(WindowSize[0], WindowSize[1]);
            Console.SetWindowPosition(0, 0);
            Console.CursorVisible = false;
        }

        public static void Run() {
            if (StartingScene == null) throw new ArgumentException("StartingScene is not set");
            SceneManager.BaseScene = StartingScene;
            GameLoop();
        }

        private static void GameLoop() {
            //Thread t = null;
            while (Running) {
                SceneManager.Update();
                SceneManager.Render();
                /*if (t != null && t.IsAlive) t.Interrupt();

                t = new Thread(new ThreadStart(SceneManager.Render));
                t.Start();*/
            }
        }

        public static void Quit() {
            Running = false;
        }
    }
}
