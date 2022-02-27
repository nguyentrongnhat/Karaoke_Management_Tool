using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CĐCNPM.Pattern.Factory
{
    public class SimpleFactory
    {
        public static SimpleFactory factory = new SimpleFactory();
        public static SimpleFactory getInstance()
        {
            return factory;
        }
        private SimpleFactory() { }
        public RoomManagerForm createRoomManager(Boolean isAdmin)
        {
            if (isAdmin == true)
            {
                return RoomManagerForm.getInstanceAdmin();
            }
            else
                return RoomManagerForm.getInstanceNV();
        }
    }
}
