using System;

namespace ConsoleUI {
    class Control {

        public static ConsoleKeyInfo GetKey() {
            if (Console.KeyAvailable) return Console.ReadKey(true);
            return new ConsoleKeyInfo();
        }

        public static void WaitForKey() {
            GetKey();
        }
        public static void WaitForKey(ConsoleKey key) {
            while (GetKey().Key != key) { }
        }
        public static ConsoleKey WaitForKey(ConsoleKey[] keys) {
            while (true) {
                ConsoleKey key = GetKey().Key;
                foreach (ConsoleKey k in keys) {
                    if (k == key) return key;
                }
            }
        }

        public static bool IsTextCharacter(ConsoleKeyInfo key) {
            return char.IsLetterOrDigit(key.KeyChar) || key.Key == ConsoleKey.Spacebar;
        }
    }
}
