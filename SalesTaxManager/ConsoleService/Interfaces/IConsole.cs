using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxManager.ConsoleService
{
    public interface IConsole
    {
        public void Write(string message);
        public void WriteLine(string message);
        public string ReadLine();
    }
}
