using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SharpOptimization.Visual.Commands
{
    public class RunCommand
    {

        private static RoutedUICommand requery;

        public static RoutedUICommand Requery
        {
            get { return requery; }
        }

        static RunCommand()
        {
            var inputs = new InputGestureCollection
            {
                new KeyGesture(Key.F5, ModifierKeys.Control, "Ctrl + F5")
            };

            requery = new RoutedUICommand("Requery", "Requery",
                typeof(RunCommand), inputs);
        }
    }
}
