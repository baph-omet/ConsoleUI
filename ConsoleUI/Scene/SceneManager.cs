using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Vergil.Utilities;

namespace ConsoleUI {
    public class SceneManager {
        internal Scene BaseScene;
        private string PreviousBuffer;

        internal SceneManager() {
            PreviousBuffer = "";
        }

        internal void Update() {
            BaseScene?.Update();
        }

        private string GetBuffer() {
            if (BaseScene == null) return "";
            return BaseScene.Render(false);
        }

        internal void Render() {
            try {
                string buffer = GetBuffer();
                List<string> rows = buffer.Split(Environment.NewLine).ToList();
                rows.ForEach(delegate(String line){ line = line.RemoveWhitespace(); });
                string[] previousRows = PreviousBuffer.Split(Environment.NewLine);
                for (int r = 0; (r < rows.Count || r < previousRows.Length) && r < Console.BufferHeight; r++) {

                    if (r >= rows.Count) {
                        rows.Add(previousRows[r]);
                        Console.SetCursorPosition(0, r);
                        Console.WriteLine(previousRows[r]);
                        continue;
                    }

                    if (r >= previousRows.Length) {
                        Console.SetCursorPosition(0, r);
                        Console.WriteLine(rows[r]);
                        continue;

                    }


                    for (int i = 0; (i < rows[r].Length || i < previousRows[r].Length) && i < Console.BufferWidth; i++) {
                        if (i >= rows[r].Length) {
                            rows[r] += previousRows[r][i];
                        } else if (i < previousRows[r].Length && rows[r][i] == previousRows[r][i]) 
                            continue;

                        Console.SetCursorPosition(i, r);

                        Console.Write(rows[r][i]);
                    }
                }

                PreviousBuffer = rows.Join(Environment.NewLine);
            } catch (ThreadInterruptedException) { }
        }

        internal Scene GetTopScene() {
            Scene s = BaseScene;
            while (s.Subscene != null) s = s.Subscene;
            return s;
        }

        public void AddSubscene(Scene scene) {
            GetTopScene().AddSubscene(scene);
        }

        public void EndSubscene() {
            GetTopScene().Superscene.Subscene = null;
        }

        public void NextScene(Scene scene) {
            GetTopScene().Superscene.Subscene = scene;
        }

        public void SetBaseScene(Scene scene) {
            BaseScene = scene;
        }
    }
}
