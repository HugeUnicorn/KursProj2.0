using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursProj.Model
{
    public enum TableName
    {
        Authors,
        Books,
        Genres,
        Role,
        PublishingHouse,
        State,        
        Users,        
        UserBookPair
    }
    public static class AppData
    {
        public static BDEntities db = new BDEntities();

        public static int userID;
    }
}
