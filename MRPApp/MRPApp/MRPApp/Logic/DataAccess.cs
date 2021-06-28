using MRPApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRPApp.Logic
{
    public class DataAccess
    {
        public static List<Settings> GetSettings()
        {
            List<Model.Settings> settings;

            using (MRPEntities ctx = new MRPEntities())
            {
                settings = ctx.Settings.ToList();
            }

            return settings;
        }

        public static int SetSettings(Settings item)
        {
            using (MRPEntities ctx = new MRPEntities())
            {
                ctx.Settings.AddOrUpdate(item);
                return ctx.SaveChanges(); // COMMIT
            }
        }

        public static List<Schedules> GetSchedules()
        {
            List<Model.Schedules> schedules;

            using (MRPEntities ctx = new MRPEntities())
            {
                schedules = ctx.Schedules.ToList();
            }

            return schedules;
        }

        public static int SetSchedules(Schedules item)
        {
            using (MRPEntities ctx = new MRPEntities())
            {
                ctx.Schedules.AddOrUpdate(item);
                return ctx.SaveChanges(); // COMMIT
            }
        }
    }
}
