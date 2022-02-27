using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CĐCNPM.Pattern.Command;

namespace CĐCNPM.Pattern.Builder
{
    public interface Command_Builder
    {
        Command_Builder Start_Builder(ICommand Start);
        Command_Builder End_Builder(ICommand End);
        Command_Builder Add_Builder(ICommand Add);
        Command_Builder Click_Builder(ICommand Click);
        Invoker_Command build();
    }

    public class Invoker_Command_Builder : Command_Builder
    {
        ICommand start;
        ICommand end;
        ICommand add;
        ICommand click;
        public Command_Builder Start_Builder (ICommand start)
        {
            this.start = start;
            return this;
        }
        public Command_Builder End_Builder (ICommand end)
        {
            this.end = end;
            return this;
        }
        public Command_Builder Add_Builder (ICommand add)
        {
            this.add = add;
            return this;
        }
        public Command_Builder Click_Builder(ICommand click)
        {
            this.click = click;
            return this;
        }
        public Invoker_Command build()
        {
            return new Invoker_Command (start, end, add, click);
        }
    }
}
