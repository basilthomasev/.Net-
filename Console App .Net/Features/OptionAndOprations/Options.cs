using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.Models;

namespace Features.OptionAndOprations
{
    public class Options
    {
        public int Option()
        {
            Console.WriteLine(Message.options);
            Console.WriteLine(Message.Operations);
            int value = Convert.ToInt32(Console.ReadLine());

            switch (value)
            {
                case 1: Console.WriteLine(Message.Inserts);
                    break;
                case 2: Console.WriteLine(Message.Delete);
                    break;
                case 3: Console.WriteLine(Message.Edit);
                    break;
                case 4: Console.WriteLine(Message.View);
                    break;
                default: Console.WriteLine(Message.ValidDigit);
                    break;
            }
            return value;
        }
    }
}
